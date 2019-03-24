
namespace Event.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class VotingsController : Controller
    {
        private readonly IVotingRepository votingRepository;
        private readonly IUserHelper userHelper;

        public VotingsController(IVotingRepository votingRepository, IUserHelper userHelper)
        {
            this.votingRepository = votingRepository;
            this.userHelper = userHelper;
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
                return NotFound();
            }

            var votings = await this.votingRepository.GetByIdAsync(id.Value);
            if (votings == null)
            {
                return NotFound();
            }

            return View(votings);
        }

        // GET: Votings/Create
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
                // TODO: Pending to change to: this.User.Identity.Name
                voting.User = await this.userHelper.GetUserByEmailAsync("diegozapata1345z@gmail.com");
                await this.votingRepository.CreateAsync(voting);
                return RedirectToAction(nameof(Index));
            }

            return View(voting);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
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
                    // TODO: Pending to change to: this.User.Identity.Name
                    voting.User = await this.userHelper.GetUserByEmailAsync("jzuluaga55@gmail.com");
                    await this.votingRepository.UpdateAsync(voting);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.votingRepository.ExistAsync(voting.Id))
                    {
                        return NotFound();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await this.votingRepository.GetByIdAsync(id.Value);
            if (voting == null)
            {
                return NotFound();
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
    }


}
