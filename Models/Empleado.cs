using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi.Models
{
    public class Empleado
    {
        [Key]
        [Display(Name = "Código")]
        public int idEmpleados { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Direccion { get; set; }
        [Required, EmailAddress]
		public string Email { get; set; }
        public string Password { get; set; }
		public string Telefono { get; set; }
        public string Avatar { get; set; }
        public string Tipo { get; set; }
        
       
	}
}