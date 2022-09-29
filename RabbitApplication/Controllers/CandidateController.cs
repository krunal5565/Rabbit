using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitApplication.Data;
using RabbitApplication.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using RabbitApplication.Helpers;
using RabbitApplication.Entity;

namespace FundaClearApp.Controllers
{
    public class CandidateController : Controller
    {
        public string connectionString;

        private readonly ApplicationDbContext _context;

        public CandidateController(ApplicationDbContext context)
        {
            _context = context;
        }

     
        
        public IActionResult JobApplySuccess()
        {
            return View();

        }
        
        [HttpPost]
        public IActionResult JobApply(CandidateModel model)
        {
            string emailId = User.Identity.Name;
            Candidate objCandidate =  _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();
            objCandidate.Fname = model.Fname;
            objCandidate.CandidateId = model.CandidateId;
            objCandidate.Lname = model.Lname;
            objCandidate.Mname = model.Mname;
            objCandidate.Title = model.Title;
            objCandidate.PermanentAddress = model.PermanentAddress;
            objCandidate.PresentAddress = model.PresentAddress;
            objCandidate.Mobile = model.Mobile;
            objCandidate.Email = model.Email;
            objCandidate.AlternateMobile = model.AlternateMobile;
            objCandidate.Gender = model.Gender;
            objCandidate.DOB = model.DOB;
            objCandidate.Caste = model.Caste;
            objCandidate.City = model.City;
            objCandidate.Pincode = model.Pincode;
          
            _context.Candidate.Update(objCandidate);

            CandidateJobProfileMapping objCandidateJobProfileMapping = new CandidateJobProfileMapping();
            objCandidateJobProfileMapping.CandidateJobProfileMappingId = Guid.NewGuid().ToString();
            objCandidateJobProfileMapping.JobProfileId = model.JobProfileId;
            objCandidateJobProfileMapping.Candidateid = model.CandidateId;
            objCandidateJobProfileMapping.JobAppliedDate = DateTime.Now;

            _context.CandidateJobProfileMapping.Add(objCandidateJobProfileMapping);

            _context.SaveChanges();

            return RedirectToAction("JobApplySuccess", "Candidate");
        }


        public ViewResult Details(string id)
        {
            CandidateModel objCandidateModel = new CandidateModel();

            try
            {
                string emailId = User.Identity.Name;

               Candidate entityCandidate = _context.Candidate.Where(x=>x.Email == emailId).FirstOrDefault();

                objCandidateModel = ApplicationHelper.BindCandidateHelperData(entityCandidate);
                objCandidateModel.JobProfileId = id;


            }
            catch(Exception ex)
            {

            }
            return View(objCandidateModel);
        }

        public ActionResult Grid()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult index()
        {
            List<CandidateModel> objCandidateModelList = new List<CandidateModel>();

            try
            {
                List<Candidate> entityCandidateList = _context.Candidate.ToList();

                foreach (Candidate objCandidate in entityCandidateList)
                {
                    CandidateModel objCandidateModel = ApplicationHelper.BindCandidateHelperData(objCandidate);
                    
                    CandidateJobProfileMapping objCandidateJobProfileMapping = _context.CandidateJobProfileMapping.Where(x => x.Candidateid == objCandidate.CandidateId).FirstOrDefault();

                    objCandidateModel.JobProfileName = _context.JobProfile.Where(x => x.JobProfileId == objCandidateJobProfileMapping.JobProfileId).FirstOrDefault().Name;

                    objCandidateModelList.Add(objCandidateModel);
                }
            }
            catch (Exception ex)
            {

            }

            return View("Admin/index", objCandidateModelList);
        }


        [HttpGet]
        public IActionResult ViewFiles(string id)
        {
            var candidateFile = _context.CandidateFiles.Where(x=>x.CandidateId == id);

            return View("Admin/Files", candidateFile);
        }

        public IActionResult view(string id)
        {
            CandidateModel objCandidateModel = new CandidateModel();

            try
            {
                if (id == null || id == "")
                {
                    return NotFound();
                }

                var candidate = _context.Candidate.FirstOrDefault(m => m.CandidateId == id);

                objCandidateModel = ApplicationHelper.BindCandidateHelperData(candidate);

                if (candidate == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

            }

            return View("Admin/view", objCandidateModel);
        }

        [HttpPost]
        public IActionResult Login(LoginDetailsModel model)
        {
            try
            {
                LoginDetails dbLoginDetails = _context.LoginDetails.AsQueryable().Where(x => x.Username == model.Username
                && x.Password == model.Password).FirstOrDefault();

                if (dbLoginDetails != null)
                {
                    var identity = new ClaimsIdentity(new[] { 
                        new Claim(ClaimTypes.Name, model.Username)

                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (!string.IsNullOrEmpty(dbLoginDetails.CandidateId))
                    {
                        return RedirectToAction("index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("dashboard", "Account");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public IActionResult Files(string id)
        {
            var candidateFile = _context.CandidateFiles.Where(x => x.CandidateId == id).FirstOrDefault();

            CandidateFileModel objCandidateFileModel = ApplicationHelper.BindFileHelperData(candidateFile);

            return View("Files", objCandidateFileModel);
        }

        public IActionResult Logout()
        {
            HttpContext.Session = null;

            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Candidate");
        }

        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {

            if (ModelState.IsValid)
            {
                Candidate dbUser = new Candidate();
                dbUser.Email = model.Email;
                dbUser.Createddate = DateTime.Now;
                dbUser.Updateddate = DateTime.Now;
                dbUser.CandidateId = Guid.NewGuid().ToString();
                _context.Add(dbUser);
                await _context.SaveChangesAsync();

                LoginDetails dbLoginDetails = new LoginDetails();
                dbLoginDetails.Username = model.Email;
                dbLoginDetails.Password = model.Password;
                dbLoginDetails.CreatedDate = DateTime.Now;
                dbLoginDetails.UpdatedDate = DateTime.Now;
                dbLoginDetails.CandidateId = dbUser.CandidateId;
                dbLoginDetails.LoginDetailsId = Guid.NewGuid().ToString();

                _context.Add(dbLoginDetails);

                await _context.SaveChangesAsync();

                ViewBag.UserAdded = "Account created successfully. You can login using your account.";
                return RedirectToAction(nameof(Login));
            }

            return View();
        }
    }
}
