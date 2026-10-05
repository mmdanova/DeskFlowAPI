using Deskflow.Api.Exceptions;

namespace Deskflow.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NaoEncontradoException ex)
            {
                await Responder(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (RegraNegocioException ex)
            {
                await Responder(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao processar a requisição.");
                await Responder(context, StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
            }
        }

        private static Task Responder(HttpContext context, int statusCode, string mensagem)
        {
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(new { erro = mensagem });
        }
    }
}
