namespace Event.Web.Controllers.API
{
    using Data;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    public class VotingsController : Controller
    {
        private readonly IVotingRepository votingRepository;

        public VotingsController(IVotingRepository votingRepository)
        {
            this.votingRepository = votingRepository;
        }

        [HttpGet]
        public IActionResult GetVotings()
        {
            return this.Ok(this.votingRepository.GetAll());
        }
    }

}
