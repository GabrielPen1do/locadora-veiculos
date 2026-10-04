# Documentação da API Locadora de Veículos

Base local: `http://localhost:5099`
Swagger UI: `http://localhost:5099/swagger/index.html`
Formato de dados: JSON

## Fabricantes

| Método | Rota | Descrição | Parâmetros | Corpo | Respostas e retorno |
| --- | --- | --- | --- | --- | --- |
| GET | `/api/fabricantes` | Lista fabricantes | Nenhum | Nenhum | `200`: lista de fabricantes; `500`: erro interno |
| GET | `/api/fabricantes/{id}` | Consulta fabricante por ID | Rota: `id` inteiro | Nenhum | `200`: fabricante; `404`: não encontrado; `500`: erro interno |
| POST | `/api/fabricantes` | Cadastra fabricante | Nenhum | `nome` obrigatório, até 100 caracteres | `201`: fabricante criado; `400`: dados inválidos; `409`: nome duplicado; `500`: erro interno |
| PUT | `/api/fabricantes/{id}` | Atualiza fabricante | Rota: `id` inteiro | `nome` obrigatório, até 100 caracteres | `204`: atualizado; `400`: dados inválidos; `404`: não encontrado; `409`: nome duplicado; `500`: erro interno |
| DELETE | `/api/fabricantes/{id}` | Exclui fabricante | Rota: `id` inteiro | Nenhum | `204`: excluído; `404`: não encontrado; `409`: possui veículos; `500`: erro interno |

## Categorias de veículos

| Método | Rota | Descrição | Parâmetros | Corpo | Respostas e retorno |
| --- | --- | --- | --- | --- | --- |
| GET | `/api/categoriasveiculos` | Lista categorias | Nenhum | Nenhum | `200`: lista de categorias; `500`: erro interno |
| GET | `/api/categoriasveiculos/{id}` | Consulta categoria por ID | Rota: `id` inteiro | Nenhum | `200`: categoria; `404`: não encontrada; `500`: erro interno |
| POST | `/api/categoriasveiculos` | Cadastra categoria | Nenhum | `nome` obrigatório e `descricao` opcional | `201`: categoria criada; `400`: dados inválidos; `409`: nome duplicado; `500`: erro interno |
| PUT | `/api/categoriasveiculos/{id}` | Atualiza categoria | Rota: `id` inteiro | `nome` obrigatório e `descricao` opcional | `204`: atualizada; `400`: dados inválidos; `404`: não encontrada; `409`: nome duplicado; `500`: erro interno |
| DELETE | `/api/categoriasveiculos/{id}` | Exclui categoria | Rota: `id` inteiro | Nenhum | `204`: excluída; `404`: não encontrada; `409`: possui veículos; `500`: erro interno |

## Veículos

| Método | Rota | Descrição | Parâmetros | Corpo | Respostas e retorno |
| --- | --- | --- | --- | --- | --- |
| GET | `/api/veiculos` | Lista veículos | Nenhum | Nenhum | `200`: lista com fabricante e categoria; `500`: erro interno |
| GET | `/api/veiculos/{id}` | Consulta veículo por ID | Rota: `id` inteiro | Nenhum | `200`: veículo; `404`: não encontrado; `500`: erro interno |
| POST | `/api/veiculos` | Cadastra veículo | Nenhum | `modelo`, `placa`, `ano`, `quilometragem`, `valorDiaria`, `fabricanteId`, `categoriaVeiculoId` | `201`: veículo criado; `400`: validação ou FK inválida; `409`: placa duplicada; `500`: erro interno |
| PUT | `/api/veiculos/{id}` | Atualiza veículo | Rota: `id` inteiro | Mesmo JSON do POST | `204`: atualizado; `400`: validação ou FK inválida; `404`: não encontrado; `409`: placa duplicada; `500`: erro interno |
| DELETE | `/api/veiculos/{id}` | Exclui veículo | Rota: `id` inteiro | Nenhum | `204`: excluído; `404`: não encontrado; `409`: possui aluguéis; `500`: erro interno |

## Clientes

| Método | Rota | Descrição | Parâmetros | Corpo | Respostas e retorno |
| --- | --- | --- | --- | --- | --- |
| GET | `/api/clientes` | Lista clientes | Nenhum | Nenhum | `200`: lista de clientes; `500`: erro interno |
| GET | `/api/clientes/{id}` | Consulta cliente por ID | Rota: `id` inteiro | Nenhum | `200`: cliente; `404`: não encontrado; `500`: erro interno |
| POST | `/api/clientes` | Cadastra cliente | Nenhum | `nome`, `cpf`, `email` obrigatórios; `telefone` opcional | `201`: cliente criado; `400`: dados inválidos; `409`: CPF ou email duplicado; `500`: erro interno |
| PUT | `/api/clientes/{id}` | Atualiza cliente | Rota: `id` inteiro | Mesmo JSON do POST | `204`: atualizado; `400`: dados inválidos; `404`: não encontrado; `409`: CPF ou email duplicado; `500`: erro interno |
| DELETE | `/api/clientes/{id}` | Exclui cliente | Rota: `id` inteiro | Nenhum | `204`: excluído; `404`: não encontrado; `409`: possui aluguéis; `500`: erro interno |

## Aluguéis

| Método | Rota | Descrição | Parâmetros | Corpo | Respostas e retorno |
| --- | --- | --- | --- | --- | --- |
| GET | `/api/alugueis` | Lista aluguéis | Nenhum | Nenhum | `200`: lista com cliente e veículo; `500`: erro interno |
| GET | `/api/alugueis/{id}` | Consulta aluguel por ID | Rota: `id` inteiro | Nenhum | `200`: aluguel; `404`: não encontrado; `500`: erro interno |
| POST | `/api/alugueis` | Cadastra aluguel | Nenhum | `clienteId`, `veiculoId`, datas, quilometragens e valores | `201`: aluguel criado; `400`: validação ou FK inválida; `409`: conflito no banco; `500`: erro interno |
| PUT | `/api/alugueis/{id}` | Atualiza aluguel ou registra devolução | Rota: `id` inteiro | Mesmo JSON do POST | `204`: atualizado; `400`: validação ou FK inválida; `404`: não encontrado; `409`: conflito no banco; `500`: erro interno |
| DELETE | `/api/alugueis/{id}` | Exclui aluguel | Rota: `id` inteiro | Nenhum | `204`: excluído; `404`: não encontrado; `409`: conflito no banco; `500`: erro interno |

## Filtros

| Método | Rota | Descrição | Parâmetros | Corpo | Respostas e retorno |
| --- | --- | --- | --- | --- | --- |
| GET | `/api/filtros/veiculos-por-fabricante` | Veículos de um fabricante com INNER JOIN | Query: `fabricante` string, obrigatório. Exemplo: `?fabricante=Fiat` | Nenhum | `200`: veículos e fabricante; `400`: parâmetro vazio; `500`: erro interno |
| GET | `/api/filtros/veiculos-por-categoria` | Veículos de uma categoria com INNER JOIN | Query: `categoria` string, obrigatório. Exemplo: `?categoria=SUV` | Nenhum | `200`: veículos e categoria; `400`: parâmetro vazio; `500`: erro interno |
| GET | `/api/filtros/alugueis-por-cliente` | Aluguéis de um cliente com INNER JOIN múltiplo | Query: `cpf` string, obrigatório | Nenhum | `200`: aluguéis, cliente e veículo; `400`: parâmetro vazio; `500`: erro interno |
| GET | `/api/filtros/clientes-com-alugueis` | Clientes com ou sem aluguéis usando LEFT JOIN | Nenhum | Nenhum | `200`: clientes e aluguéis opcionais; `500`: erro interno |
| GET | `/api/filtros/veiculos-com-historico` | Veículos com ou sem histórico usando LEFT JOIN | Nenhum | Nenhum | `200`: veículos e aluguéis opcionais; `500`: erro interno |

## Exemplos de corpos

### Veículo

```json
{
  "modelo": "Argo",
  "placa": "ABC1D23",
  "ano": 2024,
  "quilometragem": 1000,
  "valorDiaria": 150.00,
  "fabricanteId": 1,
  "categoriaVeiculoId": 1
}
```

### Aluguel

```json
{
  "clienteId": 1,
  "veiculoId": 1,
  "dataInicio": "2026-10-04T10:00:00",
  "dataFimPrevista": "2026-10-07T10:00:00",
  "dataDevolucao": null,
  "quilometragemInicial": 1000,
  "quilometragemFinal": null,
  "valorDiaria": 150.00,
  "valorTotal": null
}
```
