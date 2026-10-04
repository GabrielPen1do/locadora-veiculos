# LocadoraVeiculos

Sistema de aluguel de veículos em ASP.NET Core com alvo .NET 8, Entity Framework Core 8 e SQL Server Express. A Solution `LocadoraVeiculos.sln` pode ser aberta no Visual Studio 2022.

## Estrutura

- `LocadoraVeiculos.Api/Models`: entidades do banco.
- `LocadoraVeiculos.Api/Data/ApplicationContext.cs`: DbSets, relacionamentos e restrições.
- `LocadoraVeiculos.Api/Dtos`: objetos de entrada e saída.
- `LocadoraVeiculos.Api/Controllers`: endpoints CRUD e filtros.
- `LocadoraVeiculos.Api/Migrations`: histórico de migrations.
- `MODELO_BANCO.md`: descrição do banco.
- `ROTAS.md`: relação dos endpoints.

## Configuração

A connection string `DefaultConnection` em `LocadoraVeiculos.Api/appsettings.json` aponta para `.\SQLEXPRESS` usando autenticação integrada do Windows. Ela pode ser substituída localmente pela variável `ConnectionStrings__DefaultConnection`.

## Etapa 2 - Backend

A API possui controllers para fabricantes, categorias de veículos, veículos, clientes e aluguéis. Cada entidade oferece criação, consulta, atualização e exclusão por meio de DTOs. As validações tratam campos obrigatórios, duplicidades, chaves estrangeiras, datas, quilometragens e valores. Erros inesperados usam Problem Details sem expor informações internas.

O `FiltrosController` contém cinco consultas relacionais. Três usam `INNER JOIN`: veículos por fabricante, veículos por categoria e aluguéis por cliente. Duas usam `LEFT JOIN`: clientes com aluguéis e veículos com histórico. Os resultados das consultas com `LEFT JOIN` incluem registros sem relacionamento.

## Execução

```powershell
dotnet restore
dotnet ef database update --project LocadoraVeiculos.Api --startup-project LocadoraVeiculos.Api
dotnet run --project LocadoraVeiculos.Api
```

A API não aplica migrations automaticamente. O comando de atualização exige uma instância SQL Server Express acessível.

## Etapa 3 - Testes e Documentação

O Swagger/OpenAPI está integrado com Swashbuckle e documenta todos os controllers. Depois de iniciar a API, a interface fica disponível em `http://localhost:5099/swagger/index.html` quando a aplicação é executada nessa porta.

- A descrição dos 30 endpoints está em `DOCUMENTACAO_API.md`.
- O relatório dos 38 testes manuais está em `RELATORIO_TESTES.md`.
- Os 38 prints obtidos pelo Swagger estão em `docs/evidencias/`.
