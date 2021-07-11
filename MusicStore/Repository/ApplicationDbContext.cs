using System.Windows;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;

namespace MusicStore.Repository
{
    public class ApplicationDbContext : DbContext
    { 
        public DbSet<User> Users { get; set; }
        public DbSet<Storage> Storages  { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Collective> Collectives { get; set; }
        public DbSet<ClientBoughtGood> ClientBoughtGoods { get; set; }
       
       
        public ApplicationDbContext()
        {
              //Database.EnsureDeleted();
              //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Collective>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Publisher>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Record>().HasIndex(i => i.Name).IsUnique();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
              optionsBuilder.UseSqlServer(@"Server=MSI\SQLEXPRESS;Database=MusicStore;Trusted_Connection=True;");
        } 
    }
}