using FirebaseAdmin.Auth;
using System.Security.Claims;

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

            // Libera swagger, favicon e ping sem autenticação
            if (path != null && (path.StartsWith("/swagger") || path.StartsWith("/favicon.ico") || path.StartsWith("/ping")))
            {
                await _next(context);
                return;
            }

            // Libera requisições OPTIONS (CORS preflight)
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

                    var claims = new[] { new Claim("uid", uid) };
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
