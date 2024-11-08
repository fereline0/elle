using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elle.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace elle
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<City> Cities { get; set; } = default!;
        public DbSet<Home> Homes { get; set; } = default!;
        public DbSet<Immovable> Immovables { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    "User Id=postgres.rjurdayyqqeogvkwwaqj;Password=aRHPDtHFuYj1N0Fq;Server=aws-0-eu-central-1.pooler.supabase.com;Port=5432;Database=postgres;"
                );
            }
        }
    }
}
