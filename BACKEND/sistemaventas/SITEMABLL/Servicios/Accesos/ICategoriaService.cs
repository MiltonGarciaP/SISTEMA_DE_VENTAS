using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISTEMAVENTA.DTO;

namespace SITEMAVENTA.BLL.Servicios.Accesos
{
    public interface ICategoriaService
    {
        Task<List<CategoriaDTO>> Lista();
    }
}
