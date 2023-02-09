using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi.Models
{
    public class Pedido
    {
        [Key]
        public int idPedido { get; set; }
		public int idEmpleadoPedido { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
		public DateTime fechaPedido { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
		public DateTime fechaEntrega { get; set; }
		public int Estado { get; set; }
        public int idUsuarioPedido { get; set; }
        [ForeignKey(nameof(idUsuarioPedido))]
        public Usuario usuario {get; set;}
		public string latitudPedido { get; set; }
        public string longitudPedido { get; set; }
  //      public string direccionPedido { get; set;}
        public double montoFinal { get; set; }

       
    }
}    