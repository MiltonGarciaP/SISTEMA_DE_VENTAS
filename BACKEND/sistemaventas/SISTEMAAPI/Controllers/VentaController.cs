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
    public class VentaController : ControllerBase
    {
        private readonly IVentaService ventaService;

        public VentaController(IVentaService ventaService)
        {
            this.ventaService = ventaService;
        }

        [HttpPost]
        [Route("Resgistrar")]
        public async Task<IActionResult> Registrar([FromBody] VentaDTO venta)
        {
            var rsp = new response<VentaDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await ventaService.Registrar(venta);
            }
            catch (Exception ex)
            {

                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarpor , string? numeroVenta, string? fechaincio, string? fechafin )
        {
            var rsp = new response<List<VentaDTO>>();
            numeroVenta = numeroVenta is null ? "" : numeroVenta;
            fechaincio = fechaincio is null ? "" : fechaincio;
            fechafin = fechafin is null ? "" : fechafin;
            try
            {
                rsp.status = true;
                rsp.Value = await ventaService.Historial(buscarpor, numeroVenta , fechaincio , fechafin);

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaincio, string? fechafin)
        {
            var rsp = new response<List<ReporteDTO>>();
           
            try
            {
                rsp.status = true;
                rsp.Value = await ventaService.Reporte(fechaincio, fechafin);

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

