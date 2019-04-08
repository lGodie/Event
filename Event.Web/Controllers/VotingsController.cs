namespace Event.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Event.Web.Models;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class VotingsController : Controller
    {
        private readonly IVotingRepository votingRepository;
        private readonly IUserHelper userHelper;
        private readonly ICandidateRepository candidateRepository;

        public VotingsController(IVotingRepository votingRepository,
            IUserHelper userHelper,
            ICandidateRepository candidateRepository)
        {
            this.votingRepository = votingRepository;
            this.userHelper = userHelper;
            this.candidateRepository = candidateRepository;
        }

        // GET: Votings
        public IActionResult Index()
        {
            return View(this.votingRepository.GetAll().OrderBy(v => v.DateTimeStart));
        }

        // GET: Votings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VotingNotFound");
            }

            var votings = await this.candidateRepository.GetVotingWithCandidatesAsync(id.Value);
            if (votings == null)
            {
                return new NotFoundViewResult("VotingNotFound");
            }

            return View(votings);
        }

        // GET: Votings/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Voting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voting voting)
        {
            if (ModelState.IsValid)
            {

                voting.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await this.votingRepository.CreateAsync(voting);
                return RedirectToAction(nameof(Index));
            }

            return View(voting);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VotingNotFound");
            }

            var voting = await this.votingRepository.GetByIdAsync(id.Value);
            if (voting == null)
            {
                return NotFound();
            }

            return View(voting);
        }

        // POST: Voting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Voting voting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    voting.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await this.votingRepository.UpdateAsync(voting);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.votingRepository.ExistAsync(voting.Id))
                    {
                        return new NotFoundViewResult("VotingNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(voting);
        }

        // GET: Votings/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VotingNotFound");
            }

            var voting = await this.votingRepository.GetByIdAsync(id.Value);
            if (voting == null)
            {
                return new NotFoundViewResult("VotingNotFound");
            }

            return View(voting);
        }

        // POST: Votings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.votingRepository.GetByIdAsync(id);
            await this.votingRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult VotingNotFound()
        {
            return this.View();
        }

        public async Task<IActionResult> DeleteCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.candidateRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            var votingId = await this.candidateRepository.DeleteCandidateAsync(candidate);
            return this.RedirectToAction($"Details/{votingId}");
        }

        public async Task<IActionResult> Editcandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidate = await this.candidateRepository.GetCandidateAsync(id.Value);
            if (candidate == null)
            {
                return NotFound();
            }

            return View(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> Editcandidate(Candidate candidate)
        {
            if (this.ModelState.IsValid)
            {
                var votingId = await this.candidateRepository.UpdateCandidateAsync(candidate);
                if (votingId != 0)
                {
                    return this.RedirectToAction($"Details/{votingId}");
                }
            }

            return this.View(candidate);
        }

        public async Task<IActionResult> AddCandidate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await this.candidateRepository.GetByIdAsync(id.Value);
            if (voting == null)
            {
                return NotFound();
            }

            var model = new CandidateViewModel { VotingId = voting.Id };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddCandidate(CandidateViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.candidateRepository.AddCandidateAsync(model);
                return this.RedirectToAction($"Details/{model.VotingId}");
            }

            return this.View(model);
        }

        
    }


}

