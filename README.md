# 🏠 SafeHub

Sistema para gestão de abrigos emergenciais, oferecendo controle de ocupação, estoque, localização e administração em situações de emergência.

---

### 👥 Integrantes do Grupo

| Nome             | RM     | Sala    |
|------------------|--------|---------|
| Julia Monteiro   | 557023 | 2TDSPV  |
| Victor Henrique  | 556206 | 2TDSPH  |
| Sofia Petruk     | 556585 | 2TDSPV  |

---

### 💡 Sobre o Projeto

O **SafeHub** é uma API RESTful desenvolvida em .NET 8 para gerenciamento de abrigos emergenciais. Permite o cadastro de usuários, controle de estoque, ocupação dos abrigos, além de interações com banco de dados Oracle utilizando Entity Framework Core.

---

### ✨ Funcionalidades

- Cadastro de abrigos com localização e capacidade
- Registro de ocupação diária do abrigo
- Controle de estoque por tipo de item (água, alimento, roupa, medicamento)
- Relacionamento entre usuários e abrigos
- Validações e mensagens personalizadas nos modelos

---

### ⚙️ Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core
- Oracle Database 11g+
- Oracle.EntityFrameworkCore
- Swashbuckle (Swagger)
- xUnit para testes
- FluentAssertions

---

### 🔧 Ambiente de Desenvolvimento

```bash
# Pré-requisitos:
- .NET 8 SDK
- Oracle Database 11g ou superior
- Visual Studio 2022 ou superior

# Comandos:
dotnet restore                 # Restaura os pacotes
dotnet ef database update     # Aplica as migrations no banco Oracle
dotnet run                    # Executa a aplicação


classDiagram

  class CadastroAbrigo {
    +long IdCadastroAbrigo
    +string NomeAbrigo
    +int CapacidadePessoa
    +string NomeResponsavel
    +string Latitude
    +string Longitude
  }

  class CadastroUsuario {
    +long IdUsuario
    +string Nome
    +string Email
    +string Senha
    +long FkIdAbrigo
  }

  class AbrigoOcupacao {
    +long IdOcupacao
    +int NumeroPessoa
    +DateTime DataRegistro
    +long FkIdAbrigo
  }

  class EstoqueAbrigo {
    +long IdEstoque
    +string NomeItem
    +EstoqueAbrigoEnum TipoItem
    +float Quantidade
    +long FkIdAbrigo
  }

  CadastroAbrigo "1" --> "*" CadastroUsuario : usuarios
  CadastroAbrigo "1" --> "*" AbrigoOcupacao : ocupacoes
  CadastroAbrigo "1" --> "*" EstoqueAbrigo : estoques

###📚 Acesso e Documentação
  - Após rodar o projeto com dotnet run, a aplicação estará disponível em:
    - http://localhost:5042/swagger/index.html

###📥 Exemplos de Requisições
curl -X POST https://localhost:5001/api/abrigos \
-H "Content-Type: application/json" \
-d '{
  "nomeAbrigo": "Abrigo Central",
  "capacidadePessoa": 200,
  "nomeResponsavel": "Maria Oliveira",
  "longitude": "-46.57421",
  "latitude": "-23.55052"
}'

