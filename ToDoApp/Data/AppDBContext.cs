using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoTag> TodoTags { get; set; }
        public DbSet<Tag> Tag { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoTag>()
                .HasKey(tt => new { tt.TagId, tt.TodoId });

            builder.Entity<TodoTag>()
                .HasOne(tt => tt.Todo)
                .WithMany(t => t.TodoTags)
                .HasForeignKey(tt => tt.TodoId);


            builder.Entity<TodoTag>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.TodoTags)
                .HasForeignKey(tt => tt.TagId);

            builder.Entity<Todo>()
                .Property(t => t.RowVersion)
                .IsRowVersion();

            Seed(builder);
        }

        protected void Seed(ModelBuilder builder)
        {

            builder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "Tag 1" },
                new Tag { Id = 2, Name = "Tag 2" },
                new Tag { Id = 3, Name = "Tag 3" }
            );
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Matan",
                    LastName = "Shomer",
                    Email = "mshomer@gmail.com",
                    Password = "Qawsedr1234!",
                },
                new User
                {
                    Id = 2,
                    FirstName = "Noam",
                    LastName = "Choen",
                    Email = "noam-choen@gmail.com",
                    Password = "Qawsedr1234!",
                }
            );
            builder.Entity<UserProfile>().HasData(
               new UserProfile { Id = 1, Age = 33, UserID = 1 },
               new UserProfile { Id = 2, Age = 25, UserID = 2 }
            );
            builder.Entity<Todo>().HasData(
                new Todo { Id = 1, Title = "Todo 1", UserId = 1 },
                new Todo { Id = 2, Title = "Todo 2", UserId = 1 },
                new Todo { Id = 3, Title = "Todo 3", UserId = 2 }
            );
            builder.Entity<TodoTag>().HasData(
                new TodoTag { TagId = 1, TodoId = 1 },
                new TodoTag { TagId = 2, TodoId = 1 },
                new TodoTag { TagId = 3, TodoId = 2 }
            );
        }

    }
}
