namespace Event.Web.Data
{
    using Event.Web.Data.Entities;
    using System.Linq;

    public interface IVotingRepository : IGenericRepository<Voting>
    {
        IQueryable GetAllWithUsers();
    }
}
