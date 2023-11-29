using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var querryproducto = await _productoRepository.consultar();
                var listaProductos = querryproducto.Include(cat => cat.IdCategoriaNavigation).ToList();
                return _mapper.Map<List<ProductoDTO>>(listaProductos.ToList());
            }
            catch 
            
            {
                throw;
            }


        }
        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepository.crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.IdProducto == 0)
                    throw new TaskCanceledException("No se puede crear");
                return _mapper.Map<ProductoDTO>(modelo);
            }
            catch

            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);
                var productoEncontrado = await _productoRepository.obtener(
                 u => u.IdProducto == productoModelo.IdProducto
                    );

                if (productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                bool respuesta = await _productoRepository.Editar(productoEncontrado);

                if (!respuesta)
                    throw new TaskCanceledException("No se pudo editar");

                return respuesta;
            }
            catch

            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await _productoRepository.obtener(p => p.IdProducto == id);

                if(productoEncontrado == null)
                    throw new TaskCanceledException("El producto no existe");

                bool respuesta = await _productoRepository.Eliminar(productoEncontrado);

                if (respuesta)
                    throw new TaskCanceledException("No se pudo eliminar");

                return respuesta;
            }
            catch

            {
                throw;
            }
        }

        
    }
}
