namespace LocadoraVeiculos.Api.Models;

public class Aluguel
{
    public int Id { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFimPrevista { get; set; }
    public DateTime? DataDevolucao { get; set; }
    public int QuilometragemInicial { get; set; }
    public int? QuilometragemFinal { get; set; }
    public decimal ValorDiaria { get; set; }
    public decimal? ValorTotal { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public int VeiculoId { get; set; }
    public Veiculo Veiculo { get; set; } = null!;
}
