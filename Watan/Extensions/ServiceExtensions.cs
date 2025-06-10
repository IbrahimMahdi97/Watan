using System.Reflection;
using System.Text;
using FluentMigrator.Runner;
using Interfaces;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Service;
using Service.Interface;

namespace Watan.Extensions;

public static class ServiceExtensions
{
	public static void ConfigureCors(this IServiceCollection services) =>
		services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy", builder =>
			builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());
		});

	public static void ConfigureLoggerService(this IServiceCollection services) =>
		services.AddSingleton<ILoggerManager, LoggerManager>();

	public static void ConfigureFluentMigrator(this IServiceCollection services, 
		IConfiguration configuration) => services.AddLogging(c => 
		c.AddFluentMigratorConsole())
			.AddFluentMigratorCore().ConfigureRunner(c => 
				c.AddSqlServer2016().WithGlobalConnectionString(configuration
						.GetConnectionString("sqlConnection"))
					.ScanIn(Assembly.GetExecutingAssembly())
						.For.Migrations());

	public static void ConfigureRepositoryManager(this IServiceCollection services) =>
		services.AddScoped<IRepositoryManager, RepositoryManager>();

	public static void ConfigureServiceManager(this IServiceCollection services) =>
		services.AddScoped<IServiceManager, ServiceManager>();

	public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtSettings = configuration.GetSection("JwtSettings");
		var secretKey = jwtSettings["secretKey"];

		services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,

				ValidIssuer = jwtSettings["validIssuer"],
				ValidAudience = jwtSettings["validAudience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
			};
		});
	}
	
	public static void ConfigureSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(s =>
		{
			s.SwaggerDoc("v1", new OpenApiInfo { Title = "Watan API", Version = "v1" });
			s.AddSecurityDefinition("Bearer",
				new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Place to add JWT with Bearer",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "Bearer"
				});
			s.AddSecurityRequirement(new OpenApiSecurityRequirement()
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
						Name = "Bearer",
					},
					Array.Empty<string>()
				}
			});
		});
	}
}
