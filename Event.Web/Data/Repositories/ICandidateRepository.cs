namespace Event.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Models;


    public interface ICandidateRepository : IGenericRepository<Voting>
    {
        IQueryable GetAllWithUsers();
        IQueryable GetVotingsWithCandidates();

        Task<Voting> GetVotingWithCandidatesAsync(int id);

        Task<Candidate> GetCandidateAsync(int id);

        Task AddCandidateAsync(CandidateViewModel model);

        Task<int> UpdateCandidateAsync(Candidate candidate);

        Task<int> DeleteCandidateAsync(Candidate candidate);

    }
}
