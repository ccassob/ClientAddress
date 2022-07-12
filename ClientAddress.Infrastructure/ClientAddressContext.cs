using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientAddress.Infrastructure
{
    public class ClientAddressContext : DbContext
    {
        public ClientAddressContext(DbContextOptions<ClientAddressContext> options)
         : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

    public class Address
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Client { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}