using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codeFirst.Models
{
    public class DeveloperDbContext : DbContext
    {
        public DeveloperDbContext(DbContextOptions <DeveloperDbContext> options) : base(options)
        {

        }

        public DbSet<Developer> Developer { get; set; }
        public DbSet<Country> Country { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfiguration(new DeveloperEntityConfiguration());
        //}
    }
}
