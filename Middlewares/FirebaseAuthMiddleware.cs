using FirebaseAdmin.Auth;
using System.Security.Claims;
using climby.Repositories;

namespace climby.Middlewares
{
    public class FirebaseAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public FirebaseAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            if (path != null && (path.StartsWith("/swagger") || path.StartsWith("/favicon.ico") || path.StartsWith("/ping")))
            {
                await _next(context);
                return;
            }

            if (context.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = 200;
                await _next(context);
                return;
            }

            var authorization = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            {
                var token = authorization.Substring("Bearer ".Length).Trim();

                try
                {
                    var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);
                    var uid = decodedToken.Uid;

                    var userRepository = context.RequestServices.GetRequiredService<IUserRepository>();

                    // Busca o usuário no banco pelo UID do Firebase
                    var user = await userRepository.GetByFirebaseUidAsync(uid);

                    if (user == null)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Usuário não encontrado.");
                        return;
                    }

                    var claims = new List<Claim>
                    {
                        new Claim("uid", uid),
                        new Claim("city", user.City ?? string.Empty)
                    };

                    var identity = new ClaimsIdentity(claims, "Firebase");
                    context.User = new ClaimsPrincipal(identity);
                }
                catch (FirebaseAuthException ex)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync($"Token inválido: {ex.Message}");
                    return;
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync($"Erro interno: {ex.Message}");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token não fornecido.");
                return;
            }

            await _next(context);
        }
    }
}