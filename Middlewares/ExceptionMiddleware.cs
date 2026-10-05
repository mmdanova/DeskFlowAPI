using Deskflow.Api.Exceptions;

namespace Deskflow.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
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
        }

        private static Task Responder(HttpContext context, int statusCode, string mensagem)
        {
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(new { erro = mensagem });
        }
    }
}
