using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISTEMAVENTA.MODEL;

namespace SISTEMAVENTA.DAL.Modelos.interfaces
{
    public interface IventaRepository : IGenericRepository<Venta>
    {
        Task<Venta> Registrar(Venta modelo);

    }
}
