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
    public class PedidosController : Controller
    {
        private readonly DataContext contexto;
        private readonly IWebHostEnvironment environment;

        public PedidosController(DataContext contexto, IWebHostEnvironment environment)
        {
            this.contexto = contexto;
            this.environment = environment;
        }

        // GET: api/<controller>
        [HttpGet] // obttener todos los Pedidos
        public async Task<IActionResult> Get()
        {
            try
            {
                var Pedidos = contexto.Pedidos;
                return Ok(Pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost("Crear")] // Agrega a la base el pedido

        // Agregar un if para preguntar el estado del pedido si el pedido anterior esta "terminado" que se cree uno nuevo, sino que se edite el ultimo pedido
        public async Task<ActionResult<Pedido>> Post([FromBody] Pedido Pedidos)
        {
                var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
				var LocalPort = feature?.LocalPort.ToString();
				var ipv4 = HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
				var ipConexion = "http://" + ipv4 + ":" + LocalPort + "/";

            try
            {

                 var usuario = User.Identity.Name;
                Usuario usuario1 = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario); 
                Pedidos.idUsuarioPedido = usuario1.idUsuario; 
                var ped = contexto.Pedidos.Where(x => x.idUsuarioPedido == usuario1.idUsuario);
                var ultiP = ped.Max(x => x.idPedido);
                var consulta = contexto.Pedidos.Where(x => x.idPedido == ultiP && x.Estado == 1);
             //   var z = contexto.Pedidos.Select(x => x.idPedido == ultiP && x.Estado == 1);
                var consulta2 = contexto.Pedidos.Where(x => x.idPedido == ultiP);
               if(consulta.Any()){
                    contexto.Add(Pedidos);
                    await contexto.SaveChangesAsync();
                    return Pedidos;
                    

               }else{

                    Pedidos = consulta2.Single();
                    return Pedidos;

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<controller>
		[HttpPut("ModificarUsuario")] //actualizar la informacion del pedido del usuario
		public async Task<IActionResult> Modificar( [FromBody] Pedido Pedido)
		
		{
			try
			{
				
				var usuario = User.Identity.Name;
				
					
					Usuario original = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario);
                    Pedido.idUsuarioPedido = original.idUsuario;
                    Pedido.Estado = 1;

					
					contexto.Pedidos.Update(Pedido);	
					await contexto.SaveChangesAsync();
					return Ok(Pedido);
			
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}




        
        
	}
}