using JogoGourmet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Data
{
    public class JogoGourmetContext : DbContext
    {
        public JogoGourmetContext(DbContextOptions<JogoGourmetContext> options) : base(options) { }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Description> Descriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishDescription>()
                .HasKey(dd => new { dd.DishId, dd.DescriptionId });

            modelBuilder.Entity<DishDescription>()
                .HasOne(dd => dd.Dish)
                .WithMany(d => d.DishDescriptions)
                .HasForeignKey(dd => dd.DishId);

            modelBuilder.Entity<DishDescription>()
                .HasOne(dd => dd.Description)
                .WithMany(d => d.DishDescriptions)
                .HasForeignKey(dd => dd.DescriptionId);

            modelBuilder.Entity<Description>().HasData(
               new Description { Id = 1, Text = "suits with coffee" },
               new Description { Id = 2, Text = "the best dish in the world" },
               new Description { Id = 3, Text = "the best way to eat beans" }
           );

            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name = "Pão de queijo" },
                new Dish { Id = 2, Name = "Feijão Tropeiro" },
                new Dish { Id = 3, Name = "Pão de sal" }
            );
            modelBuilder.Entity<DishDescription>().HasData(
                new DishDescription { DishId = 1, DescriptionId = 1 },
                new DishDescription { DishId = 1, DescriptionId = 2 },
                new DishDescription { DishId = 2, DescriptionId = 3 },
                new DishDescription { DishId = 3, DescriptionId = 1 },
                new DishDescription { DishId = 3, DescriptionId = 2 }
            );
        }

    }
}
