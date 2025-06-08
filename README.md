
# 🌦️ Climby - Sistema de Monitoramento Climático e Abrigos

**Climby** é um projeto acadêmico que oferece alertas em situações climáticas críticas (chuvas fortes, calor extremo) e lista abrigos disponíveis na cidade do usuário. A ideia é ajudar pessoas em momentos de emergência climática, integrando dados meteorológicos com informações locais.

---

## 🚀 Tecnologias Utilizadas

### Backend
- [.NET 8](https://dotnet.microsoft.com/)
- **Entity Framework Core** (com Oracle Database)
- **Oracle Database** (banco de dados FIAP)
- **ML.NET** (modelo preditivo para alertas climáticos)
- **Swagger** (documentação dos endpoints)

### Frontend (em desenvolvimento/testes)
- [React Native (Expo)](https://expo.dev/)

### Extras
- **Wokwi** (simulação de sensores IoT)

---

## 🛠️ Como Executar o Projeto

### Pré-requisitos
- .NET 8 SDK instalado
- Oracle Client (ou acesso remoto ao banco Oracle da FIAP)
- Visual Studio ou VS Code

### Passos

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/climby-api.git
   cd climby-api
   ```

2. Restaure os pacotes e execute as migrações (caso tenha):
   ```bash
   dotnet restore
   dotnet ef database update
   ```

3. Execute o backend:
   ```bash
   dotnet run
   ```

4. Acesse a documentação Swagger para testar os endpoints:
   ```
   https://localhost:7173/swagger
   ```

---

## 📚 Documentação dos Endpoints

> Atualmente, o projeto **não possui autenticação**, então todos os endpoints estão abertos.

### Users

| Método | Endpoint        | Descrição                |
|--------|-----------------|--------------------------|
| POST    | /api/user/login      | Cria login de usuário |
| GET    | /api/user/{id}      | Lista informacoes do usuário |
| POST   | /api/user     | Cadastra um usuário     |
| PUT    | /api/user/{id} | Atualiza um usuário      |
| DELETE | /api/user/{id} | Deleta um usuário        |

### Shelters

| Método | Endpoint                    | Descrição                                                  |
|--------|-----------------------------|------------------------------------------------------------|
| GET    | /api/shelters/  | Lista abrigos por cidade (exibe nome, telefone, endereço, etc) |

##  Parâmetros de Query

| Parâmetro | Tipo     | Obrigatório | Descrição                        |
|-----------|----------|-------------|----------------------------------|
| `city`    | `string` | Sim         | Nome da cidade a ser pesquisada |

## Exemplo de Requisição

https://localhost:7173/api/shelters?city=S%C3%A3o%20Paulo

### Weather

| Método | Endpoint              | Descrição                                                       |
|--------|-----------------------|-----------------------------------------------------------------|
| GET    | /api/weather/Today   | Retorna clima atual e previsão de alertas automáticos via ML.NET |

##  Parâmetros de Query

| Parâmetro | Tipo     | Obrigatório | Descrição                        |
|-----------|----------|-------------|----------------------------------|
| `city`    | `string` | Sim         | Nome da cidade a ser pesquisada |

## Exemplo de Requisição

GET https://localhost:7173/api/weather/today?city=S%C3%A3o%20Paulo

### Sensor (nao conectado aos sensores)

| Método | Endpoint              | Descrição                                                       |
|--------|-----------------------|-----------------------------------------------------------------|
| POST    | /api/Sensor/sensor-weather  | Envia dados do sensor e retorna um possivel alerta|

---

## 🤖 Machine Learning

- Utiliza **ML.NET** para prever alertas climáticos com base em dados meteorológicos fornecidos pela OpenWeatherAPI, como temperatura. nivel de chuva e etc.

---

## 📌 Observações

- A autenticação foi removida devido a problemas técnicos.

---

Projeto desenvolvido por **Celeste**  
Estudante de Análise e Desenvolvimento de Sistemas na FIAP  
São Paulo - Brasil  
[linkedin :)](https://linkedin.com/in/celestetanaka)
