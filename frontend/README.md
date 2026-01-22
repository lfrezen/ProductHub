# ProductHub - Frontend

Sistema de gerenciamento de produtos desenvolvido em Angular.

## 🚀 Tecnologias

- Angular 19.2.0
- TypeScript 5.7
- Bootstrap 5.3.8
- RxJS 7.8
- JWT Authentication

## ✨ Features do Angular 19 Utilizadas

- ✅ **Standalone Components** - Sem necessidade de NgModules
- ✅ **Control Flow Syntax** - `@if`, `@for`, `@switch`
- ✅ **Functional Guards** - Guards como funções puras
- ✅ **Functional Interceptors** - Interceptors funcionais

## 📋 Funcionalidades

- ✅ Autenticação com JWT
- ✅ CRUD completo de produtos
- ✅ Registro de vendas
- ✅ Atualização de estoque em tempo real
- ✅ Interface responsiva
- ✅ Validação de formulários
- ✅ Tratamento de erros
- ✅ Loading states
- ✅ Optimistic UI updates

## 🛠️ Como Executar

```bash
# Instalar dependências
npm install

# Executar em desenvolvimento
ng serve

# Build para produção
ng build --configuration production
```

## 🌍 Configuração de Ambiente

Edite `src/environments/environment.development.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: "https://localhost:7103/api",
};
```

## 🏗️ Arquitetura

```
src/app/
├── core/          # Serviços singleton, guards, interceptors
├── features/      # Funcionalidades por módulo
└── shared/        # Componentes reutilizáveis
```
