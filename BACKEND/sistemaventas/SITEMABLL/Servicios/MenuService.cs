using System;
using System.Collections.Generic;
using System.Linq;
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
    public class MenuService : IMenuService
    {
        private readonly IGenericRepository<Usuario> _UsuarioRepository;
        private readonly IGenericRepository<Menu> _menuRepository;
        private readonly IGenericRepository<MenuRol> _menuRolRepository;
        private readonly IMapper _mapper;

        public MenuService(IGenericRepository<Usuario> usuarioRepository, 
        IGenericRepository<Menu> menuRepository, IGenericRepository<MenuRol> menuRolRepository, IMapper mapper)
        {
            _UsuarioRepository = usuarioRepository;
            _menuRepository = menuRepository;
            _menuRolRepository = menuRolRepository;
            _mapper = mapper;
        }

        public async Task<List<MenuDTO>> Lista(int idUsuario)
        {
            IQueryable<Usuario> tbusuario = await _UsuarioRepository.consultar(u => u.IdUsuario == idUsuario);
            IQueryable<MenuRol> tbMenuRol = await _menuRolRepository.consultar();
            IQueryable<Menu> tbMenu = await _menuRepository.consultar();

            try 
            {
                IQueryable<Menu> tbresultado = (from u in tbusuario
                                                join mr in tbMenuRol on u.IdRol equals mr.IdRol
                                                join m in tbMenu on mr.IdMenu equals m.IdMenu
                                                select m).AsQueryable();

                var listaMenus = tbresultado.ToList();
                return _mapper.Map<List<MenuDTO>>(listaMenus);
            }
            catch 
            {

                throw;
            }
        }
    }
}
