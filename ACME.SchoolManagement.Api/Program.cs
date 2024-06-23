using ACME.SchoolManagement.Api.Installers.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Registrar manejadores de excepci�n
//builder.Services.AddTransient<IExceptionHandler, UnauthorizedAccessExceptionHandler>();
//builder.Services.AddTransient<IExceptionHandler, PaymentExceptionHandler>();
//builder.Services.AddTransient<IExceptionHandler, DataConsistencyExceptionHandler>();
//builder.Services.AddTransient<IExceptionHandler, InvalidDataExceptionHandler>();


//// Otros servicios
//builder.Services.AddMvc(
//                config =>
//                {
//                    config.Filters.Add(typeof(GlobalExceptionFilter));
//                });
//builder.Services.AddHttpContextAccessor();

builder.Services.InstallServicesInAssembly(configuration);
var app = builder.Build();

// Configura el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
