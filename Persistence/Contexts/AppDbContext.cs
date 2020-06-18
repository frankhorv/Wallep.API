using Microsoft.EntityFrameworkCore;
using Wallet.API.Domain.Models;

namespace Wallet.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Balance>().ToTable("Balances");
            builder.Entity<Balance>().HasKey(p => new { p.PlayerId, p.Asset });

            builder.Entity<Operation>().ToTable("Operations");
            builder.Entity<Operation>().Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Entity<Transaction>().ToTable("Transactions");
            builder.Entity<Transaction>().HasMany(p => p.Operations).WithOne(p => p.Transaction).HasForeignKey(p => p.TransactionId);

            builder.Entity<Player>().ToTable("Players");
            builder.Entity<Player>().HasMany(p => p.Balances).WithOne(p => p.Player).HasForeignKey(p => p.PlayerId);
            builder.Entity<Player>().HasMany(p => p.Transactions).WithOne(p => p.Player).HasForeignKey(p => p.PlayerId);
        }
    }
}