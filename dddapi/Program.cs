using dddapi.Infrastructure.Contracts.Contracts;
using dddapi.Infrastructure.Impl.Context;
using dddapi.Infrastructure.Impl.Data;
using dddapi.Infrastructure.Impl.Impl;
using dddapi.ServiceLibrary.Contracts.Contracts;
using dddapi.ServiceLibrary.Impl.Implementation;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main"); // si aparece esto texto,  sginfica que logger esta funcionando bien

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddScoped<IDataBaseService, DataBaseService>(); // caundo usas injection de dependecia --> private readonly y interfaces
    builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
    builder.Services.AddScoped<ISchoolService, SchoolService>();

    builder.Services.AddControllers();

    builder.Services.AddDbContext<DataContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // agregar logger al contenedor de dependencias
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e) // aqui capturamos la excepcion y vamos a logearla
{
    logger.Error(e, "Program has stopped because there was an exception.");
    throw;
}
finally
{
    NLog.LogManager.Shutdown(); // para asegurarse que cuando la aplicacion se acabe, tambien se cierre Nlog
}
