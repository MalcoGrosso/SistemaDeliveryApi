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

        public async Task<ActionResult<Pago>> Post([FromBody] Pago Pago)
        {
             

            try
            {

                 var usuario = User.Identity.Name;
                Usuario usuario1 = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario); 
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


        [HttpGet("ConsultarPago")] // obtener todos los Pagos
        public async Task<IActionResult> ConsulPago()
        {
            try
            {
                var usuario = User.Identity.Name;
                var user = await contexto.Usuarios.FirstOrDefaultAsync(x => x.Email == usuario);
                var pago = contexto.Pago.Where(x => x.usuario.idUsuario == user.idUsuario);
                var check = await pago.FirstOrDefaultAsync();
                var ultiP = pago.Max(x => x.idPago);
                var pedido = contexto.Pedidos.Where(x => x.idUsuarioPedido == user.idUsuario);
                var ultiPedido = pedido.Max(x => x.idPedido);
                var ultimoPedidoPago = contexto.Pago.Where(x => x.usuario.idUsuario == user.idUsuario && x.idPedidoPago == ultiPedido);
                
                if(ultimoPedidoPago == null || ultimoPedidoPago.Count() == 0){
                    
                    var Pagos = await ultimoPedidoPago.FirstOrDefaultAsync(x => x.idPago == ultiP);
                    return Ok(Pagos);
                    
                }else{
                
                var Pagos = await ultimoPedidoPago.FirstOrDefaultAsync(x => x.idPago == ultiP);
                return Ok(Pagos);
                    }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        
        
	}
}