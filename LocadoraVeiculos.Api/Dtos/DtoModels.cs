using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculos.Api.Dtos;

public class FabricanteRequest
{
    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;
}

public record FabricanteResponse(int Id, string Nome);

public class CategoriaVeiculoRequest
{
    [Required, MaxLength(80)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Descricao { get; set; }
}

public record CategoriaVeiculoResponse(int Id, string Nome, string? Descricao);

public class VeiculoRequest
{
    [Required, MaxLength(100)]
    public string Modelo { get; set; } = string.Empty;

    [Required, MaxLength(7)]
    public string Placa { get; set; } = string.Empty;

    public int Ano { get; set; }

    [Range(0, int.MaxValue)]
    public int Quilometragem { get; set; }

    [Range(typeof(decimal), "0", "9999999999999999")]
    public decimal ValorDiaria { get; set; }

    [Range(1, int.MaxValue)]
    public int FabricanteId { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoriaVeiculoId { get; set; }
}

public record VeiculoResponse(
    int Id,
    string Modelo,
    string Placa,
    int Ano,
    int Quilometragem,
    decimal ValorDiaria,
    int FabricanteId,
    string Fabricante,
    int CategoriaVeiculoId,
    string Categoria);

public class ClienteRequest
{
    [Required, MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required, MaxLength(11)]
    public string Cpf { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(254)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Telefone { get; set; }
}

public record ClienteResponse(int Id, string Nome, string Cpf, string Email, string? Telefone);

public class AluguelRequest
{
    [Range(1, int.MaxValue)]
    public int ClienteId { get; set; }

    [Range(1, int.MaxValue)]
    public int VeiculoId { get; set; }

    public DateTime DataInicio { get; set; }
    public DateTime DataFimPrevista { get; set; }
    public DateTime? DataDevolucao { get; set; }

    [Range(0, int.MaxValue)]
    public int QuilometragemInicial { get; set; }

    [Range(0, int.MaxValue)]
    public int? QuilometragemFinal { get; set; }

    [Range(typeof(decimal), "0.01", "9999999999999999")]
    public decimal ValorDiaria { get; set; }

    [Range(typeof(decimal), "0", "9999999999999999")]
    public decimal? ValorTotal { get; set; }
}

public record AluguelResponse(
    int Id,
    int ClienteId,
    string Cliente,
    int VeiculoId,
    string Veiculo,
    string Placa,
    DateTime DataInicio,
    DateTime DataFimPrevista,
    DateTime? DataDevolucao,
    int QuilometragemInicial,
    int? QuilometragemFinal,
    decimal ValorDiaria,
    decimal? ValorTotal);
