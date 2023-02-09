using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi.Models
{
    public class Pago
    {
        [Key]
        public int idPago { get; set; }
		public int idUsuarioPago { get; set; }
        [ForeignKey(nameof(idUsuarioPago))]
        public Usuario usuario {get; set;}
        public int idPedidoPago { get; set; }
        [ForeignKey(nameof(idPedidoPago))]
        public Pedido pedido {get; set;} 
        [NotMapped]
        public DateTime FechaPago { get; set; }
        public int idTipoPagoP { get; set; }
        [ForeignKey(nameof(idTipoPagoP))]
        public TipoPago tipoPago {get; set;}
       
    }
}    


