namespace Event.Web.Controllers.API
{
    using Data;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Web.Http;
    using Event.Web.Data.Entities;
    using Event.Web.Helpers;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class CandidateController : Controller
    {
        private readonly ICandidateRepository candidateRepository;
        

        public CandidateController(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
            
            
        }

        [HttpGet]
        public IActionResult GetVotings()
        {
            return Ok(this.candidateRepository.GetVotingsWithCandidates());
        }
    }
}
