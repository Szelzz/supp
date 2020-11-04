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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
        }
    }
}
