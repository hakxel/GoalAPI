using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoalAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalAPI.DBContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<Book> Books { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with initial data
            modelBuilder.Entity<Book>().HasData(
                new Book()
                {
                    Id = Guid.Parse("a4529ee2-d396-4251-86fd-5fa7e8953748"),
                    Title = "1984",
                    Subtitle = "",
                    Author = "George Orwell",
                }
            );

            modelBuilder.Entity<Note>().HasData(
                new Note()
                {
                    Id = Guid.Parse("aca83d0e-d0cd-4676-9c25-fd1248fad24c"),
                    Content = "Deeply disturbing",
                    BookId = Guid.Parse("a4529ee2-d396-4251-86fd-5fa7e8953748")
                });
        }
    }
}
