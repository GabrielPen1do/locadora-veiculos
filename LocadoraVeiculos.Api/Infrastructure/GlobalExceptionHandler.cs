using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculos.Api.Infrastructure;

public class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Erro não tratado na requisição {Method} {Path}",
            httpContext.Request.Method, httpContext.Request.Path);
        var status = exception is DbUpdateException
            ? StatusCodes.Status409Conflict
            : StatusCodes.Status500InternalServerError;

        httpContext.Response.StatusCode = status;
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = new ProblemDetails
            {
                Status = status,
                Title = status == StatusCodes.Status409Conflict
                    ? "Conflito ao salvar os dados."
                    : "Ocorreu um erro interno."
            },
            Exception = exception
        });
    }
}
