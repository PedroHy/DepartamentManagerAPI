using DepartamentManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DepartamentManager.Context
{
    public class AppDbContext : DbContext
    {
        //Database context
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //setting Tables
        public DbSet<Departament> Departament { get; set; }
        public DbSet<Colaborator> Colaborator { get; set; }
    }
}
