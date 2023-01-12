using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi.Models
{
    public class DetallePedido
    {
        [Key]
        public int idDetallePedido { get; set; }
        public int idUsuarioDP { get; set; }
		public int idProductoDP { get; set; }
        public Double precioPedido { get; set; }
        public int IdentificadorDetallePedido { get; set; }
        
    }
}    