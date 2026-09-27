namespace LocadoraVeiculos.Api.Models;

public class Veiculo
{
    public int Id { get; set; }
    public string Modelo { get; set; } = string.Empty;
    public string Placa { get; set; } = string.Empty;
    public int Ano { get; set; }
    public int Quilometragem { get; set; }
    public decimal ValorDiaria { get; set; }
    public int FabricanteId { get; set; }
    public Fabricante Fabricante { get; set; } = null!;
    public int CategoriaVeiculoId { get; set; }
    public CategoriaVeiculo CategoriaVeiculo { get; set; } = null!;
    public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
}
