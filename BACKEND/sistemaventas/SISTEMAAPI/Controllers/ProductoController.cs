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
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoservices;

        public ProductoController(IProductoService productoservices)
        {
            _productoservices = productoservices;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new response<List<ProductoDTO>>();


            try
            {
                rsp.status = true;
                rsp.Value = await _productoservices.Lista();

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
      
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ProductoDTO producto)
        {
            var rsp = new response<ProductoDTO>();

            try
            {
                rsp.status = true;
                rsp.Value = await _productoservices.Crear(producto);
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
        public async Task<IActionResult> Editar([FromBody] ProductoDTO producto)
        {
            var rsp = new response<bool>();

            try
            {
                rsp.status = true;
                rsp.Value = await _productoservices.Editar(producto);
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
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new response<bool>();

            try
            {
                rsp.status = true;
                rsp.Value = await _productoservices.Eliminar(id);
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
