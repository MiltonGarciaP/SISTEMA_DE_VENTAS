using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISTEMAVENTA.DAL.Modelos.interfaces;
using SISTEMAVENTA.MODEL;

namespace SISTEMAVENTA.DAL.Modelos
{
    public class ventaModelo : GenericRepository<Venta> , IventaRepository
    {
        private readonly SistemaContext _dbcontext;

        public ventaModelo(SistemaContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using (var trasaction = _dbcontext.Database.BeginTransaction()) 
            {
                try {

                    foreach (DetalleVenta dv in modelo.DetalleVenta) 
                    {
                     Producto producto = _dbcontext.Productos.Where(p=> p.IdProducto == dv.IdProducto).First();
                        producto.Stock = producto.Stock = dv.Cantidad;
                        _dbcontext.Productos.Update(producto);
                     
                    }
                    await _dbcontext.SaveChangesAsync();
                    NumeroDocumento correlativo = _dbcontext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;
                    _dbcontext.NumeroDocumentos.Update(correlativo);
                    await _dbcontext.SaveChangesAsync();
                    int cantiadadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", cantiadadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - cantiadadDigitos);
                    modelo.NumeroDocumento = numeroVenta;

                    await _dbcontext.Venta.AddAsync(modelo);
                    await _dbcontext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    trasaction.Commit();

                }catch 
                { 
                 trasaction.Rollback();
                    throw;
                }
                return ventaGenerada;
            
            }
        }
    }
}
