using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi.Models
{
    public class usuario
    {
        [Key]
        [Display(Name = "Código")]
        public int idUsuario { get; set; }
		[Required]
		public string nombre { get; set; }
		[Required]
		public string apellido { get; set; }
		[Required]
		public string direccion { get; set; }
        [Required, EmailAddress]
		public string email { get; set; }
        public string password { get; set; }
		public string telefono { get; set; }
        public string avatar { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
		
       
	}
}