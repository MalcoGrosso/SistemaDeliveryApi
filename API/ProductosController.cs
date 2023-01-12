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



        
        
	}
}
