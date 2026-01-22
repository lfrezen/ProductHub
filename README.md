# 📦 ProductHub

Sistema completo de gerenciamento de produtos de supermercado, desenvolvido com **.NET 8** e **Angular 19**, demonstrando aplicação prática de arquitetura limpa, boas práticas de desenvolvimento e tecnologias modernas.

---

## 📋 Índice

- [Visão Geral](#-visão-geral)
- [Funcionalidades](#-funcionalidades)
- [Tecnologias](#-tecnologias)
- [Arquitetura](#-arquitetura)
- [Pré-requisitos](#-pré-requisitos)
- [Como Executar](#-como-executar)
- [Testes](#-testes)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Decisões Técnicas](#-decisões-técnicas)
- [Segurança](#-segurança)
- [Melhorias Futuras](#-melhorias-futuras)
- [Autor](#-autor)

---

## 🎯 Visão Geral

O **ProductHub** é uma aplicação full-stack para gerenciamento de produtos de supermercado, incluindo controle de estoque e registro de vendas. O sistema foi desenvolvido seguindo princípios de **Clean Architecture**, **SOLID**, e **Domain-Driven Design (DDD)**.

### Diferenciais

- ✅ Autenticação JWT com proteção de rotas
- ✅ Background service para monitoramento de estoque
- ✅ Arquitetura escalável e testável
- ✅ Interface moderna e responsiva
- ✅ Cobertura de testes unitários (32 testes)
- ✅ API RESTful documentada com Swagger

---

## 🚀 Funcionalidades

### Autenticação

- Login com JWT
- Proteção de rotas (Guards)
- Interceptor HTTP para token automático
- Logout com limpeza de sessão

### Gerenciamento de Produtos

- ✅ **Criar** produtos (nome, preço, quantidade)
- ✅ **Listar** produtos com filtros e indicadores
- ✅ **Editar** produtos existentes
- ✅ **Excluir** produtos com confirmação
- ✅ **Registrar vendas** com atualização de estoque em tempo real

### Monitoramento Automático

- 🤖 **Background Service** que verifica produtos sem vendas
- ⏰ Executa a cada 24h (configurável via appsettings.json)
- 📦 Marca produtos como "fora de estoque" após 10 dias sem vendas

### Interface

- 📱 Responsivo (mobile-first)
- 🎨 Interface moderna com Bootstrap 5
- ⚡ Feedback instantâneo (loading, erros, sucesso)
- 🔄 Atualização otimista da UI

---

## 🛠️ Tecnologias

### Backend (.NET 8)

- **.NET 8.0** - Framework principal
- **C# 12.0** - Linguagem
- **Entity Framework Core 8.0** - ORM
- **SQL Server LocalDB** - Banco de dados
- **JWT Bearer** - Autenticação
- **BCrypt.Net** - Hash de senhas
- **Swagger/OpenAPI** - Documentação da API
- **xUnit + FluentAssertions + Moq** - Testes unitários

### Frontend (Angular 19)

- **Angular 19.2.0** - Framework
- **TypeScript 5.7.2** - Linguagem
- **RxJS 7.8.0** - Programação reativa
- **Bootstrap 5.3.8** - UI Framework
- **Bootstrap Icons 1.13.1** - Ícones
- **jwt-decode 4.0.0** - Decodificação JWT

---

## 🏗️ Arquitetura

### Backend - Clean Architecture

```
ProductHub/
├── backend/
│   ├── src/
│   │   ├── ProductHub.Api/           # Controllers, Middleware, Configuration
│   │   ├── ProductHub.Application/   # Use Cases, DTOs, Services
│   │   ├── ProductHub.Domain/        # Entities, Business Rules
│   │   └── ProductHub.Infrastructure/# Repositories, DbContext, Background Services
│   └── tests/
│       ├── ProductHub.Domain.Tests/
│       ├── ProductHub.Application.Tests/
│       └── ProductHub.Infrastructure.Tests/
```

#### Camadas

**1. Domain (Núcleo)**

- Entidades com lógica de negócio encapsulada
- Sem dependências externas
- Regras de validação no construtor

**2. Application**

- Use Cases (Commands/Queries)
- Services com lógica de aplicação
- Interfaces (Abstractions)

**3. Infrastructure**

- Implementação de repositórios
- DbContext do Entity Framework
- Background Services
- Integrações externas

**4. API**

- Controllers (thin controllers)
- Middleware de exceções
- Configuração de DI
- Swagger/OpenAPI

### Frontend - Component Architecture

```
frontend/src/app/
├── core/
│   ├── guards/          # Proteção de rotas
│   ├── interceptors/    # HTTP interceptors
│   ├── models/          # Interfaces TypeScript
│   └── services/        # Serviços singleton
├── features/
│   ├── auth/           # Login
│   ├── layout/         # Navbar
│   └── products/       # CRUD de produtos
└── shared/             # Componentes reutilizáveis
```

---

## 📦 Pré-requisitos

### Backend

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (incluído no Visual Studio)

### Frontend

- [Node.js](https://nodejs.org/) >= 18.19.0
- npm >= 9.0.0

---

## ⚙️ Como Executar

### 1️⃣ Clone o repositório

```bash
git clone https://github.com/lfrezen/ProductHub.git
cd ProductHub
```

### 2️⃣ Backend (.NET)

```bash
cd backend

# Restaurar dependências
dotnet restore

# Criar banco de dados
dotnet ef database update --startup-project src/ProductHub.Api --project src/ProductHub.Infrastructure

# Executar a API
cd src/ProductHub.Api
dotnet run
```

**API:** `https://localhost:7103`  
**Swagger:** `https://localhost:7103/swagger`

### 3️⃣ Frontend (Angular)

```bash
# Em outro terminal
cd frontend

# Instalar dependências
npm install

# Executar aplicação
ng serve
```

**Aplicação:** `http://localhost:4200`

### 4️⃣ Credenciais Padrão

```
Usuário: admin
Senha: admin123
```

---

## 🧪 Testes

### Backend

```bash
cd backend
dotnet test
```

**Resultados:**

- ✅ Domain: 20 testes passando
- ✅ Application: 7 testes passando
- ✅ Infrastructure: 5 testes passando
- ✅ **Total: 32 testes | 0 falhas**

---

## 💡 Decisões Técnicas

### Por que Clean Architecture?

- ✅ **Testabilidade** - Domain isolado, fácil de testar
- ✅ **Manutenibilidade** - Baixo acoplamento entre camadas
- ✅ **Escalabilidade** - Fácil adicionar novos recursos

### Por que DDD?

- ✅ **Encapsulamento** - Lógica dentro das entidades
- ✅ **Invariantes** - Regras de negócio sempre válidas
- ✅ **Linguagem Ubíqua** - Código reflete o domínio

### Por que JWT?

- ✅ **Stateless** - Não precisa guardar sessões no servidor
- ✅ **Escalável** - Funciona em ambientes distribuídos

### Por que Angular 19 com Standalone?

- ✅ **Simplicidade** - Sem NgModules complexos
- ✅ **Performance** - Tree-shaking melhorado
- ✅ **Modern Features** - @if, @for (Control Flow Syntax)

---

## 🔐 Segurança

### Backend

- ✅ Senhas hasheadas com BCrypt
- ✅ JWT com expiração configurável
- ✅ Validações em múltiplas camadas
- ✅ Exception handling global
- ✅ CORS configurado

### Frontend

- ✅ Guards protegendo rotas privadas
- ✅ Interceptor adicionando token automaticamente
- ✅ Logout automático em 401
- ✅ XSS protection (Angular automático)

---

## 🚀 Melhorias Futuras

### Backend

- [ ] Refresh Token
- [ ] Rate Limiting
- [ ] Redis para cache
- [ ] Logs estruturados (Serilog)
- [ ] Docker/Kubernetes

### Frontend

- [ ] Paginação na lista
- [ ] Filtros e busca avançada
- [ ] Gráficos de vendas
- [ ] Export para Excel/PDF
- [ ] PWA (Progressive Web App)

---

## 👨‍💻 Autor

**Lucian Fiocello de Rezende**

- GitHub: [@lfrezen](https://github.com/lfrezen)
- LinkedIn: [Lucian Rezende](https://linkedin.com/in/lucianrezende)

---

## 📄 Licença

Este projeto está sob a licença MIT.

---

**⭐ Se este projeto foi útil, considere dar uma estrela!**
