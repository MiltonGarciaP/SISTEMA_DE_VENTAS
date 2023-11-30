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
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuservie;

        public MenuController(IMenuService menuservie)
        {
            _menuservie = menuservie;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista(int idUsuario)
        {
            var rsp = new response<List<MenuDTO>>();


            try
            {
                rsp.status = true;
                rsp.Value = await _menuservie.Lista(idUsuario);

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
