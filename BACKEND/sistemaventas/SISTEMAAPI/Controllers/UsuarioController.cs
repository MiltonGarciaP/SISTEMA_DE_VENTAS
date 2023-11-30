using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SISTEMAAPI.Utility;
using SISTEMAVENTA.DTO;
using SISTEMAVENTA.UTILITY;
using SITEMAVENTA.BLL.Servicios;
using SITEMAVENTA.BLL.Servicios.Accesos;

namespace SISTEMAAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _Usuarioservices;

        public UsuarioController(IUsuarioServices usuarioservices)
        {
            _Usuarioservices = usuarioservices;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new response<List<UsuarioDTO>>();


            try
            {
                rsp.status = true;
                rsp.Value = await _Usuarioservices.Lista();

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
        [HttpPost]
        [Route("InciarSesion")]
        public async Task<IActionResult>InciarSesion([FromBody] LoginDTO login) 
        {
            var rsp = new response<SesionDTO>();

            try 
            {
                rsp.status = true;
                rsp.Value = await _Usuarioservices.ValidarCredenciales(login.Correo, login.Clave);
            }catch(Exception ex) 
            {

                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new response<UsuarioDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await _Usuarioservices.Crear(usuario);
            }
            catch (Exception ex)
            {

                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] UsuarioDTO usuario)
        {
            var rsp = new response<bool>();

            try
            {
                rsp.status = true;
                rsp.Value = await _Usuarioservices.Editar(usuario);
            }
            catch (Exception ex)
            {

                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar( int id)
        {
            var rsp = new response<bool>();

            try
            {
                rsp.status = true;
                rsp.Value = await _Usuarioservices.Eliminar(id);
            }
            catch (Exception ex)
            {

                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }

    }
}
