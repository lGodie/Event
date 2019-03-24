namespace Event.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Event.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Voting> Votings { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }

}
