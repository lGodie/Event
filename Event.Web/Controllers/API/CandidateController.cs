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
    public class CandidateController : Controller
    {
        private readonly ICandidateRepository candidateRepository;
        private readonly IUserHelper userHelper;

        public CandidateController(ICandidateRepository candidateRepository, IUserHelper userHelper)
        {
            this.candidateRepository = candidateRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetCandidates()
        {
            return this.Ok(this.candidateRepository.GetVotingsWithCandidates());
        }
    }
    }
