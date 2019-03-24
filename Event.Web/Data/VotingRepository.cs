using Event.Web.Data.Entities;

namespace Event.Web.Data
{
    public class VotingRepository : GenericRepository<Voting>, IVotingRepository
    {
        public VotingRepository(DataContext context) : base(context)
        {
        }
    }

}
