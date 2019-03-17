using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Event.Web.Data;
using Event.Web.Data.Entities;

namespace Event.Web.Controllers
{
    public class VotingsController : Controller
    {
        private readonly DataContext _context;

        public VotingsController(DataContext context)
        {
            _context = context;
        }

        // GET: Votings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Votings.ToListAsync());
        }

        // GET: Votings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await _context.Votings
                .FirstOrDefaultAsync(m => m.VotingId == id);
            if (voting == null)
            {
                return NotFound();
            }

            return View(voting);
        }

        // GET: Votings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Votings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VotingId,Description,Remarks,DateTimeStart,DateTimeEnd,IsEnableBlankVote,QuantityVotes,QuantityBlankVotes,CandidateWinId")] Voting voting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(voting);
        }

        // GET: Votings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voting = await _context.Votings.FindAsync(id);
            if (voting == null)
            {
                return NotFound();
            }
            return View(voting);
        }

        // POST: Votings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VotingId,Description,Remarks,DateTimeStart,DateTimeEnd,IsEnableBlankVote,QuantityVotes,QuantityBlankVotes,CandidateWinId")] Voting voting)
        {
            if (id != voting.VotingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VotingExists(voting.VotingId))
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

            var voting = await _context.Votings
                .FirstOrDefaultAsync(m => m.VotingId == id);
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
            var voting = await _context.Votings.FindAsync(id);
            _context.Votings.Remove(voting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VotingExists(int id)
        {
            return _context.Votings.Any(e => e.VotingId == id);
        }
    }
}
