﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        { 

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client")
                .HasValue<Admin>("Admin");

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    LastName = "Bernardi",
                    Name = "Mateo",
                    Email = "mateobernardi@gmail.com",
                    UserName = "mateo",
                    Password = "123",
                    Id = 3,
                    UserType = "Admin"
                });

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    LastName = "Blanco",
                    Name = "Maria Paz",
                    Email = "pazblanco01@gmail.com",
                    UserName = "paz",
                    Password = "paz123",
                    Address = "Espora 1389",
                    Id = 2,
                    UserType = "Client"
                });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Televisor",
                    Price = 540000,
                    Stock = 10
                });


            // Relación entre Cliente y OrdenDeVenta (uno a muchos)
            modelBuilder.Entity<Client>()
           .HasMany(c => c.SaleOrders)
           .WithOne(o => o.Client)
           .HasForeignKey(o => o.ClientId)
           .OnDelete(DeleteBehavior.Cascade);//Al eliminar una entidad principal, las entidades dependientes relacionadas tambien se eliminan.
                                             //Al eliminar un Cliente, se eliminan todos los SaleOrders asociados.

            // Relación entre OrdenDeVenta y LineaDeVenta (uno a muchos)
            modelBuilder.Entity<SaleOrder>()
                .HasMany(o => o.SaleOrderDetails)
                .WithOne(l => l.SaleOrder)
                .HasForeignKey(l => l.SaleOrderId)
                .OnDelete(DeleteBehavior.Cascade); //Al eliminar una entidad principal, las entidades dependientes relacionadas tambien se eliminan.
                                                   //Al eliminar una SaleOrder, se eliminan todos los SaleOrderDetail asociados.

            // Relacion entre LineaDeVenta y Producto (muchos a uno)
            modelBuilder.Entity<SaleOrderDetail>()
                .HasOne(sol => sol.Product)
                .WithMany() //vacío porque no me interesa establecer esa relación
                .HasForeignKey(sol => sol.ProductId)
                .OnDelete(DeleteBehavior.Restrict); //Al intentar eliminar una entidad principal, no se podrá si tiene entidades dependientes que hagan referencia a la misma.
                                                    //Al intentar eliminar un Product, no se podra si tiene alguna SaleOrderDetail que lo referencie.

            base.OnModelCreating(modelBuilder);
        }
    }
}
