namespace Event.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Event.Web.Data.Entities;
    public class DataContext : DbContext
    {
        public DbSet<Voting> Votings { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }

}
