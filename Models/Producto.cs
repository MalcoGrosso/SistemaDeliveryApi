using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi.Models
{
    public class Producto
    {
        [Key]
        public int idProducto { get; set; }
		public string nombre { get; set; }
		public string imagen { get; set; }
		public Double precioProducto { get; set; }
		
    }
}    