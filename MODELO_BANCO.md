# Modelo do banco de dados

Banco: `LocadoraVeiculos` (SQL Server Express). Todas as tabelas têm chave primária inteira `Id`, gerada pelo SQL Server.

| Entidade | Campos principais | Relacionamentos |
| --- | --- | --- |
| Fabricante | Nome obrigatório, até 100 caracteres, único | Um fabricante possui muitos veículos |
| CategoriaVeiculo | Nome obrigatório, até 80 caracteres, único; Descricao opcional, até 300 | Uma categoria possui muitos veículos |
| Veiculo | Modelo obrigatório, até 100; Placa obrigatória, até 7, única; Ano maior que 1900; ValorDiaria decimal(18,2), não negativo | FabricanteId e CategoriaVeiculoId são FKs obrigatórias; um veículo possui muitos aluguéis |
| Cliente | Nome obrigatório, até 150; Cpf obrigatório, até 11, único; Email obrigatório, até 254, único; Telefone opcional, até 20 | Um cliente possui muitos aluguéis |
| Aluguel | DataInicio e DataFim obrigatórias, com fim igual ou posterior ao início; ValorTotal decimal(18,2), não negativo | ClienteId e VeiculoId são FKs obrigatórias |

As quatro FKs usam exclusão restrita para preservar o histórico dos aluguéis. Índices únicos protegem nomes de fabricantes e categorias, placas, CPFs e emails. As regras numéricas e de datas são `CHECK` constraints na migration inicial. Validações de formato, disponibilidade do veículo e sobreposição de períodos pertencem a uma etapa futura da aplicação.
