using Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using Repository;
using Shared.Helpers;
using Watan.Extensions;
using Watan.Migrations;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureCors();
builder.Services.ConfigureLoggerService();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<Database>();
//todo: for notifications
//builder.Services.AddSingleton<IFirebaseService, FirebaseService>();
builder.Services.ConfigureFluentMigrator(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSwagger();
builder.Services.AddAuthentication();
builder.Services.ConfigureJwt(builder.Configuration);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(WatanPresentation.AssemblyReference).Assembly).AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateConverter());
    });

var app = builder.Build();

/*app.UsePathBase("/Watan");
app.Use((context, next) =>
{
    context.Request.PathBase = "/Watan";
    return next();
});*/

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

//todo: if needed
//app.UseSwaggerAuthorized();

app.UseSwagger();

app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Watan API v1");
    s.RoutePrefix = string.Empty;
});

/*app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/Watan/swagger/v1/swagger.json", "Watan API v1");
    s.RoutePrefix = string.Empty;
});*/

app.MapControllers();

app.MigrateDatabase(logger);

app.Run();