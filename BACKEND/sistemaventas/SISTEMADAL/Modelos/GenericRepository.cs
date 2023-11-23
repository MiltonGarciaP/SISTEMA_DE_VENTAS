using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISTEMAVENTA.DAL.Modelos.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace SISTEMAVENTA.DAL.Modelos
{
    public class GenericRepository<Tmodelo> : IGenericRepository<Tmodelo> where Tmodelo : class
    {

        private readonly SistemaContext _dbContext;

        public GenericRepository(SistemaContext dbContext) 
        {
        
          _dbContext = dbContext;
        }

        public async Task<Tmodelo> obtener(Expression<Func<Tmodelo, bool>> filtro)
        {
            try
            {
                Tmodelo modelo = await _dbContext.Set<Tmodelo>().FirstOrDefaultAsync(filtro);
                return modelo;
            }
            catch {
                throw;
            }
        }

        public async Task<Tmodelo> crear(Tmodelo modelo)
        {
            try
            {
                _dbContext.Set<Tmodelo>().Add(modelo);
                await _dbContext.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Editar(Tmodelo modelo)
        {
            try
            {
                _dbContext.Set<Tmodelo>().Update(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

    public async Task<bool> Eliminar(Tmodelo modelo)
    {
            try
            {
                _dbContext.Set<Tmodelo>().Remove(modelo);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    public async Task<IQueryable<Tmodelo>> consultar(Expression<Func<Tmodelo, bool>> filtro = null)
        {
            try
            {

                IQueryable<Tmodelo> queryModelo = filtro == null ? _dbContext.Set<Tmodelo>() : _dbContext.Set<Tmodelo>().Where(filtro);
                return queryModelo;
            }
            catch
            {
                throw;
            }
        }
      
    }
    
}
