using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SISTEMAVENTA.DAL.Modelos.interfaces;
using SISTEMAVENTA.DTO;
using SISTEMAVENTA.MODEL;
using SITEMAVENTA.BLL.Servicios.Accesos;
namespace SITEMAVENTA.BLL.Servicios
{
    public class DashboardService : IDashBoardService
    {

        private readonly IventaRepository _ventaRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public DashboardService(IventaRepository ventaRepository, IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        private IQueryable<Venta> retornarVentas (IQueryable<Venta> tablaVenta , int restarCantidadDias) 
        {

            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentasUltimaSemana() 
        {
         int total = 0;

            IQueryable<Venta> _ventaQuery = await _ventaRepository.consultar();

            if(_ventaQuery.Count() > 0) 
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                total = tablaVenta.Count();
            
            }
            return total;
        }
        private async Task<string> TotalIngresosUltimaSemana() 
        { 
        decimal resultado = 0;
        IQueryable<Venta> _ventaQuery = await _ventaRepository.consultar();

            if(_ventaQuery.Count() > 0) 
            
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                resultado = tablaVenta.Select(v => v.Total).Sum(v => v.Value);
            }
            return Convert.ToString(resultado, new CultureInfo("es-RD"));
        }
        private async Task<int> TotalProductos() 
        {
            IQueryable<Producto> _productoQuery = await _productoRepository.consultar();

            int total = _productoQuery.Count();
            return total;

        }

        private async Task<Dictionary<string,int>> VentasUltimaSemana() 
        {
         Dictionary<string,int> resultado = new Dictionary<string,int>();
         IQueryable<Venta> _ventaQuery = await _ventaRepository.consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = retornarVentas(_ventaQuery, -7);
                resultado = tablaVenta
                .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                .Select(dv => new { fecha = dv.Key.ToString("dd/MM/yyyy"), total = dv.Count() })
                .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);

            
        }
            return resultado;
    }

        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDashboard = new DashboardDTO();

            try
            {
                vmDashboard.TotalVentas = await TotalVentasUltimaSemana();
                vmDashboard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDashboard.TotalProductos = await TotalProductos();

                List<VentaSemanaDTO> listaventaSemana = new List<VentaSemanaDTO> ();

                foreach (KeyValuePair<string ,int> item in await VentasUltimaSemana()) 
                {
                    listaventaSemana.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        total = item.Value
                    });
                
                } 
            }
            catch 
            {
                throw;
            }
            return vmDashboard;
         }
    }

}