namespace Event.Web.Data
{
    using Event.Web.Data.Entities;
    public class CandidateRepository : GenericRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(DataContext context) : base(context)
        {
        }
    }
}
