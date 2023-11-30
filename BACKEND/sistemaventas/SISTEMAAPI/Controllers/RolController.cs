using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SISTEMAAPI.Utility;
using SISTEMAVENTA.DTO;
using SISTEMAVENTA.UTILITY;
using SITEMAVENTA.BLL.Servicios.Accesos;

namespace SISTEMAAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista() 
        {
            var rsp = new response<List<RolDTO>>();


            try 
            {
                rsp.status = true;
                rsp.Value = await _rolService.Lista();
            
            }catch (Exception ex) 
            {
             rsp.status=false;
             rsp.msg = ex.Message;
            
            }
            return Ok(rsp);
        }

    }
}
