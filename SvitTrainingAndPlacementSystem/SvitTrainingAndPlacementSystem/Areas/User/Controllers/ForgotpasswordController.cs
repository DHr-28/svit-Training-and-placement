using SvitTrainingAndPlacementSystem.Areas.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SvitTrainingAndPlacementSystem.Areas.User.Controllers
{
    public class ForgotpasswordController : Controller
    {
        // GET: User/Forgotpassword
        [HttpGet]
        public ActionResult password()
        {
            return View();
        }
        [HttpPost]
        public ActionResult password(ForgotpasswordModel model)
        {
            try {
                var email = model.Email;
                SvitTrainingPlacementEntities obj = new SvitTrainingPlacementEntities();
                string password = null;
                if (password == null)
                {
                    var verifyemail = obj.Users.Where(p => p.strEmail == email).FirstOrDefault();
                    
                    if (verifyemail != null)
                        {
                            password = verifyemail.strPassword;
                        }
                   
                    
                }

                sendmail(email, password);
            }
            catch(Exception e)
            {
                throw e;
            }
            return RedirectToAction("Index", "Home");
        }
     private void sendmail(string email,string pass)


        {
            using (MailMessage mm = new MailMessage("pradip.ddu@gmail.com", email))
            {
                mm.Subject = "login data";
                mm.Body = "password" + pass;
                mm.IsBodyHtml = false;
                SmtpClient smpt = new SmtpClient();
                smpt.Host = "Smtp.gmail.com";
                smpt.Port = 587;
                NetworkCredential Networkcred = new System.Net.NetworkCredential("pradip.ddu@gmail.com", "shiftenter94265");
                smpt.Credentials= Networkcred;
                smpt.EnableSsl = true;
                smpt.UseDefaultCredentials = true;

                smpt.Send(mm);

            }
                      
         }

            

            
        }

    }
