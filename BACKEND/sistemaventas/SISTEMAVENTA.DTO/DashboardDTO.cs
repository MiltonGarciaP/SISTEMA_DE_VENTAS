using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISTEMAVENTA.DTO
{
    public class DashboardDTO
    {
        public int TotalVentas { get; set; }
        public int TotalIngresos { get; set; }
        public List<DashboardDTO> Dashboard { get; set; }
    }
}
