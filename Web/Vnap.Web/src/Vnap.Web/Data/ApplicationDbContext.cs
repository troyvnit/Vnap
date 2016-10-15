using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Vnap.Web.Models;
using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantDisease> PlantDiseases { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Solution> Solutions { get; set; }

        public virtual int Commit()
        {
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetSection("ConnectionStrings:DefaultConnection").Value);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
