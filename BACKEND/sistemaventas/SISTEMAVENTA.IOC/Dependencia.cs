using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SISTEMAVENTA.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISTEMAVENTA.DAL.Modelos.interfaces;
using SISTEMAVENTA.DAL.Modelos;
using SISTEMAVENTA.IOC;
using SISTEMAVENTA.UTILITY;
using SITEMAVENTA.BLL.Servicios.Accesos;
using SITEMAVENTA.BLL.Servicios;

namespace SISTEMAVENTA.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SistemaContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSql"));
            });
         
            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IventaRepository, ventaModelo>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioServices, UsuarioService>();
            services.AddScoped<ICategoriaService,CategoriaService>();
            services.AddScoped<IProductoService,ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDashBoardService, DashboardService>();
            services.AddScoped<IMenuService, MenuService>();
        }
    }
}
