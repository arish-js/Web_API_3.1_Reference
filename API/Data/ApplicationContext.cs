using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Command> Commands { get; set; }
    }
}
