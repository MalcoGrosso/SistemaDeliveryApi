using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SistemaDeliveryApi_.Net_Core.Models;
using SistemaDeliveryApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http.Features;
using Org.BouncyCastle.Asn1.Ocsp;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SistemaDeliveryApi_.Net_Core.Api
{
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
	public class UsuariosController : ControllerBase//
	{
		private readonly DataContext contexto;
		private readonly IConfiguration config;
		

		public UsuariosController(DataContext contexto, IConfiguration config)
		{
			this.contexto = contexto;
			this.config = config;
			
		}
		// GET: api/<controller>
		[HttpGet] //obtener todos los Usuarios
		public async Task<ActionResult<Usuario>> Get()
		{
			try
			{
				
				var usuario =  User.Identity.Name;
				var usu = await contexto.Usuarios.SingleOrDefaultAsync(x => x.Email == usuario);
				return Ok(usu);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// POST api/<controller>/login
		[HttpPost("login")] // Login de la aplicacion
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginView loginView)
		{
			try
			{
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: loginView.Password,
					salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));
				var p = await contexto.Usuarios.FirstOrDefaultAsync(x =>  x.Email == loginView.Usuario);
				if (p == null || p.Password != hashed)
				{
					return BadRequest("Nombre de usuario o clave incorrecta");
				} 
				else
				{
					var key = new SymmetricSecurityKey(
						System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
					var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, p.Email),
						new Claim("FullName", p.Nombre + " " + p.Apellido),
						new Claim("id", p.idUsuario + " " ),
						new Claim(ClaimTypes.Role, "Usuario"),
					};

					var token = new JwtSecurityToken(
						issuer: config["TokenAuthentication:Issuer"],
						audience: config["TokenAuthentication:Audience"],
						claims: claims,
						expires: DateTime.Now.AddMinutes(600),
						signingCredentials: credenciales
					);
					return Ok(new JwtSecurityTokenHandler().WriteToken(token));
				}
			
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// PUT api/<controller>
		[HttpPut("Actualizar")] //actualizar la informacion del usuario
		public async Task<IActionResult> Actualizar( [FromBody] Usuario entidad)
		
		{
			try
			{
				
				var usuario = User.Identity.Name;
				if (ModelState.IsValid)
				{
					
					Usuario original = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == usuario);
					entidad.idUsuario = original.idUsuario;

					if (String.IsNullOrEmpty(entidad.Password) )
					{
						entidad.Password = original.Password;
					}
					else
					{
						entidad.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
							password: entidad.Password,
							salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
							prf: KeyDerivationPrf.HMACSHA1,
							iterationCount: 1000,
							numBytesRequested: 256 / 8));
					}
					contexto.Usuarios.Update(entidad);	
					await contexto.SaveChangesAsync();
					return Ok(entidad);
					
		
				}
				return BadRequest();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}



		// GET: api/<controller>
		[HttpGet("mail")] // envio del segundo mail con la contraseña sin hashear
		public async Task<ActionResult> mail()
		{
			try
			{
				

				var perfil = new{
				Email =  User.Identity.Name,
				Nombre = User.Claims.First(x=> x.Type == "FullName").Value,
				Rol = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value
				};

				Random rand        = new Random(Environment.TickCount);
				string randomChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
				string nuevaClave  = "";
				for (int i = 0; i < 8; i++)
				{
					nuevaClave += randomChars[rand.Next(0, randomChars.Length)];
				}

				String nuevaClaveSin = nuevaClave;

				nuevaClave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
							password: nuevaClave,
							salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
							prf: KeyDerivationPrf.HMACSHA1,
							iterationCount: 1000,
							numBytesRequested: 256 / 8));

				
				Usuario original = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == perfil.Email);
				original.Password = nuevaClave;
				contexto.Usuarios.Update(original);	
				await contexto.SaveChangesAsync();	
				
				var message = new MimeKit.MimeMessage();
				message.To.Add(new MailboxAddress(perfil.Nombre, "thecastleofilusion@gmail.com")); // original.Email  
				message.From.Add(new MailboxAddress("Sistema Delivery", "thecastleofilusion@gmail.com")); // "SistemaDelivery@DominioPropio.com"
				message.Subject = "Testing";
				message.Body = new TextPart("html")
				{
					Text = @$"<h1>Hola {perfil.Nombre}</h1>
					<p>Bienvenido, esta es su nueva Clave {nuevaClaveSin}</p>",					
				};

				message.Headers.Add("Encabezado", "Valor");
				MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
				client.ServerCertificateValidationCallback = (object sender,
				System.Security.Cryptography.X509Certificates.X509Certificate certificate,
				System.Security.Cryptography.X509Certificates.X509Chain chain,
				System.Net.Security.SslPolicyErrors sslPolicyErrors) => { return true;};
				client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.Auto);
				client.Authenticate(config["SMTPUser"], config["SMTPPass"]);

				await client.SendAsync(message);
				return Ok("Su contraseña se ha restablecido, por favor verifique su email para recibir la nueva contraseña");
			
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpPost("emailPedido")] // envio del primer mail con el token para acceder al metodo mail
		[AllowAnonymous]
		public async Task<IActionResult> GetByEmail([FromForm]string email)
		{
				var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
				var LocalPort = feature?.LocalPort.ToString();
				var ipv4 = HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
				var ipConexion = "http://" + ipv4 + ":" + LocalPort;
			try
			{	

				var entidad1 = await contexto.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
			//	var entidad= new PropietarioView(entidad1);
				var key = new SymmetricSecurityKey(
						System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
					var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, entidad1.Email),
						new Claim("FullName", entidad1.Nombre + " " + entidad1.Apellido),
						new Claim("id", entidad1.idUsuario + " " ),
						new Claim(ClaimTypes.Role, "Usuario"),
					};

					var token = new JwtSecurityToken(
						issuer: config["TokenAuthentication:Issuer"],
						audience: config["TokenAuthentication:Audience"],
						claims: claims,
						expires: DateTime.Now.AddMinutes(600),
						signingCredentials: credenciales
					);
					var to = new JwtSecurityTokenHandler().WriteToken(token);
					
					var direccion =  ipConexion + "/API/Usuarios/mail?access_token=" + to;
					try
			{
				
	
				var message = new MimeKit.MimeMessage();
				message.To.Add(new MailboxAddress(entidad1.Nombre, "thecastleofilusion@gmail.com")); // entidad1.email 
				message.From.Add(new MailboxAddress("Sistema Delivery", "thecastleofilusion@gmail.com")); // "SistemaDelivery@DominioPropio.com"
				message.Subject = "Testing";
				message.Body = new TextPart("html")

				
				{
					Text = @$"<h1>Hola {entidad1.Nombre} {entidad1.Apellido} </h1>
					<p>Bienvenido, por favor siga el siguiente link para restablecer su contraseña <a href={direccion} >Restablecer</a> </p>
					<p>En caso de no haber pedido restablecer su contraseña por favor desestime este correo",					
				};
				
				


				message.Headers.Add("Encabezado", "Valor");
				MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
				client.ServerCertificateValidationCallback = (object sender,
				System.Security.Cryptography.X509Certificates.X509Certificate certificate,
				System.Security.Cryptography.X509Certificates.X509Chain chain,
				System.Net.Security.SslPolicyErrors sslPolicyErrors) => { return true;};
				client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.Auto);
				client.Authenticate(config["SMTPUser"], config["SMTPPass"]);
				await client.SendAsync(message);
			
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
				return entidad1 != null ? Ok(entidad1) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("Nuevo")] // nuevo usuario
		[AllowAnonymous]
        public async Task<ActionResult<Usuario>> Post([FromBody] Usuario usuario)
        {
                var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
				var LocalPort = feature?.LocalPort.ToString();
				var ipv4 = HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
				var ipConexion = "http://" + ipv4 + ":" + LocalPort + "/";

            try
            {

					usuario.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
							password: usuario.Password,
							salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
							prf: KeyDerivationPrf.HMACSHA1,
							iterationCount: 1000,
							numBytesRequested: 256 / 8));
                 
                    contexto.Add(usuario);
                    await contexto.SaveChangesAsync();
                    return usuario;

                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



	}
}
