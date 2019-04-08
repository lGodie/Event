namespace Event.Web.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using Models;


    public class CandidateRepository : GenericRepository<Voting>, ICandidateRepository
    {
        private readonly DataContext context;
        public CandidateRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task AddCandidateAsync(CandidateViewModel model)
        {
            var voting = await this.GetVotingWithCandidatesAsync(model.VotingId);
            if (voting == null)
            {
                return;
            }

            voting.Candidates.Add(new Candidate { Name = model.Name, Proposal = model.Proposal });
            this.context.Votings.Update(voting);
            await this.context.SaveChangesAsync();
        }


        public async Task<int> DeleteCandidateAsync(Candidate candidate)
        {
            var voting = await this.context.Votings.Where(c => c.Candidates.Any(ca => ca.Id == ca.Id)).FirstOrDefaultAsync();
            if (voting == null)
            {
                return 0;
            }

            this.context.Candidates.Remove(candidate);
            await this.context.SaveChangesAsync();
            return voting.Id;
        }

        

        public IQueryable GetVotingsWithCandidates()
        {
            return this.context.Votings
                .Include(c => c.Candidates)
                .OrderBy(c => c.Description);
        }

        public async Task<Voting> GetVotingWithCandidatesAsync(int id)
        {
            return await this.context.Votings
                .Include(c => c.Candidates)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateCandidateAsync(Candidate candidate)
        {
            var voting = await this.context.Votings
                .Where(c => c.Candidates.Any(ca => ca.Id == candidate.Id)).FirstOrDefaultAsync();
            if (voting == null)
            {
                return 0;
            }

            this.context.Candidates.Update(candidate);
            await this.context.SaveChangesAsync();
            return candidate.Id;
        }


        public async Task<Candidate> GetCandidateAsync(int id)
        {
            return await this.context.Candidates.FindAsync(id);
        }

        public IQueryable GetAllWithUsers()
        {
            return this.context.Votings.Include(v => v.User);
        }
    }
}
