using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RabbitApplication.Data;
using RabbitApplication.Model;

namespace RabbitApplication.Controllers
{
    public class JobProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobProfile.ToListAsync());
        }

        // GET: JobProfiles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobProfile = await _context.JobProfile
                .FirstOrDefaultAsync(m => m.id == id);
            if (jobProfile == null)
            {
                return NotFound();
            }

            return View(jobProfile);
        }

        // GET: JobProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description,createddate,updateddate,isactive")] JobProfile jobProfile)
        {
            jobProfile.createddate = DateTime.UtcNow;
            jobProfile.updateddate = DateTime.UtcNow;

            if (ModelState.IsValid)
            {

                _context.Add(jobProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobProfile);
        }

        // GET: JobProfiles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobProfile = await _context.JobProfile.FindAsync(id);
            if (jobProfile == null)
            {
                return NotFound();
            }
            return View(jobProfile);
        }

        // POST: JobProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id,name,description,createddate,updateddate,isactive")] JobProfile jobProfile)
        {
            if (id != jobProfile.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobProfileExists(jobProfile.id))
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
            return View(jobProfile);
        }

        // GET: JobProfiles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobProfile = await _context.JobProfile
                .FirstOrDefaultAsync(m => m.id == id);
            if (jobProfile == null)
            {
                return NotFound();
            }

            return View(jobProfile);
        }

        // POST: JobProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var jobProfile = await _context.JobProfile.FindAsync(id);
            _context.JobProfile.Remove(jobProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobProfileExists(long id)
        {
            return _context.JobProfile.Any(e => e.id == id);
        }
    }
}
