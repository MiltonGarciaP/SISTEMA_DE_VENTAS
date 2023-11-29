using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SISTEMAVENTA.DAL.Modelos.interfaces;
using SISTEMAVENTA.DTO;
using SISTEMAVENTA.MODEL;
using SITEMAVENTA.BLL.Servicios.Accesos;

namespace SITEMAVENTA.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IventaRepository _ventaRepository;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepository;
        private readonly IMapper _mapper;

        public VentaService(IventaRepository ventaRepository, IGenericRepository<DetalleVenta> detalleVentaRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _detalleVentaRepository = detalleVentaRepository;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepository.Registrar(_mapper.Map<Venta>(modelo));

                if (ventaGenerada.IdVenta == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVentak, string FechaInicio, string FechaFin)
        {
            IQueryable<Venta> query = await _ventaRepository.consultar();
            var listaResltado = new List<Venta>();
            try 
            {
                if (buscarPor == "fecha")
                {
                    DateTime fecha_inicio = DateTime.ParseExact(FechaInicio, "dddd/MM/yyyy", new CultureInfo("es-RD"));
                    DateTime fecha_fin = DateTime.ParseExact(FechaFin, "dddd/MM/yyyy", new CultureInfo("es-RD"));

                    listaResltado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                    v.FechaRegistro.Value.Date >= fecha_fin.Date
                    ).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation)
                    .ToListAsync();

                }
                else 
                {
                    listaResltado = await query.Where(v => v.NumeroDocumento == numeroVentak
                     ).Include(dv => dv.DetalleVenta)
                     .ThenInclude(p => p.IdProductoNavigation)
                     .ToListAsync();

                }
            }
            catch 
            {
                throw;
            }
            return _mapper.Map<List<VentaDTO>>(listaResltado);
        }

        
        public async Task<List<ReporteDTO>> Reporte(string FechaInicio, string FechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleVentaRepository.consultar();
            var listaResltado = new List<DetalleVenta>();
            try
            {
                DateTime fecha_inicio = DateTime.ParseExact(FechaInicio, "dddd/MM/yyyy", new CultureInfo("es-RD"));
                DateTime fecha_fin = DateTime.ParseExact(FechaFin, "dddd/MM/yyyy", new CultureInfo("es-RD"));

                listaResltado = await query
                    .Include(p =>p.IdVentaNavigation)
                    .Include(p =>p.IdVentaNavigation)
                    .Where(dv =>
                     dv.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                     dv.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_fin.Date
                    ).ToListAsync();
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<ReporteDTO>>(listaResltado);
        }
    }
}
