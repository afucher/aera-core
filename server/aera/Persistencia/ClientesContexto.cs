using System;
using Microsoft.EntityFrameworkCore;

namespace aera_core.Persistencia
{
    public class ClientesContexto : DbContext
    {
        public DbSet<ClienteDB> Clientes { get; set; }
        public ClientesContexto(DbContextOptions opções) : base(opções) { }
    }
}