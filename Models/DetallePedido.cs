using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi.Models
{
    public class DetallePedido
    {
        [Key]
        public int idDetallePedido { get; set; }
        public int idUsuarioDP { get; set; }
        [ForeignKey(nameof(idUsuarioDP))]
        public Usuario usuario {get; set;}
		public int idProductoDP { get; set; }
        [ForeignKey(nameof(idProductoDP))]
        public Producto producto{get; set;}
        public Double precioPedido { get; set; }
        public int IdentificadorDetallePedido { get; set; }
        [ForeignKey(nameof(IdentificadorDetallePedido))]
        public Pedido pedido {get; set;}
    }
}    



