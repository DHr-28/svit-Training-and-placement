using SvitTrainingAndPlacementSystem.Areas.User.Models;
using SvitTrainingAndPlacementSystem.helper;
using SvitTrainingAndPlacementSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace SvitTrainingAndPlacementSystem.Areas.Admin.Controllers
{
    public class CodController : BaseController
    {
        private object db;

        // GET: User/Cod
        public ActionResult CoordinatorList()
        {
            return View();
        }

        public ActionResult AptitudeTestList()
        {
            return View();
        }
        public ActionResult CreateCordinator()
        {
           
            
            CordinatorModel objCordinatorModel = new CordinatorModel();
            using (SvitTrainingPlacementEntities objSvitTrainingPlacementEntities = new SvitTrainingPlacementEntities())
            {
                objSvitTrainingPlacementEntities.Configuration.LazyLoadingEnabled = false;
                IQueryable<Branch> query = objSvitTrainingPlacementEntities.Branches;//.Include("CompanyBranches");

                var lstBranch = query.Select(p => new { p.BranchId, p.BranchName }).ToList();// objSvitTrainingPlacementEntities.Branches.ToList();

                objCordinatorModel.branchselectlist = new SelectList(lstBranch, "BranchId", "BranchName");
            }

            return View(objCordinatorModel);
        }

        public ActionResult SendEmail()
        {
            CordinatorModel objCordinatorModel = new CordinatorModel();
            using (SvitTrainingPlacementEntities objSvitTrainingPlacementEntities = new SvitTrainingPlacementEntities())
            {
                objSvitTrainingPlacementEntities.Configuration.LazyLoadingEnabled = false;
                IQueryable<Branch> query = objSvitTrainingPlacementEntities.Branches;//.Include("CompanyBranches");

                var lstBranch = query.Select(p => new { p.BranchId, p.BranchName }).ToList();// objSvitTrainingPlacementEntities.Branches.ToList();

                objCordinatorModel.branchselectlist = new SelectList(lstBranch, "BranchId", "BranchName");
            }

            return View(objCordinatorModel);
        }

        public ActionResult Aptitude_Test()
        {
            Aptitude_TestModel objAptitude_TestModel = new Aptitude_TestModel();
            using (SvitTrainingPlacementEntities objSvitTrainingPlacementEntities = new SvitTrainingPlacementEntities())
            {
                objSvitTrainingPlacementEntities.Configuration.LazyLoadingEnabled = false;
                IQueryable<Branch> query = objSvitTrainingPlacementEntities.Branches ;//.Include("CompanyBranches");

                var lstBranch = query.Select(p => new { p.BranchId, p.BranchName }).ToList();// objSvitTrainingPlacementEntities.Branches.ToList();

                objAptitude_TestModel.branchselectlist = new SelectList(lstBranch, "BranchId", "BranchName");
            }

            return View(objAptitude_TestModel);
        }
      



        [HttpPost]
        public ActionResult CreateCordinator(CordinatorModel objCordinatorModel)
        {
            using (SvitTrainingPlacementEntities objSvitTrainingPlacementEntities = new SvitTrainingPlacementEntities())
            {

                
                try
                {
                    coordinator objcoordinator = new coordinator();
                    objcoordinator.FirstName = objCordinatorModel.FirstName;
                    objcoordinator.LastName = objCordinatorModel.LastName;
                    objcoordinator.Email = objCordinatorModel.Email;
                    objcoordinator.Password = System.Web.Security.Membership.GeneratePassword(6, 2);//objCordinatorModel.Password;
                    objcoordinator.PhoneNumber = Convert.ToInt64(objCordinatorModel.PhoneNumber);
                    objcoordinator.gender = objCordinatorModel.gender;  
                    objcoordinator.Residency = objCordinatorModel.Residency;
                    objcoordinator.BranchId = objCordinatorModel.BranchId;
                    
                    objSvitTrainingPlacementEntities.coordinators.Add(objcoordinator);
                    objSvitTrainingPlacementEntities.SaveChanges();
                    //send mail of
                    sendemail(objcoordinator.Email, objcoordinator.Password);



                }
                catch (Exception e)
                {

                }
                return RedirectToAction("CoordinatorList");
            }
        }

        private void sendemail(string toemail, string mailcontent)
        {
            using (MailMessage mm = new MailMessage("pradip.ddu@gmail.com", toemail))
            {
                mm.Subject = "LoginCredentials";
                mm.Body = "Your login pasword is " + mailcontent;
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("pradip.ddu@gmail.com", "shiftenter94265");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
        [HttpPost]
        public ActionResult SendEmail(CordinatorModel objCordinatorModel, HttpPostedFileBase fileUploader)
        {
            using (SvitTrainingPlacementEntities objSvitTrainingPlacementEntities = new SvitTrainingPlacementEntities())
            {
                coordinator objcoordinator = new coordinator();
                objcoordinator.Email = objCordinatorModel.Email;
                objcoordinator.BranchId = objCordinatorModel.BranchId;

                SvitTrainingPlacementEntities objsvitTrainingPlacementities = new SvitTrainingPlacementEntities();
                IQueryable<Student> query = objsvitTrainingPlacementities.Students;
                foreach (var item in query)
                {
                    if (item.BranchId == objCordinatorModel.BranchId)
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                        mail.From = new MailAddress("your_email_address@gmail.com");
                        mail.To.Add(item.Email.ToString());
                        mail.Subject = "Test Mail";
                        mail.Body = "This is for testing SMTP mail from GMAIL";
                        if (fileUploader != null)
                        {
                            string fileName = Path.GetFileName(fileUploader.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                        }
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("pradip.ddu@gmail.com", "shiftenter94265");
                        SmtpServer.EnableSsl = true;

                        SmtpServer.Send(mail);
                    }

                }

            }

            return RedirectToAction("CoordinatorList");
        }


        [HttpPost]
        public ActionResult Aptitude_Test(Aptitude_TestModel objAptitude_TestModel)
        {
            using (SvitTrainingPlacementEntities objSvitTrainingPlacementEntities = new SvitTrainingPlacementEntities())
            {
                Aptitude_Test objAptitude_Test = new Aptitude_Test();
                objAptitude_Test.Branch_id = objAptitude_TestModel.BranchId;
                objAptitude_Test.Date = objAptitude_TestModel.Date;

                objSvitTrainingPlacementEntities.Aptitude_Test.Add(objAptitude_Test);
                objSvitTrainingPlacementEntities.SaveChanges();
            }

            return RedirectToAction("AptitudeTestList");
        }
        public JsonResult gatallcordinTORlist()
        {
            SvitTrainingPlacementEntities objsvitTrainingPlacementities = new SvitTrainingPlacementEntities();
            IQueryable<coordinator> query = objsvitTrainingPlacementities.coordinators;
           // var lststudent = query.Select(p => new { p.FirstName, p.LastName, p.Email, p.PhoneNumber , p.BranchId , p.Residency })
                var lststudent = (from sa in objsvitTrainingPlacementities.coordinators
                                  join bn in objsvitTrainingPlacementities.Branches on sa.BranchId equals bn.BranchId

                                  select new
                                  {
                                      FirstName = sa.FirstName,
                                      LastName = sa.LastName,
                                      Gender = sa.gender,
                                      Email = sa.Email,
                                      BranchName = bn.BranchName,
                                      PhoneNumber = sa.PhoneNumber,
                                      Residency = sa.Residency,
                                      intCoordinatorId = sa.co_ordinatorid

                                  }).ToList();
            return Json(lststudent, JsonRequestBehavior.AllowGet);
        }

      
        // public JsonResult gatallAptitudeTestlist()



        public CustomJsonResult gatallAptitudeTestlist()
        {
            SvitTrainingPlacementEntities objsvitTrainingPlacementities = new SvitTrainingPlacementEntities();
            IQueryable<Aptitude_Test> query = objsvitTrainingPlacementities.Aptitude_Test;
            IQueryable<Branch> queryB = objsvitTrainingPlacementities.Branches;

            // var lststudent = query .Select(p => new { p.Date, p.Branch_id }).ToList();
            var lststudent = (from sa in objsvitTrainingPlacementities.Aptitude_Test
                              join bn in objsvitTrainingPlacementities.Branches 
                              on sa.Branch_id equals bn.BranchId

                              select new
                              {
                                  Date = sa.Date,
                                  Branch_id = sa.Branch_id,
                                  BranchName = bn.BranchName

                              }) .Where(a=>a.Branch_id == ProjectSession.intBranchType). ToList();


            // return Json(lststudent, JsonRequestBehavior.AllowGet);
            return new CustomJsonResult(lststudent, JsonRequestBehavior.AllowGet);
        }

    }
}