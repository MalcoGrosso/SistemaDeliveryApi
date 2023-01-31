using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi.Models
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }
		[Required]
		public string? Nombre { get; set; }
		[Required]
		public string? Apellido { get; set; }
		[Required]
		public string? Direccion { get; set; }
        [Required, EmailAddress]
		public string? Email { get; set; }
        public string? Password { get; set; }
		public string? Telefono { get; set; }
        

       
	}
}