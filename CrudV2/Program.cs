using CrudV2.Business.Interfaces;
using CrudV2.Business.Mapping;
using CrudV2.Core.Interfaces;
using CrudV2.Data;
using CrudV2.Data.Repositories;
using CrudV2.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Configuração do banco de dados MySQL
        builder.Services.AddDbContext<ApplicationDbContext>((provider, options) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        // Configuração do AutoMapper
        builder.Services.AddAutoMapper(typeof(AutoMappingProfile));
        builder.Services.AddScoped<IUserUseCases, UserUseCases>();// Injecao de dependencia para a classe UserUseCases 

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
}