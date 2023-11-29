using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SISTEMAVENTA.DAL.Modelos.interfaces;
using SISTEMAVENTA.DTO;
using SISTEMAVENTA.MODEL;
using SITEMAVENTA.BLL.Servicios.Accesos;

namespace SITEMAVENTA.BLL.Servicios
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categorium> _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(IGenericRepository<Categorium> categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDTO>> Lista()
        {
           try 
            {
                var listacategorias = await _categoriaRepository.consultar();
                return _mapper.Map<List<CategoriaDTO>>(listacategorias.ToList());
            }
            catch 
            {
                throw;
            
            }
        }
    }
}
