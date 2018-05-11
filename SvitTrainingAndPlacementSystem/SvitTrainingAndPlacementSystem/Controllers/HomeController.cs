using DbAccess;
using SvitTrainingAndPlacementSystem.Areas.User.Models;
using SvitTrainingAndPlacementSystem.helper;
using SvitTrainingAndPlacementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SvitTrainingAndPlacementSystem.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult password()
        {
            return View();
        }
        [HttpPost]
        public ActionResult password(ForgotpasswordModel model)
        {
            try
            {
                var email = model.Email;
                SvitTrainingPlacementEntities obj = new SvitTrainingPlacementEntities();
                string password = null;
              
                    var verifyemail = obj.Users.Where(p => p.strEmail == email).FirstOrDefault();
                    var verifyemail1=obj.coordinators.Where(p => p.Email == email).FirstOrDefault();
                    var verifyemail2 = obj.Students.Where(p => p.Email == email).FirstOrDefault();
                    if (verifyemail != null )
                    {
                        password = verifyemail.strPassword;
                        sendmail(email, password);
                    }
                if (verifyemail1 != null )
                {
                    password = verifyemail1.Password;
                    sendmail(email, password);
                }
                if ( verifyemail2 != null)
                {
                    password = verifyemail2.Password;
                    sendmail(email, password);
                }



            }
            catch (Exception e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Home");
        }
        private void sendmail(string email, string pass)


        {
            try
            {
                using (MailMessage mm = new MailMessage("pradip.ddu@gmail.com", email))
                {
                    mm.Subject = "login data";
                    mm.Body = "password" + pass;
                    mm.IsBodyHtml = false;
                    SmtpClient smpt = new SmtpClient();
                    smpt.Host = "Smtp.gmail.com";

                    smpt.EnableSsl = true;

                    NetworkCredential Networkcred = new System.Net.NetworkCredential("pradip.ddu@gmail.com", "shiftenter94265");
                    smpt.UseDefaultCredentials = true;
                    smpt.Credentials = Networkcred;

                    smpt.Port = 587;
                    smpt.Send(mm);

                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }







        public ActionResult Index()
        {
            return View();

        }


        /// <summary>
        /// Post Login Submit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginSubmit(LogInModel model)
        {
            if (String.IsNullOrEmpty(model.strEmail))
                {
                ModelState.AddModelError("Email", "Correct Email Required");
                }
            if (String.IsNullOrEmpty(model.strPassword))
            {
                ModelState.AddModelError("Password", "Correct Password Required");
            }


            if (ModelState.IsValid)
            {
                SvitTrainingPlacementEntities objEProvisionEntities = new SvitTrainingPlacementEntities();

                //officers
                User LoginUser = objEProvisionEntities.Users.SingleOrDefault(p => p.strEmail == model.strEmail
                    && p.strPassword == model.strPassword);

                //validation
               

                if (LoginUser != null)
                {
                    //   ClearSession();
                    ProjectSession.intUserID = LoginUser.intUserid;
                    ProjectSession.strEmail = LoginUser.strEmail;
                    ProjectSession.intUserType = 1;
                    objEProvisionEntities.Dispose();
                    return RedirectToAction("Index", "User/CompanyInfo");

                    //   return RedirectToAction("getallcompany", "User/studentcompanyinfo");
                }
                
                //coordinator
                coordinator Logincoordinator = objEProvisionEntities.coordinators.SingleOrDefault(p => p.Email == model.strEmail
                   && p.Password == model.strPassword);

                if (Logincoordinator != null)
                {
                    //   ClearSession();
                    ProjectSession.intUserID = Convert.ToInt32(Logincoordinator.co_ordinatorid);
                    ProjectSession.strEmail = Logincoordinator.Email;
                    ProjectSession.intUserType = 2;
                    objEProvisionEntities.Dispose();
                    return RedirectToAction("StudentList", "User/Student");
                }

                //student
                Student LoginStudent = objEProvisionEntities.Students.SingleOrDefault(p => p.Email == model.strEmail
                   && p.Password == model.strPassword);

                if (LoginStudent != null)
                {
                    //   ClearSession();
                    ProjectSession.intUserID = Convert.ToInt32(LoginStudent.intStudentId);
                    ProjectSession.strEmail = LoginStudent.Email;
                    ProjectSession.intUserType = 3;
                    ProjectSession.intBranchType = LoginStudent.BranchId;
                    objEProvisionEntities.Dispose();
                    return RedirectToAction("Index", "User/studentcompanyinfo");
                }
               
            }
            return RedirectToAction("Index", "Home/Index");
           // return View(model);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            return RedirectToAction("Index", "Home");

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult First()
        {
            return View();
        }
    }


}