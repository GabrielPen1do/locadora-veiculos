# Rotas da Etapa 2

## CRUD

| Método | Rota | Descrição |
| --- | --- | --- |
| GET | `/api/fabricantes` | Lista fabricantes |
| GET | `/api/fabricantes/{id}` | Consulta fabricante |
| POST | `/api/fabricantes` | Cria fabricante |
| PUT | `/api/fabricantes/{id}` | Atualiza fabricante |
| DELETE | `/api/fabricantes/{id}` | Exclui fabricante |
| GET | `/api/categoriasveiculos` | Lista categorias |
| GET | `/api/categoriasveiculos/{id}` | Consulta categoria |
| POST | `/api/categoriasveiculos` | Cria categoria |
| PUT | `/api/categoriasveiculos/{id}` | Atualiza categoria |
| DELETE | `/api/categoriasveiculos/{id}` | Exclui categoria |
| GET | `/api/veiculos` | Lista veículos |
| GET | `/api/veiculos/{id}` | Consulta veículo |
| POST | `/api/veiculos` | Cria veículo |
| PUT | `/api/veiculos/{id}` | Atualiza veículo |
| DELETE | `/api/veiculos/{id}` | Exclui veículo |
| GET | `/api/clientes` | Lista clientes |
| GET | `/api/clientes/{id}` | Consulta cliente |
| POST | `/api/clientes` | Cria cliente |
| PUT | `/api/clientes/{id}` | Atualiza cliente |
| DELETE | `/api/clientes/{id}` | Exclui cliente |
| GET | `/api/alugueis` | Lista aluguéis |
| GET | `/api/alugueis/{id}` | Consulta aluguel |
| POST | `/api/alugueis` | Cria aluguel |
| PUT | `/api/alugueis/{id}` | Atualiza aluguel |
| DELETE | `/api/alugueis/{id}` | Exclui aluguel |

## Filtros

| Método | Rota | Junção |
| --- | --- | --- |
| GET | `/api/filtros/veiculos-por-fabricante?fabricante=Fiat` | INNER JOIN de veículo e fabricante |
| GET | `/api/filtros/veiculos-por-categoria?categoria=SUV` | INNER JOIN de veículo e categoria |
| GET | `/api/filtros/alugueis-por-cliente?cpf=...` | INNER JOIN de aluguel, cliente e veículo |
| GET | `/api/filtros/clientes-com-alugueis` | LEFT JOIN de cliente e aluguel |
| GET | `/api/filtros/veiculos-com-historico` | LEFT JOIN de veículo e aluguel |
