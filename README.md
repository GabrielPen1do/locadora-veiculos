# LocadoraVeiculos

Etapa 1 da modelagem de um sistema de aluguel de veículos. A Solution `LocadoraVeiculos.sln` abre no Visual Studio 2022 e contém uma API ASP.NET Core com alvo .NET 8, Entity Framework Core 8 e SQL Server Express. Esta etapa contém somente o modelo de dados e a migration inicial; não há endpoints de negócio.

## Estrutura

- `LocadoraVeiculos.Api/Models`: Fabricante, CategoriaVeiculo, Veiculo, Cliente e Aluguel.
- `LocadoraVeiculos.Api/Data/ApplicationContext.cs`: DbSets, chaves, relacionamentos e restrições.
- `LocadoraVeiculos.Api/Migrations`: migration `InitialCreate`.
- `MODELO_BANCO.md`: descrição das tabelas e regras.

## Configuração

Instale o SDK e o runtime do .NET 8, SQL Server Express e, para aplicar migrations, `dotnet-ef` compatível com EF Core 8. A connection string `DefaultConnection` em `LocadoraVeiculos.Api/appsettings.json` aponta para `.\SQLEXPRESS` usando autenticação integrada do Windows. Ajuste o servidor por configuração local ou variável de ambiente `ConnectionStrings__DefaultConnection` conforme seu ambiente. Não grave credenciais no repositório.

```powershell
dotnet restore
dotnet build LocadoraVeiculos.sln
dotnet ef database update --project LocadoraVeiculos.Api --startup-project LocadoraVeiculos.Api
```

A criação do banco exige uma instância SQL Server Express acessível. A API não aplica migrations automaticamente.
