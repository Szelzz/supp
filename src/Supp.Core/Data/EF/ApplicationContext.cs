using Microsoft.EntityFrameworkCore;
using Supp.Core.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Supp.Core.Data.EF
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ApplicationContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostRelation> PostRelations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Parents)
                .WithOne(r => r.Parent)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Children)
                .WithOne(r => r.Child)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
