using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraVeiculos.Api.Migrations
{
    public partial class Etapa2Backend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Aluguel_Datas",
                table: "Alugueis");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Aluguel_ValorTotal",
                table: "Alugueis");

            migrationBuilder.RenameColumn(
                name: "DataFim",
                table: "Alugueis",
                newName: "DataFimPrevista");

            migrationBuilder.AddColumn<int>(
                name: "Quilometragem",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorTotal",
                table: "Alugueis",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDevolucao",
                table: "Alugueis",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuilometragemFinal",
                table: "Alugueis",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuilometragemInicial",
                table: "Alugueis",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorDiaria",
                table: "Alugueis",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.Sql("UPDATE a SET a.ValorDiaria = CASE WHEN v.ValorDiaria > 0 THEN v.ValorDiaria ELSE 0.01 END FROM Alugueis a INNER JOIN Veiculos v ON a.VeiculoId = v.Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Veiculo_Quilometragem",
                table: "Veiculos",
                sql: "[Quilometragem] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Aluguel_DataDevolucao",
                table: "Alugueis",
                sql: "[DataDevolucao] IS NULL OR [DataDevolucao] >= [DataInicio]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Aluguel_Datas",
                table: "Alugueis",
                sql: "[DataFimPrevista] > [DataInicio]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Aluguel_QuilometragemFinal",
                table: "Alugueis",
                sql: "[QuilometragemFinal] IS NULL OR [QuilometragemFinal] >= [QuilometragemInicial]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Aluguel_QuilometragemInicial",
                table: "Alugueis",
                sql: "[QuilometragemInicial] >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Aluguel_ValorDiaria",
                table: "Alugueis",
                sql: "[ValorDiaria] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Aluguel_ValorTotal",
                table: "Alugueis",
                sql: "[ValorTotal] IS NULL OR [ValorTotal] >= 0");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Veiculo_Quilometragem",
                table: "Veiculos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Aluguel_DataDevolucao",
                table: "Alugueis");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Aluguel_Datas",
                table: "Alugueis");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Aluguel_QuilometragemFinal",
                table: "Alugueis");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Aluguel_QuilometragemInicial",
                table: "Alugueis");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Aluguel_ValorDiaria",
                table: "Alugueis");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Aluguel_ValorTotal",
                table: "Alugueis");

            migrationBuilder.DropColumn(
                name: "Quilometragem",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "DataDevolucao",
                table: "Alugueis");

            migrationBuilder.DropColumn(
                name: "QuilometragemFinal",
                table: "Alugueis");

            migrationBuilder.DropColumn(
                name: "QuilometragemInicial",
                table: "Alugueis");

            migrationBuilder.DropColumn(
                name: "ValorDiaria",
                table: "Alugueis");

            migrationBuilder.RenameColumn(
                name: "DataFimPrevista",
                table: "Alugueis",
                newName: "DataFim");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorTotal",
                table: "Alugueis",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Aluguel_Datas",
                table: "Alugueis",
                sql: "[DataFim] >= [DataInicio]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Aluguel_ValorTotal",
                table: "Alugueis",
                sql: "[ValorTotal] >= 0");
        }
    }
}
