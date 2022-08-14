using CursoOnline.Dados;
using CursoOnline.Dados.Contextos;
using CursoOnline.Dados.Repositorios;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Domain;
using CursoOnline.Dominio.PublicosAlvo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CursoOnline.Ioc
{
    public static class StartupIoc
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration["ConnectionString"])
            );

            services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
            services.AddScoped<ICursoRepositorio, CursoRepositorio>();
            services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ArmazenadorDeCurso>();
            services.AddScoped<ArmazenadorDeAluno>();

            services.AddScoped<IConversorDePublicoAlvo, ConversorDePublicoAlvo>();
        }
    }
}
