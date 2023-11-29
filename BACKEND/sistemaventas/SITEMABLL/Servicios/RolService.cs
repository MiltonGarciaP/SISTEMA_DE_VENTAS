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
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly IMapper _mapper;

        public RolService(IGenericRepository<Rol> rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try{

                var listRoles = await _rolRepository.consultar();
                return _mapper.Map<List<RolDTO>>(listRoles.ToList());
            } 
            catch{
                throw;
            }
        }
    }
}
