using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace SISTEMAVENTA.DAL.Modelos.interfaces
{
    public interface IGenericRepository<Tmodel> where Tmodel : class
    {
        Task<Tmodel> obtener(Expression<Func<Tmodel, bool>> filtro);
        Task<Tmodel> crear(Tmodel modelo);
        Task<bool> Editar(Tmodel modelo);
        Task<bool> Eliminar(Tmodel modelo);
        Task<IQueryable<Tmodel>> consultar(Expression<Func<Tmodel, bool>> filtro = null);

    }
}
