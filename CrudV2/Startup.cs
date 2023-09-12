using CrudV2.Business.Mapping;
using Microsoft.OpenApi.Models;
using CrudV2.Data;
using Microsoft.EntityFrameworkCore;
using CrudV2.Business.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CrudV2.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // Adicione um log para verificar se o construtor do Startup está sendo chamado corretamente
            Console.WriteLine("Chamou o construtor do Startup");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configuração do banco de dados MySQL
            //services.AddDbContext<ApplicationDbContext>((provider, options) =>
            //{
            //    var configuration = provider.GetRequiredService<IConfiguration>();
            //    var connectionString = configuration.GetConnectionString("DefaultConnection");
            //    options.UseMySQL(connectionString);
            //});

            // Configuração do AutoMapper
            services.AddAutoMapper(typeof(AutoMappingProfile)); // Substitua 'AutoMappingProfile' pelo nome da sua classe de perfil

            services.AddScoped<IUserUseCases, UserUseCases>();// Injecao de dependencia para a classe UserUseCases 
            // Outras configurações de serviços, como injeção de dependência, banco de dados, autenticação, etc.


            // Configuração do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CrudV2", Version = "v1" });
            });

            #region JWT
            // Configuração do JWT
            var key = Encoding.ASCII.GetBytes("abcd1234"); // Substitua por uma chave segura!
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Você pode definir como true se quiser validar o emissor (issuer)
                    ValidateAudience = false // Você pode definir como true se quiser validar a audiência (audience)
                };
            });
            #endregion

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrudV2");
                });

                dbContext.Database.EnsureCreated();
            }
            else
            {
                // Configurações de tratamento de erros em ambiente de produção
                // Por exemplo, app.UseExceptionHandler("/Home/Error");
                // e app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Adicione um log para verificar se o pipeline de middleware está sendo configurado
            Console.WriteLine("Configurando o pipeline de middleware");
        }
    }
}
