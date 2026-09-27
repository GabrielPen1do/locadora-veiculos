using LocadoraVeiculos.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FiltrosController(ApplicationContext context) : ControllerBase
{
    [HttpGet("veiculos-por-fabricante")]
    public async Task<IActionResult> VeiculosPorFabricante([FromQuery] string fabricante)
    {
        if (string.IsNullOrWhiteSpace(fabricante)) return BadRequest();
        var consulta = from veiculo in context.Veiculos.AsNoTracking()
                       join fab in context.Fabricantes.AsNoTracking() on veiculo.FabricanteId equals fab.Id
                       where fab.Nome.Contains(fabricante)
                       select new { VeiculoId = veiculo.Id, veiculo.Modelo, veiculo.Placa, veiculo.Ano, veiculo.Quilometragem, Fabricante = fab.Nome };
        return Ok(await consulta.ToListAsync());
    }

    [HttpGet("veiculos-por-categoria")]
    public async Task<IActionResult> VeiculosPorCategoria([FromQuery] string categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria)) return BadRequest();
        var consulta = from veiculo in context.Veiculos.AsNoTracking()
                       join cat in context.CategoriasVeiculo.AsNoTracking() on veiculo.CategoriaVeiculoId equals cat.Id
                       where cat.Nome.Contains(categoria)
                       select new { VeiculoId = veiculo.Id, veiculo.Modelo, veiculo.Placa, veiculo.Ano, veiculo.Quilometragem, Categoria = cat.Nome };
        return Ok(await consulta.ToListAsync());
    }

    [HttpGet("alugueis-por-cliente")]
    public async Task<IActionResult> AlugueisPorCliente([FromQuery] string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) return BadRequest();
        var consulta = from aluguel in context.Alugueis.AsNoTracking()
                       join cliente in context.Clientes.AsNoTracking() on aluguel.ClienteId equals cliente.Id
                       join veiculo in context.Veiculos.AsNoTracking() on aluguel.VeiculoId equals veiculo.Id
                       where cliente.Cpf == cpf
                       select new
                       {
                           AluguelId = aluguel.Id, Cliente = cliente.Nome, cliente.Cpf,
                           Veiculo = veiculo.Modelo, veiculo.Placa, aluguel.DataInicio,
                           aluguel.DataFimPrevista, aluguel.DataDevolucao, aluguel.ValorDiaria, aluguel.ValorTotal
                       };
        return Ok(await consulta.ToListAsync());
    }

    [HttpGet("clientes-com-alugueis")]
    public async Task<IActionResult> ClientesComAlugueis()
    {
        var consulta = from cliente in context.Clientes.AsNoTracking()
                       join aluguel in context.Alugueis.AsNoTracking() on cliente.Id equals aluguel.ClienteId into alugueis
                       from aluguel in alugueis.DefaultIfEmpty()
                       select new
                       {
                           ClienteId = cliente.Id, cliente.Nome, cliente.Cpf,
                           AluguelId = aluguel == null ? (int?)null : aluguel.Id,
                           DataInicio = aluguel == null ? (DateTime?)null : aluguel.DataInicio,
                           DataFimPrevista = aluguel == null ? (DateTime?)null : aluguel.DataFimPrevista
                       };
        return Ok(await consulta.ToListAsync());
    }

    [HttpGet("veiculos-com-historico")]
    public async Task<IActionResult> VeiculosComHistorico()
    {
        var consulta = from veiculo in context.Veiculos.AsNoTracking()
                       join aluguel in context.Alugueis.AsNoTracking() on veiculo.Id equals aluguel.VeiculoId into alugueis
                       from aluguel in alugueis.DefaultIfEmpty()
                       select new
                       {
                           VeiculoId = veiculo.Id, veiculo.Modelo, veiculo.Placa, veiculo.Quilometragem,
                           AluguelId = aluguel == null ? (int?)null : aluguel.Id,
                           DataInicio = aluguel == null ? (DateTime?)null : aluguel.DataInicio,
                           DataDevolucao = aluguel == null ? null : aluguel.DataDevolucao
                       };
        return Ok(await consulta.ToListAsync());
    }
}
