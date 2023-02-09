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
    public class DetallePedidoController : Controller
    {
        private readonly DataContext contexto;
        private readonly IWebHostEnvironment environment;

        public DetallePedidoController(DataContext contexto, IWebHostEnvironment environment)
        {
            this.contexto = contexto;
            this.environment = environment;
        }

        [HttpPost("AgregarDetallePedido")] // Agrega a la base el detalle del pedido
        public async Task<ActionResult<DetallePedido>> Post([FromBody] DetallePedido detallePedido)
        {
                var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
				var LocalPort = feature?.LocalPort.ToString();
				var ipv4 = HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
				var ipConexion = "http://" + ipv4 + ":" + LocalPort + "/";

            try
            {

                 var usuario = User.Identity.Name;
                Usuario usuario1 = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario); 
           //     Pedido pedido1 = await contexto.Pedidos.OrderBy().LastOrDefaultAsync(x => x.idUsuarioPedido == usuario1.idUsuario);
                var pedido = contexto.Pedidos.Where(x => x.idUsuarioPedido == usuario1.idUsuario);
                var ultiP = pedido.Max(x => x.idPedido);
                var consulta = contexto.Pedidos.Where(x => x.idPedido == ultiP && x.idUsuarioPedido == usuario1.idUsuario);
                detallePedido.idUsuarioDP = usuario1.idUsuario;     
                detallePedido.IdentificadorDetallePedido = consulta.Single().idPedido;

                    contexto.Add(detallePedido);
                    await contexto.SaveChangesAsync();
                    return detallePedido;


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("QuitarDetallePedido")] // Quita de la base el detalle del pedido
        public async Task<ActionResult<DetallePedido>> Quitar([FromBody] DetallePedido detallePedido)
        {
                var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
				var LocalPort = feature?.LocalPort.ToString();
				var ipv4 = HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
				var ipConexion = "http://" + ipv4 + ":" + LocalPort + "/";

            try
            {

                 var usuario = User.Identity.Name;
                Usuario usuario1 = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario); 
                var pedidoDetallesUsuario = contexto.DetallePedido.Where(x => x.idUsuarioDP == usuario1.idUsuario);
                var ultiP = pedidoDetallesUsuario.Max(x => x.IdentificadorDetallePedido);
                var consulta = contexto.DetallePedido.Where(x => x.IdentificadorDetallePedido == ultiP && x.idUsuarioDP == usuario1.idUsuario && x.idProductoDP == detallePedido.idProductoDP);
                var consultaIdDetallePedido = consulta.Min(x=> x.idDetallePedido);
                var borrar = consulta.First(x => x.idDetallePedido == consultaIdDetallePedido);
                    if (borrar == null ){
						return NotFound();
                    }
                    else{
                    contexto.Remove(borrar);
                    await contexto.SaveChangesAsync();
                    return borrar;
                    }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // GET: api/<controller>
        [HttpGet] // obttener todos los Detalles
        public async Task<IActionResult> Get()
        {
            try
            {
                var DetallePedidos = contexto.DetallePedido;
                return Ok(DetallePedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("DetallePedido")]// Listar el pedido
        public async Task<ActionResult<List<DetallePedido>>> GetDetallePedido()
        {

            try
            {
                var usuario = User.Identity.Name;
                var user = await contexto.Usuarios.FirstOrDefaultAsync(x => x.Email == usuario);
                var deta = contexto.DetallePedido.Where(x => x.idUsuarioDP == user.idUsuario || x.IdentificadorDetallePedido == 1);
        
                    return Ok(deta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

/*
        [HttpGet("CantidadPedido/{idProductoDP}")]// Listar el pedido
        public async Task<IActionResult> GetCantidadDetallePedido(int idProductoDP)
        {

            try
            {
                 var usuario = User.Identity.Name;
                Usuario usuario1 = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario); 
           //     Pedido pedido1 = await contexto.Pedidos.OrderBy().LastOrDefaultAsync(x => x.idUsuarioPedido == usuario1.idUsuario);
                var pedido = contexto.Pedidos.Where(x => x.idUsuarioPedido == usuario1.idUsuario);
                var ultiP = pedido.Max(x => x.idPedido);
                var contador = contexto.DetallePedido.Count(x=> x.idProductoDP == idProductoDP && x.IdentificadorDetallePedido == ultiP && x.idUsuarioDP == usuario1.idUsuario);
                    return Ok(contador);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
*/


        [HttpPost("CantidadPedido")]// Listar el pedido
        public async Task<ActionResult<DetallePedido>> GetCantidadDetallePedido([FromBody] DetallePedido detallePedido)
        {

            try
            {
                 var usuario = User.Identity.Name;
                Usuario usuario1 = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario); 
           //     Pedido pedido1 = await contexto.Pedidos.OrderBy().LastOrDefaultAsync(x => x.idUsuarioPedido == usuario1.idUsuario);
                var pedido = contexto.Pedidos.Where(x => x.idUsuarioPedido == usuario1.idUsuario);
                var ultiP = pedido.Max(x => x.idPedido);
                var contador = contexto.DetallePedido.Count(x=> x.idProductoDP == detallePedido.idProductoDP && x.IdentificadorDetallePedido == ultiP && x.idUsuarioDP == usuario1.idUsuario);
                detallePedido.idUsuarioDP = contador;
                    return Ok(detallePedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

/*
        // GET: api/<controller>
        [HttpGet("todoVerDetallePedido")] // obtener todos los Pedidos
        public async Task<IActionResult> obtenerVerPedidoDetalle()
        {
            try
            {
                var DetallePedido = contexto.DetallePedido;
                return Ok(DetallePedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

*/



        [HttpGet("todoVerDetallePedido/{id}")]// Listar contratos por inmuebles     // Listar detalle de pedidos por pedidos
        public async Task<ActionResult<List<Pedido>>> obtenerVerPedidoDetalle(int id)
        {

            try
            {
                var usuario = User.Identity.Name;
                var usuario1 = await contexto.Usuarios.FirstOrDefaultAsync(x => x.Email == usuario);
                var Pedido = contexto.DetallePedido.Where(x => x.pedido.idUsuarioPedido == usuario1.idUsuario && x.pedido.idPedido == id).Include(l => l.producto).ToList();
        
                    return Ok(Pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        
        
	}
}