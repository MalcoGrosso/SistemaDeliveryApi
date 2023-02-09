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
    public class ProductosController : Controller
    {
        private readonly DataContext contexto;
        private readonly IWebHostEnvironment environment;

        public ProductosController(DataContext contexto, IWebHostEnvironment environment)
        {
            this.contexto = contexto;
            this.environment = environment;
        }

        // GET: api/<controller>
        [HttpGet] // obtener todos los Productos
        public async Task<IActionResult> Get()
        {
            try
            {
                var Productos = contexto.Productos;
                return Ok(Productos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ProductosPedidos")] // obtener todos los Productos
        public async Task<object> ProductosPedidos()
        {
            try
            {
                
                var usuario = User.Identity.Name;
                Usuario usuario1 = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario); 
                var Produc = contexto.Productos.ToArray();
         //       var pedido = contexto.Pedidos.Where(x => x.idUsuarioPedido == usuario1.idUsuario);
                var pedido = contexto.Pedidos.Include(x=> x.usuario).Where(x => x.usuario.idUsuario == usuario1.idUsuario);
                var ultiP = pedido.Max(x => x.idPedido);
                var consulta1 = contexto.Pedidos.Where(x=> x.idPedido ==  ultiP).Single();
                if(consulta1.Estado == 1){
                    return Ok(Produc);
                }
                else{
                var var1  = contexto.Productos.Count();
                var var2 = var1 + 1;
                for (int i = 1, x = 0; i < var2 && x < var1; i++, x++)
				{
					var contador = contexto.DetallePedido.Count(x=>  x.IdentificadorDetallePedido == ultiP && x.idUsuarioDP == usuario1.idUsuario &&  x.idProductoDP == i);
                    var Productos = contexto.Productos.Where(x=> x.idProducto == i );
                    var producto = Productos.Single();
                    producto.cantidad = contador;
                    Produc[x] = producto;
				}
                
                return Ok(Produc);
            }}
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        
        
	}
}
