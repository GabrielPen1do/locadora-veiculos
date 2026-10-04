using LocadoraVeiculos.Api.Data;
using LocadoraVeiculos.Api.Dtos;
using LocadoraVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VeiculosController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VeiculoResponse>>> GetAll()
    {
        var itens = await Query().ToListAsync();
        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<VeiculoResponse>> GetById(int id)
    {
        var item = await Query(id).FirstOrDefaultAsync();
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<VeiculoResponse>> Create(VeiculoRequest request)
    {
        var erro = await Validar(request);
        if (erro is not null) return erro;
        var placa = request.Placa.Trim().ToUpperInvariant();
        if (await context.Veiculos.AnyAsync(x => x.Placa == placa))
            return Conflict(new ProblemDetails { Title = "Já existe um veículo com essa placa." });

        var item = new Veiculo
        {
            Modelo = request.Modelo.Trim(), Placa = placa, Ano = request.Ano,
            Quilometragem = request.Quilometragem, ValorDiaria = request.ValorDiaria,
            FabricanteId = request.FabricanteId, CategoriaVeiculoId = request.CategoriaVeiculoId
        };
        context.Veiculos.Add(item);
        await context.SaveChangesAsync();
        var response = await Query(item.Id).FirstAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, VeiculoRequest request)
    {
        var item = await context.Veiculos.FindAsync(id);
        if (item is null) return NotFound();
        var erro = await Validar(request);
        if (erro is not null) return erro;
        var placa = request.Placa.Trim().ToUpperInvariant();
        if (await context.Veiculos.AnyAsync(x => x.Placa == placa && x.Id != id))
            return Conflict(new ProblemDetails { Title = "Já existe um veículo com essa placa." });

        item.Modelo = request.Modelo.Trim();
        item.Placa = placa;
        item.Ano = request.Ano;
        item.Quilometragem = request.Quilometragem;
        item.ValorDiaria = request.ValorDiaria;
        item.FabricanteId = request.FabricanteId;
        item.CategoriaVeiculoId = request.CategoriaVeiculoId;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await context.Veiculos.FindAsync(id);
        if (item is null) return NotFound();
        if (await context.Alugueis.AnyAsync(x => x.VeiculoId == id))
            return Conflict(new ProblemDetails { Title = "O veículo possui aluguéis associados." });
        context.Veiculos.Remove(item);
        await context.SaveChangesAsync();
        return NoContent();
    }

    private IQueryable<VeiculoResponse> Query(int? id = null)
    {
        var query = context.Veiculos.AsNoTracking();
        if (id.HasValue) query = query.Where(x => x.Id == id.Value);
        return query.Select(x => new VeiculoResponse(x.Id, x.Modelo, x.Placa, x.Ano,
            x.Quilometragem, x.ValorDiaria, x.FabricanteId, x.Fabricante.Nome,
            x.CategoriaVeiculoId, x.CategoriaVeiculo.Nome));
    }

    private async Task<ActionResult?> Validar(VeiculoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Modelo) || string.IsNullOrWhiteSpace(request.Placa))
            return BadRequest(new ProblemDetails { Title = "Modelo e placa são obrigatórios." });
        if (request.Ano < 1901 || request.Ano > DateTime.UtcNow.Year + 1)
            return BadRequest(new ProblemDetails { Title = "Ano de fabricação inválido." });
        if (!await context.Fabricantes.AnyAsync(x => x.Id == request.FabricanteId))
            return BadRequest(new ProblemDetails { Title = "FabricanteId inválido." });
        if (!await context.CategoriasVeiculo.AnyAsync(x => x.Id == request.CategoriaVeiculoId))
            return BadRequest(new ProblemDetails { Title = "CategoriaVeiculoId inválido." });
        return null;
    }
}
