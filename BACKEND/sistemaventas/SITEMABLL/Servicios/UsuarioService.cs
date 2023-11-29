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
    public class UsuarioService : IUsuarioServices
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IGenericRepository<Usuario> usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<List<UsuarioDTO>> Lista()
        {
            try 
            {
                var queryUsuario = await _usuarioRepository.consultar();
                var listaUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).ToList();
                return _mapper.Map<List<UsuarioDTO>>(listaUsuario);
            } catch 
            {
                throw;
            }
        }
        public async Task<SesionDTO>  ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuario = await _usuarioRepository.consultar
                    (u => u.Correo == correo &&
                     u.Clave == clave);

                if (queryUsuario.FirstOrDefault() == null)
                    throw new TaskCanceledException("El usuario no existe");

                 Usuario devolverUsuario = queryUsuario.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<SesionDTO>(devolverUsuario);
            }
            catch 
            {
                throw;
            }
        }
        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try 
            {
                var CreadoUsuario = await _usuarioRepository.crear(_mapper.Map<Usuario>(modelo));

                if (CreadoUsuario.IdUsuario == 0)
                    throw new TaskCanceledException("El usuario no existe");
                var query = await _usuarioRepository.consultar(u => u.IdUsuario == CreadoUsuario.IdUsuario);
                CreadoUsuario = query.Include(rol => rol.IdRolNavigation).First();

                return _mapper.Map<UsuarioDTO>(CreadoUsuario);
            }
            catch 
            {

                throw;
            }
        }
        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try {

                var usuarioModelo = _mapper.Map<Usuario>(modelo);
                var usuarioEncontrado = await _usuarioRepository.obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);

                if (usuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");

                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.EsActivo = usuarioModelo.EsActivo;

                bool respuesta = await _usuarioRepository.Editar(usuarioEncontrado);

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
                var UsuarioEncontrado = await _usuarioRepository.obtener(u => u.IdUsuario == id);
                if (UsuarioEncontrado == null)
                    throw new TaskCanceledException("El usuario no existe");
                bool respuesta = await _usuarioRepository.Eliminar(UsuarioEncontrado);
                if (!respuesta)
                    throw new TaskCanceledException("No se pudo eliminar");

                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        Task<List<SesionDTO>> IUsuarioServices.ValidarCredenciales(string correo, string clave)
        {
            throw new NotImplementedException();
        }
    }
}
