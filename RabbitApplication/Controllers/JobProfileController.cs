using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitApplication.Data;
using RabbitApplication.Entity;
using RabbitApplication.Helpers;
using RabbitApplication.Models;

namespace RabbitApplication.Controllers
{
    public class JobProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public JobProfileController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            List<JobProfileModel> lstJobProfileModel = new List<JobProfileModel>();

            if (User.Identity.IsAuthenticated)
            {
                var jobProfile = await _context.JobProfile.ToListAsync();
                foreach (JobProfile objJobProfile in jobProfile)
                {
                    lstJobProfileModel.Add(ApplicationHelper.BindJobProfileEntityToModel(objJobProfile));
                }

                return View(lstJobProfileModel);
            }
            else
            {
                return RedirectToAction("Login", "Candidate");
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }

                var jobProfile = await _context.JobProfile.FirstOrDefaultAsync(m => m.JobProfileId == id);

                JobProfileModel objJobProfileModel = ApplicationHelper.BindJobProfileEntityToModel(jobProfile);

                if (jobProfile == null)
                {
                    return NotFound();
                }

                return View(objJobProfileModel);
            }
            else
            {
                return RedirectToAction("Login", "Candidate");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobProfileModel jobProfileModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (jobProfileModel != null)
                    {
                        jobProfileModel.JobProfileId = Guid.NewGuid().ToString();
                        JobProfile jobProfile = ApplicationHelper.BindJobProfileModelToEntity(jobProfileModel);

                        _context.Add(jobProfile);
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
                return View(jobProfileModel);
            }
            else
            {
                return RedirectToAction("Login", "Candidate");
            }
        }

        public IActionResult CareersDetails(string id)
        {
                if (string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }

                var jobProfile = _context.JobProfile
                    .FirstOrDefault(m => m.JobProfileId == id);

                JobProfileModel objJobProfileModel = ApplicationHelper.BindJobProfileEntityToModel(jobProfile);

                if (jobProfile == null)
                {
                    return NotFound();
                }

                return View("CareersJobProfileDetails", objJobProfileModel);
            
        }

        // GET: JobProfile/Edit/5
        public IActionResult Edit(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }

                var jobProfile = _context.JobProfile.Where(x => x.JobProfileId == id).FirstOrDefault();

                JobProfileModel objJobProfileModel = ApplicationHelper.BindJobProfileEntityToModel(jobProfile);

                if (objJobProfileModel == null)
                {
                    return NotFound();
                }
                return View(objJobProfileModel);
            }
            else
            {
                return RedirectToAction("Login", "Candidate");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(JobProfileModel jobProfileModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(jobProfileModel.JobProfileId))
                {
                    return NotFound();
                }

                //  if (ModelState.IsValid)
                {
                    try
                    {
                        var jobProfile = _context.JobProfile.Where(x => x.JobProfileId == jobProfileModel.JobProfileId).FirstOrDefault();

                        jobProfile.NumberOfPositions = jobProfileModel.NumberOfPositions;
                        jobProfile.EndDate = jobProfileModel.EndDate;
                        jobProfile.Description = jobProfileModel.Description;
                        jobProfile.StartDate = jobProfileModel.StartDate;
                        jobProfile.Name = jobProfileModel.Name;

                        _context.Update(jobProfile);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!JobProfileExists(jobProfileModel.Id))
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
                // return View(jobProfileModel);
            }
            else
            {
                return RedirectToAction("Login", "Candidate");
            }
        }


        //[HttpGet]
        //public IActionResult FileUpload()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult Files()
        //{
        //    string candidateId = _context.LoginDetails.Where(x => x.Username == User.Identity.Name).FirstOrDefault().CandidateId;

        //    var candidateFile = _context.CandidateFiles.Where(x => x.CandidateId == candidateId);

        //    return View(candidateFile);
        //}


        // GET: JobProfile/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var jobProfile = await _context.JobProfile
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (jobProfile == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(jobProfile);
        //}

        // POST: JobProfile/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    var jobProfile = await _context.JobProfile.FindAsync(id);
        //    _context.JobProfile.Remove(jobProfile);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool JobProfileExists(long id)
        {
            return _context.JobProfile.Any(e => e.Id == id);
        }
    }
}
