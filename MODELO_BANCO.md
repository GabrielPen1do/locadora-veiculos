# Modelo do banco de dados

Banco `LocadoraVeiculos` em SQL Server Express. Todas as tabelas possuem chave primária inteira `Id`.

| Entidade | Campos principais | Relacionamentos |
| --- | --- | --- |
| Fabricante | Nome obrigatório e único | Um fabricante possui muitos veículos |
| CategoriaVeiculo | Nome obrigatório e único; descrição opcional | Uma categoria possui muitos veículos |
| Veiculo | Modelo, placa única, ano, quilometragem e valor da diária | Pertence a um fabricante e uma categoria; possui muitos aluguéis |
| Cliente | Nome, CPF único, email único e telefone opcional | Um cliente possui muitos aluguéis |
| Aluguel | Datas de início, fim previsto e devolução; quilometragens inicial e final; valores da diária e total | Pertence a um cliente e um veículo |

As chaves estrangeiras usam exclusão restrita. Restrições `CHECK` garantem ano válido no banco, quilometragens não negativas, coerência entre datas e valores permitidos. A migration `Etapa2Backend` preserva a coluna de término existente ao renomeá-la para `DataFimPrevista` e adiciona os campos necessários para a devolução.
