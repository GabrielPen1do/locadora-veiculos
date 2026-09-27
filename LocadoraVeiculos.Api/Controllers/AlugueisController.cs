using LocadoraVeiculos.Api.Data;
using LocadoraVeiculos.Api.Dtos;
using LocadoraVeiculos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlugueisController(ApplicationContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AluguelResponse>>> GetAll()
    {
        var itens = await Query().ToListAsync();
        return Ok(itens);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AluguelResponse>> GetById(int id)
    {
        var item = await Query().FirstOrDefaultAsync(x => x.Id == id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<AluguelResponse>> Create(AluguelRequest request)
    {
        var erro = await Validar(request);
        if (erro is not null) return erro;
        var item = Mapear(request, new Aluguel());
        context.Alugueis.Add(item);
        await context.SaveChangesAsync();
        var response = await Query().FirstAsync(x => x.Id == item.Id);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AluguelRequest request)
    {
        var item = await context.Alugueis.FindAsync(id);
        if (item is null) return NotFound();
        var erro = await Validar(request);
        if (erro is not null) return erro;
        Mapear(request, item);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await context.Alugueis.FindAsync(id);
        if (item is null) return NotFound();
        context.Alugueis.Remove(item);
        await context.SaveChangesAsync();
        return NoContent();
    }

    private IQueryable<AluguelResponse> Query() => context.Alugueis.AsNoTracking().Select(x =>
        new AluguelResponse(x.Id, x.ClienteId, x.Cliente.Nome, x.VeiculoId, x.Veiculo.Modelo,
            x.Veiculo.Placa, x.DataInicio, x.DataFimPrevista, x.DataDevolucao,
            x.QuilometragemInicial, x.QuilometragemFinal, x.ValorDiaria, x.ValorTotal));

    private async Task<ActionResult?> Validar(AluguelRequest request)
    {
        if (!await context.Clientes.AnyAsync(x => x.Id == request.ClienteId))
            return BadRequest(new ProblemDetails { Title = "ClienteId inválido." });
        if (!await context.Veiculos.AnyAsync(x => x.Id == request.VeiculoId))
            return BadRequest(new ProblemDetails { Title = "VeiculoId inválido." });
        if (request.DataFimPrevista <= request.DataInicio)
            return BadRequest(new ProblemDetails { Title = "DataFimPrevista deve ser posterior à DataInicio." });
        if (request.DataDevolucao.HasValue && request.DataDevolucao < request.DataInicio)
            return BadRequest(new ProblemDetails { Title = "DataDevolucao não pode ser anterior à DataInicio." });
        if (request.QuilometragemFinal.HasValue && request.QuilometragemFinal < request.QuilometragemInicial)
            return BadRequest(new ProblemDetails { Title = "QuilometragemFinal deve ser maior ou igual à QuilometragemInicial." });
        return null;
    }

    private static Aluguel Mapear(AluguelRequest request, Aluguel item)
    {
        item.ClienteId = request.ClienteId;
        item.VeiculoId = request.VeiculoId;
        item.DataInicio = request.DataInicio;
        item.DataFimPrevista = request.DataFimPrevista;
        item.DataDevolucao = request.DataDevolucao;
        item.QuilometragemInicial = request.QuilometragemInicial;
        item.QuilometragemFinal = request.QuilometragemFinal;
        item.ValorDiaria = request.ValorDiaria;
        item.ValorTotal = request.ValorTotal;
        return item;
    }
}
