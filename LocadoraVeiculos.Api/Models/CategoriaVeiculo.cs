namespace LocadoraVeiculos.Api.Models;

public class CategoriaVeiculo
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}
