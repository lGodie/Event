using System.Linq;
using Event.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Event.Web.Data
{
    public class VotingRepository : GenericRepository<Voting>, IVotingRepository
    {
        private readonly DataContext context;

        public VotingRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Votings.Include(v => v.User);
        }
    }

}
