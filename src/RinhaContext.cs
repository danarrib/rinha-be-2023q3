using Microsoft.EntityFrameworkCore;
using RinhaBackend2023Q3.Models;

namespace RinhaBackend2023Q3
{
    public class RinhaContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public RinhaContext(DbContextOptions<RinhaContext> options) : base(options)
        {
        }

    }
}
