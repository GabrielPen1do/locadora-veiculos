# Relatório de Testes - API Locadora de Veículos

## Objetivo

Validar os endpoints REST da aplicação por meio de chamadas reais à API documentada pelo Swagger, conferindo operações CRUD, filtros, validações e códigos HTTP.

## Ambiente

- ASP.NET Core 8
- Entity Framework Core 8
- SQL Server Express
- Swagger/OpenAPI com Swashbuckle
- URL utilizada: http://localhost:5099
- Data da execução: 04/10/2026

## Resumo

- 35 testes executados.
- 25 operações CRUD testadas.
- 5 filtros testados.
- 5 cenários de validação e erro testados.
- 35 resultados com os códigos HTTP esperados.
- Evidência geral do Swagger: docs/evidencias/00-swagger.png.
- Resultados estruturados: docs/resultados-testes.json.

## Testes realizados

### Teste 01 - Cadastrar fabricante

**Método:** POST
**Endpoint:** /api/fabricantes
**Objetivo:** Cadastrar fabricante.
**Dados enviados:** {"nome":"Teste Fabricante 134734"}
**Status esperado:** 201 Created
**Status obtido:** 201 Created
**Retorno obtido:**

```json
{"id":13,"nome":"Teste Fabricante 134734"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/01-teste-api.png

### Teste 02 - Listar fabricantes

**Método:** GET
**Endpoint:** /api/fabricantes
**Objetivo:** Listar fabricantes.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"id":13,"nome":"Teste Fabricante 134734"}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/02-teste-api.png

### Teste 03 - Consultar fabricante por ID

**Método:** GET
**Endpoint:** /api/fabricantes/13
**Objetivo:** Consultar fabricante por ID.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
{"id":13,"nome":"Teste Fabricante 134734"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/03-teste-api.png

### Teste 04 - Atualizar fabricante

**Método:** PUT
**Endpoint:** /api/fabricantes/13
**Objetivo:** Atualizar fabricante.
**Dados enviados:** {"nome":"Teste Fabricante Atualizado 134734"}
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/04-teste-api.png

### Teste 05 - Excluir fabricante

**Método:** DELETE
**Endpoint:** /api/fabricantes/13
**Objetivo:** Excluir fabricante.
**Dados enviados:** Nenhum.
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/05-teste-api.png

### Teste 06 - Cadastrar categoria

**Método:** POST
**Endpoint:** /api/categoriasveiculos
**Objetivo:** Cadastrar categoria.
**Dados enviados:** {"descricao":"Categoria criada pelos testes da Etapa 3","nome":"Teste Categoria 134734"}
**Status esperado:** 201 Created
**Status obtido:** 201 Created
**Retorno obtido:**

```json
{"id":12,"nome":"Teste Categoria 134734","descricao":"Categoria criada pelos testes da Etapa 3"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/06-teste-api.png

### Teste 07 - Listar categorias

**Método:** GET
**Endpoint:** /api/categoriasveiculos
**Objetivo:** Listar categorias.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"id":12,"nome":"Teste Categoria 134734","descricao":"Categoria criada pelos testes da Etapa 3"}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/07-teste-api.png

### Teste 08 - Consultar categoria por ID

**Método:** GET
**Endpoint:** /api/categoriasveiculos/12
**Objetivo:** Consultar categoria por ID.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
{"id":12,"nome":"Teste Categoria 134734","descricao":"Categoria criada pelos testes da Etapa 3"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/08-teste-api.png

### Teste 09 - Atualizar categoria

**Método:** PUT
**Endpoint:** /api/categoriasveiculos/12
**Objetivo:** Atualizar categoria.
**Dados enviados:** {"descricao":"Categoria atualizada","nome":"Teste Categoria Atualizada 134734"}
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/09-teste-api.png

### Teste 10 - Excluir categoria

**Método:** DELETE
**Endpoint:** /api/categoriasveiculos/12
**Objetivo:** Excluir categoria.
**Dados enviados:** Nenhum.
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/10-teste-api.png

### Teste 11 - Cadastrar cliente

**Método:** POST
**Endpoint:** /api/clientes
**Objetivo:** Cadastrar cliente.
**Dados enviados:** {"email":"cliente.134734@exemplo.com","cpf":"71347340000","nome":"Cliente Teste","telefone":"31999990000"}
**Status esperado:** 201 Created
**Status obtido:** 201 Created
**Retorno obtido:**

```json
{"id":19,"nome":"Cliente Teste","cpf":"71347340000","email":"cliente.134734@exemplo.com","telefone":"31999990000"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/11-teste-api.png

### Teste 12 - Listar clientes

**Método:** GET
**Endpoint:** /api/clientes
**Objetivo:** Listar clientes.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"id":19,"nome":"Cliente Teste","cpf":"71347340000","email":"cliente.134734@exemplo.com","telefone":"31999990000"}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/12-teste-api.png

### Teste 13 - Consultar cliente por ID

**Método:** GET
**Endpoint:** /api/clientes/19
**Objetivo:** Consultar cliente por ID.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
{"id":19,"nome":"Cliente Teste","cpf":"71347340000","email":"cliente.134734@exemplo.com","telefone":"31999990000"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/13-teste-api.png

### Teste 14 - Atualizar cliente

**Método:** PUT
**Endpoint:** /api/clientes/19
**Objetivo:** Atualizar cliente.
**Dados enviados:** {"email":"cliente.134734@exemplo.com","cpf":"71347340000","nome":"Cliente Teste Atualizado","telefone":"31999991111"}
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/14-teste-api.png

### Teste 15 - Excluir cliente

**Método:** DELETE
**Endpoint:** /api/clientes/19
**Objetivo:** Excluir cliente.
**Dados enviados:** Nenhum.
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/15-teste-api.png

### Teste 16 - Cadastrar veículo

**Método:** POST
**Endpoint:** /api/veiculos
**Objetivo:** Cadastrar veículo.
**Dados enviados:** {"valorDiaria":180.5,"quilometragem":100,"modelo":"Pulse Teste","categoriaVeiculoId":13,"placa":"T134734","ano":2025,"fabricanteId":14}
**Status esperado:** 201 Created
**Status obtido:** 201 Created
**Retorno obtido:**

```json
{"id":15,"modelo":"Pulse Teste","placa":"T134734","ano":2025,"quilometragem":100,"valorDiaria":180.50,"fabricanteId":14,"fabricante":"Fiat Teste 134734","categoriaVeiculoId":13,"categoria":"SUV Teste 134734"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/16-teste-api.png

### Teste 17 - Listar veículos

**Método:** GET
**Endpoint:** /api/veiculos
**Objetivo:** Listar veículos.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"id":15,"modelo":"Pulse Teste","placa":"T134734","ano":2025,"quilometragem":100,"valorDiaria":180.50,"fabricanteId":14,"fabricante":"Fiat Teste 134734","categoriaVeiculoId":13,"categoria":"SUV Teste 134734"}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/17-teste-api.png

### Teste 18 - Consultar veículo por ID

**Método:** GET
**Endpoint:** /api/veiculos/15
**Objetivo:** Consultar veículo por ID.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
{"id":15,"modelo":"Pulse Teste","placa":"T134734","ano":2025,"quilometragem":100,"valorDiaria":180.50,"fabricanteId":14,"fabricante":"Fiat Teste 134734","categoriaVeiculoId":13,"categoria":"SUV Teste 134734"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/18-teste-api.png

### Teste 19 - Atualizar veículo

**Método:** PUT
**Endpoint:** /api/veiculos/15
**Objetivo:** Atualizar veículo.
**Dados enviados:** {"valorDiaria":190,"quilometragem":250,"modelo":"Pulse Teste Atualizado","categoriaVeiculoId":13,"placa":"T134734","ano":2025,"fabricanteId":14}
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/19-teste-api.png

### Teste 20 - Excluir veículo

**Método:** DELETE
**Endpoint:** /api/veiculos/15
**Objetivo:** Excluir veículo.
**Dados enviados:** Nenhum.
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/20-teste-api.png

### Teste 21 - Cadastrar aluguel

**Método:** POST
**Endpoint:** /api/alugueis
**Objetivo:** Cadastrar aluguel.
**Dados enviados:** {"valorTotal":null,"clienteId":20,"quilometragemInicial":1000,"dataFimPrevista":"2026-10-07T10:00:00","valorDiaria":150,"dataInicio":"2026-10-04T10:00:00","dataDevolucao":null,"quilometragemFinal":null,"veiculoId":16}
**Status esperado:** 201 Created
**Status obtido:** 201 Created
**Retorno obtido:**

```json
{"id":2,"clienteId":20,"cliente":"Cliente Aluguel Teste","veiculoId":16,"veiculo":"Argo Teste","placa":"A134734","dataInicio":"2026-10-04T10:00:00","dataFimPrevista":"2026-10-07T10:00:00","dataDevolucao":null,"quilometragemInicial":1000,"quilometragemFinal":null,"valorDiaria":150.00,"valorTotal":null}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/21-teste-api.png

### Teste 22 - Listar aluguéis

**Método:** GET
**Endpoint:** /api/alugueis
**Objetivo:** Listar aluguéis.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"id":2,"clienteId":20,"cliente":"Cliente Aluguel Teste","veiculoId":16,"veiculo":"Argo Teste","placa":"A134734","dataInicio":"2026-10-04T10:00:00","dataFimPrevista":"2026-10-07T10:00:00","dataDevolucao":null,"quilometragemInicial":1000,"quilometragemFinal":null,"valorDiaria":150.00,"valorTotal":null}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/22-teste-api.png

### Teste 23 - Consultar aluguel por ID

**Método:** GET
**Endpoint:** /api/alugueis/2
**Objetivo:** Consultar aluguel por ID.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
{"id":2,"clienteId":20,"cliente":"Cliente Aluguel Teste","veiculoId":16,"veiculo":"Argo Teste","placa":"A134734","dataInicio":"2026-10-04T10:00:00","dataFimPrevista":"2026-10-07T10:00:00","dataDevolucao":null,"quilometragemInicial":1000,"quilometragemFinal":null,"valorDiaria":150.00,"valorTotal":null}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/23-teste-api.png

### Teste 24 - Atualizar aluguel

**Método:** PUT
**Endpoint:** /api/alugueis/2
**Objetivo:** Atualizar aluguel.
**Dados enviados:** {"valorTotal":450,"clienteId":20,"quilometragemInicial":1000,"dataFimPrevista":"2026-10-07T10:00:00","valorDiaria":150,"dataInicio":"2026-10-04T10:00:00","dataDevolucao":"2026-10-06T15:00:00","quilometragemFinal":1280,"veiculoId":16}
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/24-teste-api.png

### Teste 25 - Filtrar veículos por fabricante

**Método:** GET
**Endpoint:** /api/filtros/veiculos-por-fabricante?fabricante=Fiat%20Teste%20134734
**Objetivo:** Filtrar veículos por fabricante.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"veiculoId":16,"modelo":"Argo Teste","placa":"A134734","ano":2024,"quilometragem":1000,"fabricante":"Fiat Teste 134734"},{"veiculoId":17,"modelo":"Toro Sem Historico","placa":"H134734","ano":2023,"quilometragem":2000,"fabricante":"Fiat Teste 134734"}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/25-teste-api.png

### Teste 26 - Filtrar veículos por categoria

**Método:** GET
**Endpoint:** /api/filtros/veiculos-por-categoria?categoria=SUV%20Teste%20134734
**Objetivo:** Filtrar veículos por categoria.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"veiculoId":16,"modelo":"Argo Teste","placa":"A134734","ano":2024,"quilometragem":1000,"categoria":"SUV Teste 134734"},{"veiculoId":17,"modelo":"Toro Sem Historico","placa":"H134734","ano":2023,"quilometragem":2000,"categoria":"SUV Teste 134734"}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/26-teste-api.png

### Teste 27 - Filtrar aluguéis por cliente

**Método:** GET
**Endpoint:** /api/filtros/alugueis-por-cliente?cpf=81347340000
**Objetivo:** Filtrar aluguéis por cliente.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"aluguelId":2,"cliente":"Cliente Aluguel Teste","cpf":"81347340000","veiculo":"Argo Teste","placa":"A134734","dataInicio":"2026-10-04T10:00:00","dataFimPrevista":"2026-10-07T10:00:00","dataDevolucao":"2026-10-06T15:00:00","valorDiaria":150.00,"valorTotal":450.00}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/27-teste-api.png

### Teste 28 - Listar clientes com ou sem aluguéis

**Método:** GET
**Endpoint:** /api/filtros/clientes-com-alugueis
**Objetivo:** Listar clientes com ou sem aluguéis.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"clienteId":20,"nome":"Cliente Aluguel Teste","cpf":"81347340000","aluguelId":2,"dataInicio":"2026-10-04T10:00:00","dataFimPrevista":"2026-10-07T10:00:00"},{"clienteId":21,"nome":"Cliente Sem Aluguel","cpf":"91347340000","aluguelId":null,"dataInicio":null,"dataFimPrevista":null}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/28-teste-api.png

### Teste 29 - Listar veículos com ou sem histórico

**Método:** GET
**Endpoint:** /api/filtros/veiculos-com-historico
**Objetivo:** Listar veículos com ou sem histórico.
**Dados enviados:** Nenhum.
**Status esperado:** 200 OK
**Status obtido:** 200 OK
**Retorno obtido:**

```json
[{"veiculoId":16,"modelo":"Argo Teste","placa":"A134734","quilometragem":1000,"aluguelId":2,"dataInicio":"2026-10-04T10:00:00","dataDevolucao":"2026-10-06T15:00:00"},{"veiculoId":17,"modelo":"Toro Sem Historico","placa":"H134734","quilometragem":2000,"aluguelId":null,"dataInicio":null,"dataDevolucao":null}]
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/29-teste-api.png

### Teste 30 - Excluir aluguel

**Método:** DELETE
**Endpoint:** /api/alugueis/2
**Objetivo:** Excluir aluguel.
**Dados enviados:** Nenhum.
**Status esperado:** 204 No Content
**Status obtido:** 204 No Content
**Retorno obtido:**

```json
Sem corpo de resposta.
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/30-teste-api.png

### Teste 31 - Consultar ID inexistente

**Método:** GET
**Endpoint:** /api/clientes/99999999
**Objetivo:** Consultar ID inexistente.
**Dados enviados:** Nenhum.
**Status esperado:** 404 Not Found
**Status obtido:** 404 Not Found
**Retorno obtido:**

```json
{"type":"https://tools.ietf.org/html/rfc9110#section-15.5.5","title":"Not Found","status":404,"traceId":"00-3ec903269c5a024087b3ebd157595fc5-f76d94851b1e07c2-00"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/31-teste-api.png

### Teste 32 - Validar campo obrigatório

**Método:** POST
**Endpoint:** /api/fabricantes
**Objetivo:** Validar campo obrigatório.
**Dados enviados:** {"nome":" "}
**Status esperado:** 400 Bad Request
**Status obtido:** 400 Bad Request
**Retorno obtido:**

```json
{"type":"https://tools.ietf.org/html/rfc9110#section-15.5.1","title":"One or more validation errors occurred.","status":400,"errors":{"Nome":["The Nome field is required."]},"traceId":"00-ce49a67769c706c99942a80ba51e05aa-5e309c0601fa8c78-00"}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/32-teste-api.png

### Teste 33 - Validar placa duplicada

**Método:** POST
**Endpoint:** /api/veiculos
**Objetivo:** Validar placa duplicada.
**Dados enviados:** {"valorDiaria":100,"quilometragem":0,"modelo":"Duplicado","categoriaVeiculoId":13,"placa":"A134734","ano":2024,"fabricanteId":14}
**Status esperado:** 409 Conflict
**Status obtido:** 409 Conflict
**Retorno obtido:**

```json
{"title":"Já existe um veículo com essa placa.","status":409}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/33-teste-api.png

### Teste 34 - Validar CPF duplicado

**Método:** POST
**Endpoint:** /api/clientes
**Objetivo:** Validar CPF duplicado.
**Dados enviados:** {"email":"outro.134734@exemplo.com","cpf":"81347340000","nome":"Duplicado","telefone":"31900000000"}
**Status esperado:** 409 Conflict
**Status obtido:** 409 Conflict
**Retorno obtido:**

```json
{"title":"Já existe um cliente com esse CPF.","status":409}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/34-teste-api.png

### Teste 35 - Validar chave estrangeira

**Método:** POST
**Endpoint:** /api/veiculos
**Objetivo:** Validar chave estrangeira.
**Dados enviados:** {"valorDiaria":100,"quilometragem":0,"modelo":"FK Invalida","categoriaVeiculoId":13,"placa":"F134734","ano":2024,"fabricanteId":99999999}
**Status esperado:** 400 Bad Request
**Status obtido:** 400 Bad Request
**Retorno obtido:**

```json
{"title":"FabricanteId inválido.","status":400}
```

**Resultado:** Sucesso.
**Evidência:** docs/evidencias/35-teste-api.png

## Conclusão

Todos os 30 endpoints existentes foram exercitados com sucesso. Os cinco testes adicionais confirmaram respostas 400, 404 e 409 para entradas inválidas, recurso inexistente, duplicidades e chave estrangeira inválida.
