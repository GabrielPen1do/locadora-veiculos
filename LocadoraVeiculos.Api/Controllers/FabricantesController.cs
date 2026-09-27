using LocadoraVeiculos.Api.Data;
using LocadoraVeiculos.Api.Dtos;
using LocadoraVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FabricantesController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FabricanteResponse>>> GetAll()
    {
        var itens = await context.Fabricantes.AsNoTracking()
            .Select(x => new FabricanteResponse(x.Id, x.Nome)).ToListAsync();
        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<FabricanteResponse>> GetById(int id)
    {
        var item = await context.Fabricantes.AsNoTracking()
            .Where(x => x.Id == id).Select(x => new FabricanteResponse(x.Id, x.Nome))
            .FirstOrDefaultAsync();
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<FabricanteResponse>> Create(FabricanteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest(new ProblemDetails { Title = "Nome é obrigatório." });
        var nome = request.Nome.Trim();
        if (await context.Fabricantes.AnyAsync(x => x.Nome == nome))
            return Conflict(new ProblemDetails { Title = "Já existe um fabricante com esse nome." });

        var item = new Fabricante { Nome = nome };
        context.Fabricantes.Add(item);
        await context.SaveChangesAsync();
        var response = new FabricanteResponse(item.Id, item.Nome);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, FabricanteRequest request)
    {
        var item = await context.Fabricantes.FindAsync(id);
        if (item is null) return NotFound();
        if (string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest(new ProblemDetails { Title = "Nome é obrigatório." });
        var nome = request.Nome.Trim();
        if (await context.Fabricantes.AnyAsync(x => x.Nome == nome && x.Id != id))
            return Conflict(new ProblemDetails { Title = "Já existe um fabricante com esse nome." });
        item.Nome = nome;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await context.Fabricantes.FindAsync(id);
        if (item is null) return NotFound();
        if (await context.Veiculos.AnyAsync(x => x.FabricanteId == id))
            return Conflict(new ProblemDetails { Title = "O fabricante possui veículos associados." });
        context.Fabricantes.Remove(item);
        await context.SaveChangesAsync();
        return NoContent();
    }
}
