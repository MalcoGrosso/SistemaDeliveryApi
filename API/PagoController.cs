using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeliveryApi_.Net_Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SistemaDeliveryApi.Models;
using Microsoft.AspNetCore.Http.Features;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaDeliveryApi_.Net_Core.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PagoController : Controller
    {
        private readonly DataContext contexto;
        private readonly IWebHostEnvironment environment;

        public PagoController(DataContext contexto, IWebHostEnvironment environment)
        {
            this.contexto = contexto;
            this.environment = environment;
        }

        // GET: api/<controller>
        [HttpGet] // obtener todos los Pagos
        public async Task<IActionResult> Get()
        {
            try
            {
                var Pago = contexto.Pago;
                return Ok(Pago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CrearPago")] // Agrega a la base el pedido

        // Agregar un if para preguntar el estado del pedido si el pedido anterior esta "terminado" que se cree uno nuevo, sino que se edite el ultimo pedido
        public async Task<ActionResult<Pago>> Post([FromBody] Pago Pago)
        {
                var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
				var LocalPort = feature?.LocalPort.ToString();
				var ipv4 = HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
				var ipConexion = "http://" + ipv4 + ":" + LocalPort + "/";

            try
            {

                 var usuario = User.Identity.Name;
                Usuario usuario1 = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario); 
                Pago.idUsuarioPago = usuario1.idUsuario; 
                var ped = contexto.Pedidos.Where(x => x.idUsuarioPedido == usuario1.idUsuario);
                var ultiP = ped.Max(x => x.idPedido);
                var consulta = contexto.Pedidos.Where(x => x.idPedido == ultiP );
                Pago.idPedidoPago = consulta.First().idPedido;
               
                    contexto.Add(Pago);
                    await contexto.SaveChangesAsync();
                    return Pago;
  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }








        
        
	}
}