using LocadoraVeiculos.Api.Data;
using LocadoraVeiculos.Api.Dtos;
using LocadoraVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteResponse>>> GetAll()
    {
        var itens = await Query().ToListAsync();
        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteResponse>> GetById(int id)
    {
        var item = await Query(id).FirstOrDefaultAsync();
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponse>> Create(ClienteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest(new ProblemDetails { Title = "Nome é obrigatório." });
        var conflito = await ValidarDuplicidade(request.Cpf, request.Email, null);
        if (conflito is not null) return conflito;
        var item = new Cliente
        {
            Nome = request.Nome.Trim(), Cpf = request.Cpf.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(), Telefone = request.Telefone?.Trim()
        };
        context.Clientes.Add(item);
        await context.SaveChangesAsync();
        var response = new ClienteResponse(item.Id, item.Nome, item.Cpf, item.Email, item.Telefone);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ClienteRequest request)
    {
        var item = await context.Clientes.FindAsync(id);
        if (item is null) return NotFound();
        if (string.IsNullOrWhiteSpace(request.Nome))
            return BadRequest(new ProblemDetails { Title = "Nome é obrigatório." });
        var conflito = await ValidarDuplicidade(request.Cpf, request.Email, id);
        if (conflito is not null) return conflito;
        item.Nome = request.Nome.Trim();
        item.Cpf = request.Cpf.Trim();
        item.Email = request.Email.Trim().ToLowerInvariant();
        item.Telefone = request.Telefone?.Trim();
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await context.Clientes.FindAsync(id);
        if (item is null) return NotFound();
        if (await context.Alugueis.AnyAsync(x => x.ClienteId == id))
            return Conflict(new ProblemDetails { Title = "O cliente possui aluguéis associados." });
        context.Clientes.Remove(item);
        await context.SaveChangesAsync();
        return NoContent();
    }

    private IQueryable<ClienteResponse> Query(int? id = null)
    {
        var query = context.Clientes.AsNoTracking();
        if (id.HasValue) query = query.Where(x => x.Id == id.Value);
        return query.Select(x => new ClienteResponse(x.Id, x.Nome, x.Cpf, x.Email, x.Telefone));
    }

    private async Task<ActionResult?> ValidarDuplicidade(string cpfInformado, string emailInformado, int? id)
    {
        if (string.IsNullOrWhiteSpace(cpfInformado) || string.IsNullOrWhiteSpace(emailInformado))
            return BadRequest(new ProblemDetails { Title = "CPF e email são obrigatórios." });
        var cpf = cpfInformado.Trim();
        var email = emailInformado.Trim().ToLowerInvariant();
        if (await context.Clientes.AnyAsync(x => x.Cpf == cpf && x.Id != id))
            return Conflict(new ProblemDetails { Title = "Já existe um cliente com esse CPF." });
        if (await context.Clientes.AnyAsync(x => x.Email == email && x.Id != id))
            return Conflict(new ProblemDetails { Title = "Já existe um cliente com esse email." });
        return null;
    }
}
