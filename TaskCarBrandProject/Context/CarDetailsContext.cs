using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCarBrandProject.Models;

namespace TaskCarBrandProject.Context
{
    public class CarDetailsContext : DbContext
    {
        public CarDetailsContext(DbContextOptions<CarDetailsContext> options) : base(options)
        {

        }


        public DbSet<CarDetails> CarDetail { get; set; }

        public DbSet<SignUp> signUpForm { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarDetails>().ToTable("CarDetails");

            modelBuilder.Entity<SignUp>().ToTable("SignUp");
        }
    }
}
