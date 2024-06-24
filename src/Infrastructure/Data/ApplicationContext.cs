using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }

        //private readonly bool isTestingEnvironment;

        //public ApplicationContext(DbContextOptions<ApplicationContext> options, bool isTestingEnvironment = false) : base(options)
        ////Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        //{
        //    this.isTestingEnvironment = isTestingEnvironment;
        //}

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<User>("User")
                .HasValue<Client>("Client");

            base.OnModelCreating(modelBuilder);
        }
    }

}
