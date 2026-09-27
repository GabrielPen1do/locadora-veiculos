using LocadoraVeiculos.Api.Data;
using LocadoraVeiculos.Api.Dtos;
using LocadoraVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasVeiculosController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaVeiculoResponse>>> GetAll()
    {
        var itens = await context.CategoriasVeiculo.AsNoTracking()
            .Select(x => new CategoriaVeiculoResponse(x.Id, x.Nome, x.Descricao)).ToListAsync();
        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaVeiculoResponse>> GetById(int id)
    {
        var item = await context.CategoriasVeiculo.AsNoTracking().Where(x => x.Id == id)
            .Select(x => new CategoriaVeiculoResponse(x.Id, x.Nome, x.Descricao)).FirstOrDefaultAsync();
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaVeiculoResponse>> Create(CategoriaVeiculoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest(new ProblemDetails { Title = "Nome é obrigatório." });
        var nome = request.Nome.Trim();
        if (await context.CategoriasVeiculo.AnyAsync(x => x.Nome == nome))
            return Conflict(new ProblemDetails { Title = "Já existe uma categoria com esse nome." });
        var item = new CategoriaVeiculo { Nome = nome, Descricao = request.Descricao?.Trim() };
        context.CategoriasVeiculo.Add(item);
        await context.SaveChangesAsync();
        var response = new CategoriaVeiculoResponse(item.Id, item.Nome, item.Descricao);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CategoriaVeiculoRequest request)
    {
        var item = await context.CategoriasVeiculo.FindAsync(id);
        if (item is null) return NotFound();
        if (string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest(new ProblemDetails { Title = "Nome é obrigatório." });
        var nome = request.Nome.Trim();
        if (await context.CategoriasVeiculo.AnyAsync(x => x.Nome == nome && x.Id != id))
            return Conflict(new ProblemDetails { Title = "Já existe uma categoria com esse nome." });
        item.Nome = nome;
        item.Descricao = request.Descricao?.Trim();
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await context.CategoriasVeiculo.FindAsync(id);
        if (item is null) return NotFound();
        if (await context.Veiculos.AnyAsync(x => x.CategoriaVeiculoId == id))
            return Conflict(new ProblemDetails { Title = "A categoria possui veículos associados." });
        context.CategoriasVeiculo.Remove(item);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
