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
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FundaClearApp.Controllers
{
    public class CandidateController : Controller
    {
        public string connectionString;

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public CandidateController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public IActionResult JobApplySuccess(string id)
        {
            JobApplySuccessModel objJobApplySuccessModel = new JobApplySuccessModel();
            objJobApplySuccessModel.AkNo = id;
          //  objJobApplySuccessModel.JO

            string emailId = User.Identity.Name;
            Candidate entityCandidate = _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();


            return View(objJobApplySuccessModel);

        }

        [HttpPost]
        public IActionResult Save(CandidateModel model)
        {
            string emailId = User.Identity.Name;
            Candidate entityCandidate = _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();

            if (entityCandidate != null)
            {
                entityCandidate.Fname = model.Fname;
                entityCandidate.CandidateId = model.CandidateId;
                entityCandidate.Lname = model.Lname;
                entityCandidate.Mname = model.Mname;
                entityCandidate.Title = model.Title;
                entityCandidate.PermanentAddress = model.PermanentAddress;
                entityCandidate.PresentAddress = model.PresentAddress;
                entityCandidate.Mobile = model.Mobile;
                entityCandidate.Email = entityCandidate.Email;
                entityCandidate.AlternateMobile = model.AlternateMobile;
                entityCandidate.Gender = model.Gender;
                entityCandidate.DOB = model.DOB;
                entityCandidate.Caste = model.Caste;
                entityCandidate.City = model.City;
                entityCandidate.Pincode = model.Pincode;

                _context.Candidate.Update(entityCandidate);

                _context.SaveChanges();
            }

            return RedirectToAction("details", "Candidate", new { id = model.JobProfileId });
        }

        [HttpPost]
        public IActionResult JobApply(CandidateModel model)
        {
            string emailId = User.Identity.Name;
            string ackNo = "";

            Candidate objCandidate =  _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();

            ackNo = Convert.ToString(DateTime.Now.Year+ DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + objCandidate.Id);

            if (objCandidate != null)
            {
               
                CandidateJobProfileMapping objCandidateJobProfileMapping = new CandidateJobProfileMapping();
                objCandidateJobProfileMapping.CandidateJobProfileMappingId = ackNo;
                objCandidateJobProfileMapping.JobProfileId = model.JobProfileId;
                objCandidateJobProfileMapping.Candidateid = model.CandidateId;
                objCandidateJobProfileMapping.JobAppliedDate = DateTime.Now;
                objCandidateJobProfileMapping.Createddate = DateTime.Now;
                _context.CandidateJobProfileMapping.Add(objCandidateJobProfileMapping);

                _context.SaveChanges();

            }

            return RedirectToAction("JobApplySuccess", "Candidate", new {id = ackNo });
        }

        public async Task<IActionResult> FileUpload(List<IFormFile> files, string fileType, string jobProfileId)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    var filePath = _config.GetSection("FilePath").Value + formFile.FileName;

                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    CandidateFile objCandidateFile = new CandidateFile();
                    objCandidateFile.Name = formFile.FileName;
                    objCandidateFile.CandidateFileId = Guid.NewGuid().ToString();
                    objCandidateFile.CandidateId = _context.LoginDetails.Where(x => x.Username == User.Identity.Name).FirstOrDefault().CandidateId;
                    objCandidateFile.FileType = fileType;
                    objCandidateFile.FilePath = filePath;
                    objCandidateFile.IsActive = true;
                    objCandidateFile.Createddate = DateTime.Now;

                    _context.CandidateFiles.Add(objCandidateFile);
                    _context.SaveChanges();

                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return RedirectToAction("details", "Candidate", new { id = jobProfileId });
        }


        [HttpPost]
        public ActionResult SaveEducationalDetails(CandidateModel model)
        {
            string emailId = User.Identity.Name;

            Candidate entityCandidate = _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();

            if(model.EducationalDetails != null)
            {
                foreach (var data in model.EducationalDetails)
                {
                    EducationalDetails objEducationalDetails = null;

                    if (data.id > 0)
                    {
                         objEducationalDetails = _context.EducationalDetails.Where(x => x.id == data.id).FirstOrDefault();

                        objEducationalDetails.Qualification = data.Qualification;
                        objEducationalDetails.Board = data.Board;
                        objEducationalDetails.YearOfPassing = data.YearOfPassing;
                        objEducationalDetails.Percentage = data.Percentage;
                        _context.Update(objEducationalDetails);

                    }
                    else
                    {
                         objEducationalDetails = new EducationalDetails();
                         objEducationalDetails = ApplicationHelper.BindEducationDetailsModelToEntity(data);
                         objEducationalDetails.CandidateId = entityCandidate.CandidateId;
                        _context.Add(objEducationalDetails);
                    }
                   
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("details", "Candidate", new { id= model.JobProfileId });
        }

        public ViewResult Details(string id)
        {
            CandidateModel objCandidateModel = new CandidateModel();

            try
            {
                string emailId = User.Identity.Name;

                Candidate entityCandidate = _context.Candidate.Where(x=>x.Email == emailId).FirstOrDefault();

                if(entityCandidate != null)
                {

                    objCandidateModel = ApplicationHelper.BindCandidateHelperData(entityCandidate);
                    objCandidateModel.JobProfileId = id;

                    List<EducationalDetails> educationalDetails = _context.EducationalDetails.Where(x => x.CandidateId == entityCandidate.CandidateId).ToList();

                    objCandidateModel.EducationalDetails = new List<EducationalDetailsModel>();
                    objCandidateModel.Files = new List<CandidateFileModel>();

                    foreach (EducationalDetails ed in educationalDetails)
                    {
                        EducationalDetailsModel objEducationalDetailsModel = new EducationalDetailsModel();
                        objEducationalDetailsModel.Percentage = ed.Percentage;
                        objEducationalDetailsModel.Board = ed.Board;
                        objEducationalDetailsModel.YearOfPassing = ed.YearOfPassing;
                        objEducationalDetailsModel.Qualification = ed.Qualification;
                        objEducationalDetailsModel.id = ed.id;
                        objCandidateModel.EducationalDetails.Add(objEducationalDetailsModel);
                    }

                    objCandidateModel.EducationalDetails.Add(new EducationalDetailsModel());

                    List<CandidateFile> files = _context.CandidateFiles.Where(x => x.CandidateId == entityCandidate.CandidateId).ToList();

                    foreach (CandidateFile file in files)
                    {
                        CandidateFileModel model = new CandidateFileModel();
                        model.Name = file.Name;
                        model.FileType = file.FileType;
                        model.Createddate = file.Createddate;
                        objCandidateModel.Files.Add(model);
                    }
                }
            }
            catch (Exception ex)
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
