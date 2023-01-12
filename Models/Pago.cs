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
        public int idEmpleadoPago { get; set; }
        public int idPedidoPago { get; set; }
		public int EstadoPago { get; set; }
        [NotMapped]
        public DateTime FechaPago { get; set; }
        public int idTipoPagoP { get; set; }
       
    }
}    