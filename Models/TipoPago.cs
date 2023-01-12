using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi.Models
{
    public class TipoPago
    {
        [Key]
        public int idTipoPago { get; set; }
		public string metodo { get; set; }
        public string nombrePagoD { get; set; }

    }
}    