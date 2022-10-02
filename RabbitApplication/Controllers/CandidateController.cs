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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Hosting;

namespace FundaClearApp.Controllers
{
    public class CandidateController : Controller
    {
        public string connectionString;

        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CandidateController(ApplicationDbContext context, IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _config = config;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult JobApplySuccess(string id)
        {
            JobApplySuccessModel objJobApplySuccessModel = new JobApplySuccessModel();
            objJobApplySuccessModel.AkNo = id;

            string emailId = User.Identity.Name;
            Candidate entityCandidate = _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();

            return View(objJobApplySuccessModel);
        }

        private void ValidateCandidateDetails(CandidateModel model)
        {
            if (String.IsNullOrEmpty(model.Title))
            {
                ModelState.AddModelError("title", "Please enter Title");
            }
            else if (String.IsNullOrEmpty(model.Fname))
            {
                ModelState.AddModelError("fname", "Please enter fname");
            }
            else if (String.IsNullOrEmpty(model.Lname))
            {
                ModelState.AddModelError("lname", "Please enter lname");
            }
            else if (String.IsNullOrEmpty(model.Gender))
            {
                ModelState.AddModelError("gender", "Please enter Gender");
            }
            else if (model.DOB == null || DateTime.MinValue == model.DOB || DateTime.MaxValue == model.DOB)
            {
                ModelState.AddModelError("dateofbirth", "Please enter Date of Birth");
            }
            else if (String.IsNullOrEmpty(model.Caste))
            {
                ModelState.AddModelError("caste", "Please enter caste");
            }
            else if (String.IsNullOrEmpty(model.Mobile))
            {
                ModelState.AddModelError("mobile", "Please enter mobile");
            }
            else if (String.IsNullOrEmpty(model.PresentAddress))
            {
                ModelState.AddModelError("mobile", "Please enter Present Address");
            }

        }

        private string GetCandidateId()
        {
            string candidateId = string.Empty;

            try
            {
                string emailId = User.Identity.Name;
                Candidate entityCandidate = _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();

                if (entityCandidate != null)
                {
                    candidateId = entityCandidate.CandidateId;
                }
            }
            catch (Exception ex)
            {

            }

            return candidateId;
        }

        [HttpPost]
        public IActionResult Save(CandidateModel model)
        {
            string emailId = User.Identity.Name;
            Candidate entityCandidate = _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();

            ValidateCandidateDetails(model);

            if (ModelState.IsValid)
            {
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
                TempData["CandidateUpdateSuccess"] = "Candidate personal details updated successfully";
                return RedirectToAction("details", "Candidate", new { id = model.CandidateJobProfileMappingId });

            }

            return View("Details", GetCandidateDetails(model.CandidateJobProfileMappingId));
        }

        [HttpPost]
        public IActionResult JobApply(CandidateModel model)
        {
            string emailId = User.Identity.Name;
            string ackNo = "";

            Candidate objCandidate = _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();

            ValidateCandidateDetails(ApplicationHelper.BindCandidateHelperData(objCandidate));

            if (ModelState.IsValid)
            {
                ackNo = Convert.ToString(DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + "" + DateTime.Now.Hour + "" + DateTime.Now.Minute + "" + objCandidate.Id);

                if (objCandidate != null)
                {
                    CandidateJobProfileMapping objCandidateJobProfileMapping = _context.CandidateJobProfileMapping.
                            Where(x => x.CandidateJobProfileMappingId == model.CandidateJobProfileMappingId).FirstOrDefault();

                    objCandidateJobProfileMapping.JobAppliedDate = DateTime.Now;
                    objCandidateJobProfileMapping.Status = "Submitted";
                    objCandidateJobProfileMapping.AknoNumber = ackNo;
                    _context.CandidateJobProfileMapping.Update(objCandidateJobProfileMapping);

                    _context.SaveChanges();

                    return RedirectToAction("JobApplySuccess", "Candidate", new { id = ackNo });
                }
            }

            return View("Details", GetCandidateDetails(model.JobProfileId));
        }

        public async Task<IActionResult> FileUpload(List<IFormFile> files, string fileName, string fileType, string CandidateJobProfileMappingId)
        {

            if (String.IsNullOrEmpty(fileType))
            {
                ModelState.AddModelError("FileType", "Please enter file type");
                return View("Details", GetCandidateDetails(CandidateJobProfileMappingId));
            }

            if (files == null || files.Count() == 0)
            {
                ModelState.AddModelError("Files", "Please upload files");
                return View("Details", GetCandidateDetails(CandidateJobProfileMappingId));
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + _config.GetSection("CandidateFilesPath").Value, formFile.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    CandidateFile objCandidateFile = new CandidateFile();
                    objCandidateFile.Name = fileName;
                    objCandidateFile.CandidateFileId = Guid.NewGuid().ToString();
                    objCandidateFile.CandidateId = _context.LoginDetails.Where(x => x.Username == User.Identity.Name).FirstOrDefault().CandidateId;
                    objCandidateFile.FileType = fileType;
                    objCandidateFile.FilePath = Path.Combine(_config.GetSection("CandidateFilesPath").Value, formFile.FileName);
                    objCandidateFile.IsActive = true;
                    objCandidateFile.Createddate = DateTime.Now;

                    _context.CandidateFiles.Add(objCandidateFile);
                    _context.SaveChanges();
                    TempData["CandidateUpdateSuccess"] = "Document file uploaded successfully.";
                }
            }

            return RedirectToAction("details", "Candidate", new { id = CandidateJobProfileMappingId });
        }


        [HttpPost]
        public ActionResult SaveEducationalDetails(CandidateModel candidateModel)
        {
            string emailId = User.Identity.Name;

            if (candidateModel != null)
            {
                Candidate entityCandidate = _context.Candidate.Where(x => x.Email == emailId).FirstOrDefault();

                if (candidateModel.EducationalDetails != null)
                {
                    //if(candidateModel.EducationalDetails.Where(String.IsNullOrEmpty(x=>x.Qualification)).Any())

                    foreach (var objCandidateModel in candidateModel.EducationalDetails)
                    {
                        EducationalDetails objEducationalDetails = null;

                        if (objCandidateModel.id > 0)
                        {
                            objEducationalDetails = _context.EducationalDetails.Where(x => x.id == objCandidateModel.id).FirstOrDefault();

                            objEducationalDetails.Qualification = objCandidateModel.Qualification;
                            objEducationalDetails.Board = objCandidateModel.Board;
                            objEducationalDetails.YearOfPassing = objCandidateModel.YearOfPassing;
                            objEducationalDetails.Percentage = objCandidateModel.Percentage;
                            _context.Update(objEducationalDetails);
                        }
                        else
                        {
                            if ((!string.IsNullOrEmpty(objCandidateModel.Board))
                                && (!string.IsNullOrEmpty(objCandidateModel.Percentage))
                                    && (!string.IsNullOrEmpty(objCandidateModel.Percentage))
                                    && (!string.IsNullOrEmpty(objCandidateModel.Qualification)))
                            {
                                objEducationalDetails = new EducationalDetails();
                                objEducationalDetails = ApplicationHelper.BindEducationDetailsModelToEntity(objCandidateModel);
                                objEducationalDetails.CandidateId = entityCandidate.CandidateId;
                                _context.Add(objEducationalDetails);

                            }

                        }
                        _context.SaveChanges();

                        TempData["CandidateUpdateSuccess"] = "Educational Details updated successfully.";
                    }
                }
            }
            return RedirectToAction("details", "Candidate", new { id = candidateModel.CandidateJobProfileMappingId });
        }

        public IActionResult Date()
        {
            return View("Datetime");
        }

        private CandidateModel GetCandidateDetails(string candidateJobProfileMappingId)
        {
            CandidateModel objCandidateModel = new CandidateModel();

            CandidateJobProfileMapping entityCandidateJobProfileMapping = _context.CandidateJobProfileMapping.
                                                Where(x => x.CandidateJobProfileMappingId == candidateJobProfileMappingId).FirstOrDefault();

            if (entityCandidateJobProfileMapping != null)
            {
                Candidate entityCandidate = _context.Candidate.Where(x => x.CandidateId == entityCandidateJobProfileMapping.Candidateid).FirstOrDefault();
                if (entityCandidate != null)
                {
                    objCandidateModel = ApplicationHelper.BindCandidateHelperData(entityCandidate);
                    objCandidateModel.JobProfileId = entityCandidateJobProfileMapping.JobProfileId;
                    objCandidateModel.CandidateJobProfileMappingId = candidateJobProfileMappingId;
                    objCandidateModel.JobProfileStatus = entityCandidateJobProfileMapping.Status;
                    objCandidateModel.EducationalDetails = new List<EducationalDetailsModel>();
                    objCandidateModel.EducationalDetails.AddRange(GetEducationalDetails(entityCandidate.CandidateId));
                    objCandidateModel.EducationalDetails.Add(new EducationalDetailsModel()); ;

                    objCandidateModel.Files = new List<CandidateFileModel>();
                    objCandidateModel.Files.AddRange(GetFileDetails(entityCandidate.CandidateId));

                }
            }

            return objCandidateModel;
        }

        public IActionResult JobApplications()
        {
            List<MyJobApplicationsModel> lstCandidateMapping = new List<MyJobApplicationsModel>();

            try
            {
                List<CandidateJobProfileMapping> entityCandidateJobMapping = _context.CandidateJobProfileMapping.Where(x => x.Status == ApplicationHelper.JobProfileStatusSubmitted).ToList();

                if (entityCandidateJobMapping != null)
                {
                    foreach (var entity in entityCandidateJobMapping)
                    {
                        var candidate = _context.Candidate.Where(x => x.CandidateId == entity.Candidateid).FirstOrDefault();

                        string strCandidate = candidate.Fname + " " + candidate.Lname;
                        var jobProfile = _context.JobProfile.Where(x => x.JobProfileId == entity.JobProfileId).FirstOrDefault().Name;
                        lstCandidateMapping.Add(ApplicationHelper.MapJobApplicationsEntityToModel(entity, jobProfile, strCandidate));

                    }
                }
            }
            catch (Exception ex)
            {

            }

            return View("Admin/Applications", lstCandidateMapping);
        }

        public IActionResult MyApplications()
        {
            List<MyJobApplicationsModel> lstCandidateMapping = new List<MyJobApplicationsModel>();

            try
            {
                string candidateId = GetCandidateId();
                List<CandidateJobProfileMapping> entityCandidateJobMapping = _context.CandidateJobProfileMapping.Where(x => x.Candidateid == candidateId).ToList();

                var candidate = _context.Candidate.Where(x => x.CandidateId == candidateId).FirstOrDefault();

                if (entityCandidateJobMapping != null)
                {
                    foreach (var entity in entityCandidateJobMapping)
                    {
                        MyJobApplicationsModel objMyJobApplications = new MyJobApplicationsModel();
                        objMyJobApplications.CandidateId = entity.Candidateid;
                        objMyJobApplications.JobApplicationDate = entity.Createddate.ToLongDateString();
                        objMyJobApplications.Status = entity.Status;
                        objMyJobApplications.AkNo = entity.AknoNumber;
                        objMyJobApplications.CandidateName = candidate.Fname + " " + candidate.Lname;
                        objMyJobApplications.CandidateJobProfileMappingId = entity.CandidateJobProfileMappingId;
                        objMyJobApplications.JobProfileName = _context.JobProfile.Where(x => x.JobProfileId == entity.JobProfileId).FirstOrDefault().Name;
                        lstCandidateMapping.Add(objMyJobApplications);

                    }
                }
            }
            catch (Exception ex)
            {

            }

            return View("MyApplications", lstCandidateMapping);
        }

        public IActionResult JobApply(string id)
        {
            string candidateJobProfileId = Guid.NewGuid().ToString();

            try
            {
                string candidateId = GetCandidateId();
                CandidateJobProfileMapping objCandidateJobProfileMapping = new CandidateJobProfileMapping();
                objCandidateJobProfileMapping.CandidateJobProfileMappingId = candidateJobProfileId;
                objCandidateJobProfileMapping.JobProfileId = id;
                objCandidateJobProfileMapping.Status = "Draft";
                objCandidateJobProfileMapping.Candidateid = candidateId;
                objCandidateJobProfileMapping.Createddate = DateTime.Now;
                _context.CandidateJobProfileMapping.Add(objCandidateJobProfileMapping);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Details", new { @id = candidateJobProfileId });
        }

        public ViewResult AdminDetails(string id)
        {
            CandidateModel objCandidateModel = new CandidateModel();

            // objCandidateModel.Gender = new Selec
            try
            {
                objCandidateModel = GetCandidateDetails(id);
            }
            catch (Exception ex)
            {

            }
            return View("Admin/Details", objCandidateModel);
        }


        public IActionResult Details(string id)
        {
            CandidateModel objCandidateModel = new CandidateModel();

            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    objCandidateModel = GetCandidateDetails(id);
                }
                catch (Exception ex)
                {

                }
                return View(objCandidateModel);
            }
            else
            {
                RedirectToAction("Login", "Candidate");
            }

            return View(objCandidateModel);
        }

        private List<CandidateFileModel> GetFileDetails(string candidateId)
        {
            List<CandidateFile> entityFiles = _context.CandidateFiles.Where(x => x.CandidateId == candidateId).ToList();

            List<CandidateFileModel> lstCandidateFiles = new List<CandidateFileModel>();

            if (entityFiles != null)
            {
                foreach (CandidateFile objCandidateFile in entityFiles)
                {
                    CandidateFileModel model = new CandidateFileModel();
                    model.Name = objCandidateFile.Name;
                    model.FileType = objCandidateFile.FileType;
                    model.Createddate = objCandidateFile.Createddate;
                    model.FilePath = objCandidateFile.FilePath;
                    lstCandidateFiles.Add(model);
                }
            }

            return lstCandidateFiles;
        }

        private List<EducationalDetailsModel> GetEducationalDetails(string candidateId)
        {
            List<EducationalDetailsModel> lstEducationalDetailsModel = new List<EducationalDetailsModel>();

            List<EducationalDetails> educationalDetails = _context.EducationalDetails.Where(x => x.CandidateId == candidateId).ToList();

            foreach (EducationalDetails ed in educationalDetails)
            {
                EducationalDetailsModel objEducationalDetailsModel = new EducationalDetailsModel();
                objEducationalDetailsModel.Percentage = ed.Percentage;
                objEducationalDetailsModel.Board = ed.Board;
                objEducationalDetailsModel.YearOfPassing = ed.YearOfPassing;
                objEducationalDetailsModel.Qualification = ed.Qualification;
                objEducationalDetailsModel.id = ed.id;
                lstEducationalDetailsModel.Add(objEducationalDetailsModel);
            }
            return lstEducationalDetailsModel;
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

                    if (objCandidateJobProfileMapping != null)
                    {
                        objCandidateModel.JobProfileName = _context.JobProfile.Where(x => x.JobProfileId == objCandidateJobProfileMapping.JobProfileId).FirstOrDefault().Name;

                        objCandidateModelList.Add(objCandidateModel);
                    }
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
            var candidateFile = _context.CandidateFiles.Where(x => x.CandidateId == id);

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

                TempData["RegisterSuccess"] = "Account created successfully. You can login using your account.";
                return RedirectToAction(nameof(Login));
            }

            return View();
        }
    }
}
