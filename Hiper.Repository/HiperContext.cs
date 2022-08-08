using Hiper.Domain.Entities;
using Hiper.Repository.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Hiper.Repository
{
  public class HiperContext : DbContext
  {
    public DbSet<Client> Clients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(@"Server=PC-CASA\SQLEXPRESS; Database=Hiper;User ID=sa;Password=1q2w3e4r5t");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new ClientMap());
    }
  }
}