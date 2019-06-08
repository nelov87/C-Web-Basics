using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Demo.Data
{
    public class DemoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DemoDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-533LOVH\\SQLEXPRESS;Database=SISDemo;Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
