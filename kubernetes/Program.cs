using Kubernetes.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

// // Configura o CORS (libera acesso do React, por exemplo)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000") // frontend React
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Usa Swagger no modo desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp"); // CORS deve vir antes dos Controllers

app.UseAuthorization();

app.MapControllers();

app.Run();
