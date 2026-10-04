# Relatório de Testes - API Locadora de Veículos

## Objetivo

Verificar pelo Swagger o funcionamento dos endpoints CRUD, filtros e validações da API.

## Ambiente

- ASP.NET Core 8
- Entity Framework Core 8
- SQL Server Express
- Swagger/OpenAPI com Swashbuckle
- Endereço utilizado: `http://localhost:5099/swagger/index.html`
- Data dos testes: 04/10/2026

## Procedimento

Os testes foram executados manualmente pela opção **Try it out** do Swagger. Para cada chamada foram conferidos a URL, o corpo enviado, o código HTTP e o corpo da resposta. Os registros relacionados foram cadastrados na ordem Fabricante, Categoria, Cliente, Veículo e Aluguel. As exclusões foram feitas na ordem inversa para respeitar as chaves estrangeiras.

## Resultados

| Nº | Método e endpoint | Dados ou objetivo | Status obtido | Retorno obtido | Evidência |
| --- | --- | --- | --- | --- | --- |
| 01 | `POST /api/Fabricantes` | Nome: Fiat Teste Manual | `201 Created` | Fabricante cadastrado com ID | [print](docs/evidencias/01-fabricante-post.png) |
| 02 | `GET /api/Fabricantes` | Listar fabricantes | `200 OK` | Lista contendo o fabricante cadastrado | [print](docs/evidencias/02-fabricante-get-all.png) |
| 03 | `GET /api/Fabricantes/{id}` | Consultar fabricante pelo ID | `200 OK` | Dados do fabricante | [print](docs/evidencias/03-fabricante-get-id.png) |
| 04 | `PUT /api/Fabricantes/{id}` | Alterar nome para Fiat Atualizada | `204 No Content` | Atualização concluída | [print](docs/evidencias/04-fabricante-put.png) |
| 05 | `POST /api/CategoriasVeiculos` | Cadastrar categoria SUV | `201 Created` | Categoria cadastrada com ID | [print](docs/evidencias/05-categoria-post.png) |
| 06 | `GET /api/CategoriasVeiculos` | Listar categorias | `200 OK` | Lista contendo a categoria cadastrada | [print](docs/evidencias/06-categoria-get-all.png) |
| 07 | `GET /api/CategoriasVeiculos/{id}` | Consultar categoria pelo ID | `200 OK` | Dados da categoria | [print](docs/evidencias/07-categoria-get-id.png) |
| 08 | `PUT /api/CategoriasVeiculos/{id}` | Alterar categoria para SUV Premium | `204 No Content` | Atualização concluída | [print](docs/evidencias/08-categoria-put.png) |
| 09 | `POST /api/Clientes` | Cadastrar cliente fictício | `201 Created` | Cliente cadastrado com ID | [print](docs/evidencias/09-cliente-post.png) |
| 10 | `GET /api/Clientes` | Listar clientes | `200 OK` | Lista contendo o cliente cadastrado | [print](docs/evidencias/10-cliente-get-all.png) |
| 11 | `GET /api/Clientes/{id}` | Consultar cliente pelo ID | `200 OK` | Dados do cliente | [print](docs/evidencias/11-cliente-get-id.png) |
| 12 | `PUT /api/Clientes/{id}` | Atualizar nome, email e telefone | `204 No Content` | Atualização concluída | [print](docs/evidencias/12-cliente-put.png) |
| 13 | `POST /api/Veiculos` | Cadastrar Pulse com fabricante e categoria existentes | `201 Created` | Veículo cadastrado com ID e relacionamentos | [print](docs/evidencias/13-veiculo-post.png) |
| 14 | `GET /api/Veiculos` | Listar veículos | `200 OK` | Lista com veículo, fabricante e categoria | [print](docs/evidencias/14-veiculo-get-all.png) |
| 15 | `GET /api/Veiculos/{id}` | Consultar veículo pelo ID | `200 OK` | Dados completos do veículo | [print](docs/evidencias/15-veiculo-get-id.png) |
| 16 | `PUT /api/Veiculos/{id}` | Atualizar modelo, quilometragem e diária | `204 No Content` | Atualização concluída | [print](docs/evidencias/16-veiculo-put.png) |
| 17 | `POST /api/Alugueis` | Cadastrar aluguel para cliente e veículo existentes | `201 Created` | Aluguel cadastrado com ID | [print](docs/evidencias/17-aluguel-post.png) |
| 18 | `GET /api/Alugueis` | Listar aluguéis | `200 OK` | Lista com cliente, veículo, datas e valores | [print](docs/evidencias/18-aluguel-get-all.png) |
| 19 | `GET /api/Alugueis/{id}` | Consultar aluguel pelo ID | `200 OK` | Dados completos do aluguel | [print](docs/evidencias/19-aluguel-get-id.png) |
| 20 | `PUT /api/Alugueis/{id}` | Registrar devolução, quilometragem final e valor total | `204 No Content` | Atualização concluída | [print](docs/evidencias/20-aluguel-put.png) |
| 21 | `GET /api/Filtros/veiculos-por-fabricante?fabricante=Fiat` | Filtrar por fabricante | `200 OK` | Veículo e fabricante retornados | [print](docs/evidencias/21-filtro-fabricante.png) |
| 22 | `GET /api/Filtros/veiculos-por-categoria?categoria=SUV` | Filtrar por categoria | `200 OK` | Veículo e categoria retornados | [print](docs/evidencias/22-filtro-categoria.png) |
| 23 | `GET /api/Filtros/alugueis-por-cliente?cpf=...` | Filtrar aluguéis pelo CPF | `200 OK` | Aluguel, cliente e veículo retornados | [print](docs/evidencias/23-filtro-alugueis-clientes.png) |
| 24 | `GET /api/Filtros/clientes-com-alugueis` | Consultar clientes com LEFT JOIN | `200 OK` | Clientes com os respectivos aluguéis | [print](docs/evidencias/24-filtro-clientes-alugueis.png) |
| 25 | `GET /api/Filtros/veiculos-com-historico` | Consultar veículos com LEFT JOIN | `200 OK` | Veículos com o histórico de aluguéis | [print](docs/evidencias/25-filtro-veiculos-historico.png) |
| 26 | `GET /api/Clientes/99999999` | Consultar ID inexistente | `404 Not Found` | Recurso não encontrado | [print](docs/evidencias/26-erro-id-inexistente.png) |
| 27 | `POST /api/Fabricantes` | Enviar nome vazio | `400 Bad Request` | Erro de campo obrigatório | [print](docs/evidencias/27-erro-campo-obrigatorio.png) |
| 28 | `POST /api/Veiculos` | Enviar placa já cadastrada | `409 Conflict` | Conflito de placa duplicada | [print](docs/evidencias/28-erro-placa-duplicada.png) |
| 29 | `POST /api/Clientes` | Enviar CPF já cadastrado | `409 Conflict` | Conflito de CPF duplicado | [print](docs/evidencias/29-erro-cpf-duplicado.png) |
| 30 | `POST /api/Clientes` | Enviar email já cadastrado | `409 Conflict` | Conflito de email duplicado | [print](docs/evidencias/30-erro-email-duplicado.png) |
| 31 | `POST /api/Veiculos` | Informar fabricante inexistente | `400 Bad Request` | FabricanteId inválido | [print](docs/evidencias/31-erro-fabricante-inexistente.png) |
| 32 | `POST /api/Veiculos` | Informar categoria inexistente | `400 Bad Request` | CategoriaVeiculoId inválido | [print](docs/evidencias/32-erro-categoria-inexistente.png) |
| 33 | `POST /api/Alugueis` | Informar fim previsto anterior ao início | `400 Bad Request` | Erro de validação das datas | [print](docs/evidencias/33-erro-data-aluguel.png) |
| 34 | `DELETE /api/Alugueis/{id}` | Excluir aluguel | `204 No Content` | Exclusão concluída | [print](docs/evidencias/34-aluguel-delete.png) |
| 35 | `DELETE /api/Veiculos/{id}` | Excluir veículo após o aluguel | `204 No Content` | Exclusão concluída | [print](docs/evidencias/35-veiculo-delete.png) |
| 36 | `DELETE /api/Clientes/{id}` | Excluir cliente após o aluguel | `204 No Content` | Exclusão concluída | [print](docs/evidencias/36-cliente-delete.png) |
| 37 | `DELETE /api/Fabricantes/{id}` | Excluir fabricante após o veículo | `204 No Content` | Exclusão concluída | [print](docs/evidencias/37-fabricante-delete.png) |
| 38 | `DELETE /api/CategoriasVeiculos/{id}` | Excluir categoria após o veículo | `204 No Content` | Exclusão concluída | [print](docs/evidencias/38-categoria-delete.png) |

## Conclusão

Os 30 endpoints da API foram testados pelo Swagger. As operações CRUD e os cinco filtros retornaram os códigos esperados. Os testes adicionais confirmaram o tratamento de campos obrigatórios, registros inexistentes, duplicidades, chaves estrangeiras e datas inválidas.
