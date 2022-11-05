using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;

using API.Interface;
using BLL;
using DAL;
using BibliotecaViva.DAO;
using DAL.Interfaces;
using BLL.Interfaces;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            AdicionarControladores(services);
            AdicionarDataContext(services, Configuration.GetConnectionString("BibliotecaVivaApiConnection"));
            RealizarInjecaoDeDependenciasBLL(services);
            RealizarInjecaoDeDependenciasDAL(services);
            DefinirConfiguracaoSwagger(services);           
        }

        private static void AdicionarControladores(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
        }
        private static void AdicionarDataContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<plataformaonlifeContext>(options => options.UseNpgsql(connectionString));
        }

        private static void RealizarInjecaoDeDependenciasBLL(IServiceCollection services)
        {
            services.AddScoped<ITipoBLL, TipoBLL>();
            services.AddScoped<IPessoaBLL, PessoaBLL>();
            services.AddScoped<IRegistroBLL, RegistroBLL>();
            services.AddScoped<ILocalidadeBLL, LocalidadeBLL>();
        }

        private static void RealizarInjecaoDeDependenciasDAL(IServiceCollection services)
        { 
            services.AddScoped<ITipoDAL, TipoDAL>();
            services.AddScoped<IIdiomaDAL, IdiomaDAL>();
            services.AddScoped<IPessoaDAL, PessoaDAL>();
            services.AddScoped<IRequisicao, Requisicao>();
            services.AddScoped<IRegistroDAL, RegistroDAL>();
            services.AddScoped<IReferenciaDAL, ReferenciaDAL>();
            services.AddScoped<ITipoRelacaoDAL, TipoRelecaoDAL>();
            services.AddScoped<ITipoExecucaoDAL, TipoExecucaoDAL>();
            services.AddScoped<IPessoaRegistroDAL, PessoaRegistroDAL>();
            services.AddScoped<ILocalidadeDAL, LocalidadeDAL>();
        }

        private static void DefinirConfiguracaoSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Plataforma Onlife API", Version = "v1" });
                options.CustomOperationIds(d => (d.ActionDescriptor as ControllerActionDescriptor)?.ActionName);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(option => { 
                option.RouteTemplate = swaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(option => {
                option.RoutePrefix = swaggerOptions.RoutePrefix;
                option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        } 
    }
}
