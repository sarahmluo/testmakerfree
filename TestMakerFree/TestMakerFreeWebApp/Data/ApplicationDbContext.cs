﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestMakerFreeWebApp.Data.Models;

namespace TestMakerFreeWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestMakerFree;Integrated Security=True;MultipleActiveResultSets=true");

        }

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define user table
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Quizzes).WithOne(i => i.User);

            // Define Quiz table
            modelBuilder.Entity<Quiz>().ToTable("Quizzes");
            modelBuilder.Entity<Quiz>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Quiz>().HasOne(i => i.User).WithMany(u => u.Quizzes);
            modelBuilder.Entity<Quiz>().HasMany(i => i.Questions).WithOne(c => c.Quiz);

            // Define Question table
            modelBuilder.Entity<Question>().ToTable("Questions");
            modelBuilder.Entity<Question>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Question>().HasOne(i => i.Quiz).WithMany(u => u.Questions);
            modelBuilder.Entity<Question>().HasMany(i => i.Answers).WithOne(c => c.Question);

            // Define Answer table
            modelBuilder.Entity<Answer>().ToTable("Answers");
            modelBuilder.Entity<Answer>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Answer>().HasOne(i => i.Question).WithMany(u => u.Answers);

            // Define Results table
            modelBuilder.Entity<Result>().ToTable("Results");
            modelBuilder.Entity<Result>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Result>().HasOne(i => i.Quiz).WithMany(u => u.Results);
        }
        #endregion

        #region Properties
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Result> Results { get; set; }
        #endregion
    }
}
