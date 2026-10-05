using Deskflow.Api.Data.Entities;
using Deskflow.Api.Repositories;
using Deskflow.Api.Repositories.Interfaces;
using Deskflow.Api.Services;
using Deskflow.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IChamadoService, ChamadoService>();
builder.Services.AddScoped<IChamadoRepository, ChamadoRepository>();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(o =>
    {
        o.SuppressMapClientErrors = true;
        o.InvalidModelStateResponseFactory = ctx =>
        {            
            var erros = ctx.ModelState
                .Where(kv => kv.Value?.Errors.Count > 0)
                .Select(kv => string.IsNullOrEmpty(kv.Key) || kv.Key.StartsWith('$')
                    ? "Corpo da requisição inválido."
                    : $"Valor inválido para o parâmetro '{kv.Key}'.")
                .Distinct();
            return new BadRequestObjectResult(new { erro = string.Join(" ", erros) });
        };
    })
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<Deskflow.Api.Middlewares.ExceptionHandlingMiddleware>();

app.UseStatusCodePages(async ctx =>
{
    ctx.HttpContext.Response.ContentType = "application/json";
    var mensagem = ctx.HttpContext.Response.StatusCode switch
    {
        404 => "Recurso não encontrado.",
        405 => "Método HTTP não permitido para este recurso.",
        415 => "Tipo de conteúdo não suportado.",
        _ => "Não foi possível processar a requisição."
    };
    await ctx.HttpContext.Response.WriteAsJsonAsync(new { erro = mensagem });
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();