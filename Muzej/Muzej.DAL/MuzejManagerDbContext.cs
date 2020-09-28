using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Muzej.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Muzej.DAL
{
    public class MuzejManagerDbContext : IdentityDbContext<AppUser>
    {
        protected MuzejManagerDbContext()
        {
        }

        public MuzejManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Maker> Makers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Motor> Motors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //country
            modelBuilder.Entity<Country>().HasData(new Country { ID = 1, Name = "Njemačka" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 2, Name = "Francuska" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 3, Name = "Sjedinjene Američke Države" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 4, Name = "Italija" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 5, Name = "Hrvatska" });
            modelBuilder.Entity<Country>().HasData(new Country { ID = 6, Name = "Švedska" });

            //car makes
            modelBuilder.Entity<Maker>().HasData(new Maker
            {
                ID = 1,
                Name = "Porsche",
                CountryID = 1
            });

            modelBuilder.Entity<Maker>().HasData(new Maker
            {
                ID = 2,
                Name = "Audi",
                CountryID = 1
            });

            modelBuilder.Entity<Maker>().HasData(new Maker
            {
                ID = 3,
                Name = "Volkswagen",
                CountryID = 1
            }); 

            modelBuilder.Entity<Maker>().HasData(new Maker
            {
                ID = 4,
                Name = "Peugeot",
                CountryID = 2
            });

            modelBuilder.Entity<Maker>().HasData(new Maker
            {
                ID = 5,
                Name = "Renault",
                CountryID = 2
            });

            modelBuilder.Entity<Maker>().HasData(new Maker
            {
                ID = 6,
                Name = "Ford",
                CountryID = 3
            });

            modelBuilder.Entity<Maker>().HasData(new Maker
            {
                ID = 7,
                Name = "Tesla",
                CountryID = 3
            });

            modelBuilder.Entity<Owner>().HasData(new Owner
            {
                ID = 1,
                FirstName = "Maja",
                LastName = "Tkalčević",
                OIB = "1234567890"
            });

            //owners
            modelBuilder.Entity<Owner>().HasData(new Owner
            {
                ID = 2,
                FirstName = "Filip",
                LastName = "Lukinić",
                OIB = "0987654321"
            });

            modelBuilder.Entity<Owner>().HasData(new Owner
            {
                ID = 3,
                FirstName = "Dora",
                LastName = "Batinjan",
                OIB = "1337733142"
            });

            //motors
            modelBuilder.Entity<Motor>().HasData(new Motor
            {
                ID = 1,
                Name = "1.2 L I3 MPI",
                Power = 55,
                Torque = 112,
                Configuration = "Multipoint fuel injection straight-three engine",
                Type = FuleType.Petrol
            });

            modelBuilder.Entity<Motor>().HasData(new Motor
            {
                ID = 2,
                Name = "4.6 L V8 918",
                Power = 400,
                Torque = 800,
                Configuration = "V8 twin turbo",
                Type = FuleType.Petrol
            });

            modelBuilder.Entity<Motor>().HasData(new Motor
            {
                ID = 3,
                Name = "Electric 918",
                Power = 126,
                Torque = 240,
                Configuration = "Electric motor",
                Type = FuleType.Electricity
            });
        }
    }
}


//dotnet ef migrations add Initial --startup-project Muzej.Web --context MuzejManagerDbContext --project Muzej.DAL

//dotnet ef database update Initial --startup-project Muzej.Web --context MuzejManagerDbContext --project Muzej.DAL
