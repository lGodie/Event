namespace Event.Web.Controllers.API
{
    using Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Event.Web.Data.Entities;
    using Event.Web.Helpers;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VotingsController : Controller
    {
        private readonly IVotingRepository votingRepository;
        private readonly IUserHelper userHelper;

        public VotingsController(IVotingRepository votingRepository, IUserHelper userHelper)
        {
            this.votingRepository = votingRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetVotings()
        {
            return this.Ok(this.votingRepository.GetAllWithUsers());
        }

        
    }

}
