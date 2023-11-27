using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMAVENTA.DTO
{
    public class ReporteDTO
    {
     public string? NumeroDocumento { get; set; }
     public string? TipoPago { get; set; }
     public string? FechaRegistro { get; set; }
     public string? TotalVenta { get; set; }
     public string? Producto { get; set; }
     public int? cantidad { get; set; }
     public string? precio { get; set; }
     public string? Total { get; set; }
    }
}
