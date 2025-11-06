# 🧭 Meeting Hub API

API REST desenvolvida com ASP.NET Core 8, seguindo boas práticas de arquitetura, injeção de dependência, testes automatizados e integração com banco de dados PostgreSQL.

### **1. Padrão CQRS com MediatR**

O desafio propõe explicitamente o uso de CQRS, e essa escolha é adequada
para o domínio de reservas, pois separa **operações de escrita
(Commands)** das **operações de leitura (Queries)**, tornando o código
mais claro, testável e preparado para evoluir.\
A biblioteca **MediatR** foi utilizada para orquestrar os fluxos de
comandos e consultas, eliminando dependências diretas entre camadas e
permitindo que as regras de negócio sejam tratadas de forma
independente.

**Benefícios:** - Isolamento entre leitura e escrita.\
- Redução de acoplamento entre controller e regras de negócio.\
- Facilidade de teste e manutenção.\
- Escalabilidade futura (ex.: separar banco de leitura e escrita).

------------------------------------------------------------------------

### **2. Clean Architecture**

A aplicação segue princípios da **Clean Architecture**, com separação
clara de responsabilidades entre camadas:

-   **Domain** → contém as entidades, regras de negócio e validações
    (ex.: verificação de conflitos de horário).\
-   **Application** → contém os *handlers* CQRS (Commands e Queries) e
    as interfaces de abstração.\
-   **Infrastructure** → implementação concreta do repositório e do
    contexto EF Core.\
-   **Web/API** → camada de apresentação responsável por expor os
    endpoints via Minimal API.

Essa estrutura favorece a **inversão de dependências**, facilita testes
unitários e torna o sistema mais **manutenível e flexível** a mudanças
tecnológicas.

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download)
- ASP.NET Core Web API
- MediatR (CQRS)
- Entity Framework Core
- PostgreSQL
- xUnit + Moq + FluentAssertions (Testes)
- Docker + Docker Compose

---

## 📦 Requisitos

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [PostgreSQL Client](https://www.postgresql.org/download/) (opcional)

---

## 🔧 Configuração do Ambiente

### 1. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/seu-repo.git
cd seu-repo
```
---

## 🐳 Docker Compose

A aplicação utiliza Docker Compose para subir a infraestrutura de desenvolvimento local (banco de dados PostgreSQL).

### 📂 Arquivo utilizado

O arquivo `compose.yaml` define os serviços necessários.

### 📌 Serviços definidos:

- `db_postgres`: Banco de dados PostgreSQL 16
    - Porta local: `5432`
    - Volume persistente: `porter_postgres_data`
    - Variáveis de ambiente:
        - `POSTGRES_DB=meeting`
        - `POSTGRES_USER=postgres`
        - `POSTGRES_PASSWORD=password`

- `pgadmin`: (opcional, se habilitado)
    - Interface para administrar o PostgreSQL via browser

---

### ▶️ Subir os serviços


```bash
docker compose -f compose.yaml up -d
```
---

## ▶️ Executar a API

Após subir o banco com o Docker Compose, rode a API com:

```bash
dotnet clean
dotnet build
dotnet run --project src/Meeting.Hub.Web.Api/Meeting.Hub.Web.Api.csproj
```

A API será iniciada em:

- 🔗 [`http://localhost:5130/swagger`](http://localhost:5130/swagger)

> Esse endereço é configurado via `launchSettings.json`.

Caso queira verificar se o ambiente está como **Development**, a variável `ASPNETCORE_ENVIRONMENT` está definida automaticamente no perfil do projeto.

---
