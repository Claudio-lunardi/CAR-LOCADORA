using AspNetCoreRateLimit;
using CarLocadora.API.Extencoes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurarServicos();
builder.Services.ConfigurarJwt();
builder.Services.ConfigurarSwagger();


builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseIpRateLimiting();

app.MapControllers();
app.Run();
