using SistemaDeliveryApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeliveryApi_.Net_Core.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }
        public DbSet<Pedido?> Pedidos { get; set; }
        public DbSet<Pago> Pago { get; set; }

        
    }
}
