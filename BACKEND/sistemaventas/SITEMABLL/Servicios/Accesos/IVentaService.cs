using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISTEMAVENTA.DTO;

namespace SITEMAVENTA.BLL.Servicios.Accesos
{
    public interface IVentaService
    {
        Task<VentaDTO> Registrar(VentaDTO modelo);
        Task<List<VentaDTO>> Historial(string buscarPor, string numeroVentak, string FechaInicio, string FechaFin);
        Task<List<ReporteDTO>> Reporte(string FechaInicio, string FechaFin);
        
    }
}
