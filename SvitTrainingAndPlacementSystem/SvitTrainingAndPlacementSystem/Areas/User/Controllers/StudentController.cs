using SvitTrainingAndPlacementSystem.Infrastructure;
using SvitTrainingAndPlacementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SvitTrainingAndPlacementSystem.Areas.Admin.Controllers
{
    public class StudentController : BaseController
    { // GET: Faculty/Student
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BulkStudentCSV()
        {
            StudentModel objStudentModel = new StudentModel();
            return View(objStudentModel);
        }

        [HttpPost]
        public ActionResult BulkStudentCSV(StudentModel model)
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase FileUpload = model.bulkuploads;
                StreamReader reader = new StreamReader(FileUpload.InputStream);
                csvToDataTable(reader, ',');
            }

            return View(model);
        }

        private bool csvToDataTable(StreamReader sr, char splitCharacter)
        {
            try
            {

                string myStringRow = sr.ReadLine();
                var rows = myStringRow.Split(splitCharacter);
                DataTable CsvData = new DataTable();
                foreach (string column in rows)
                {
                    //creates the columns of new datatable based on first row of csv

                    CsvData.Columns.Add(column);
                }
                int Enrollmentindex = CsvData.Columns.IndexOf("Enrollment");
                int FirstNameindex = CsvData.Columns.IndexOf("FirstName");
                int LastNameindex = CsvData.Columns.IndexOf("LastName");
                int Branchidindex = CsvData.Columns.IndexOf("BranchID");
                int Emailindex = CsvData.Columns.IndexOf("Email");
                int PhoneNumberindex = CsvData.Columns.IndexOf("PhoneNumber");
                int Residencyindex = CsvData.Columns.IndexOf("Residency");
                int sem1index = CsvData.Columns.IndexOf("sem1");
                int sem2index = CsvData.Columns.IndexOf("sem2");
                int sem3index = CsvData.Columns.IndexOf("sem3");
                int sem4index = CsvData.Columns.IndexOf("sem4");
                int sem5index = CsvData.Columns.IndexOf("sem5");
                int sem6index = CsvData.Columns.IndexOf("sem6");
                int CGPAindex = CsvData.Columns.IndexOf("CGPA");
                int std10PRindex = CsvData.Columns.IndexOf("std10PR");
                int std12PRindex = CsvData.Columns.IndexOf("std12PR");

                SvitTrainingPlacementEntities objEProvisionEntities = new SvitTrainingPlacementEntities();
                myStringRow = sr.ReadLine();
                while (myStringRow != null)
                {
                    //runs until string reader returns null and adds rows to dt 
                    //check position of Address column where there is a "vbv,vbv,"
                    String[] totalblock = myStringRow.Split('"');
                    String[] RecordRawArray = totalblock[0].Split(',');
                    //RecordRawArray[RecordRawArray.Length - 1] = totalblock[1];
                    // [0].Split(',')
                    // rows = myStringRow.Split(splitCharacter);


                    //create Students entityobj
                    SvitTrainingAndPlacementSystem.Student objStudents = new SvitTrainingAndPlacementSystem.Student();
                    objStudents.BranchId = 1;
                    objStudents.Enrollment = RecordRawArray[Enrollmentindex];
                    objStudents.FirstName = RecordRawArray[FirstNameindex];
                    objStudents.LastName = RecordRawArray[LastNameindex];
                    objStudents.BranchId = Convert.ToInt32(RecordRawArray[Branchidindex]);
                    objStudents.sem1 = Convert.ToDecimal(RecordRawArray[sem1index]);
                    objStudents.sem2 = Convert.ToDecimal(RecordRawArray[sem2index]);
                    objStudents.sem3 = Convert.ToDecimal(RecordRawArray[sem3index]);
                    objStudents.sem4 = Convert.ToDecimal(RecordRawArray[sem4index]);
                    objStudents.sem5 = Convert.ToDecimal(RecordRawArray[sem5index]);
                    objStudents.sem6 = Convert.ToDecimal(RecordRawArray[sem6index]);
                    objStudents.CGPA = Convert.ToDecimal(RecordRawArray[CGPAindex]);
                    objStudents.std10PR = Convert.ToDecimal(RecordRawArray[std10PRindex]);
                    objStudents.std12PR = Convert.ToDecimal(RecordRawArray[std12PRindex]);
                    objStudents.Email = RecordRawArray[Emailindex];
                    objStudents.PhoneNumber = Convert.ToInt64(RecordRawArray[PhoneNumberindex]);
                    objStudents.Residency = RecordRawArray[Residencyindex];
                    objStudents.Password = System.Web.Security.Membership.GeneratePassword(6, 2);



                    if (objEProvisionEntities.Students.Any(e => e.Enrollment == objStudents.Enrollment))
                    {
                        var getExistStudent = objEProvisionEntities.Students.Where(e => e.Enrollment == objStudents.Enrollment).
                            FirstOrDefault();
                        getExistStudent.FirstName = objStudents.FirstName;
                        getExistStudent.LastName = RecordRawArray[LastNameindex];
                        getExistStudent.Email = RecordRawArray[Emailindex];
                        getExistStudent.BranchId = Convert.ToInt32(RecordRawArray[Branchidindex]);
                        getExistStudent.Enrollment = RecordRawArray[Enrollmentindex];
                        getExistStudent.sem1 = Convert.ToDecimal(RecordRawArray[sem1index]);
                        getExistStudent.sem2 = Convert.ToDecimal(RecordRawArray[sem2index]);
                        getExistStudent.sem3 = Convert.ToDecimal(RecordRawArray[sem3index]);
                        getExistStudent.sem4 = Convert.ToDecimal(RecordRawArray[sem4index]);
                        getExistStudent.sem5 = Convert.ToDecimal(RecordRawArray[sem5index]);
                        getExistStudent.sem6 = Convert.ToDecimal(RecordRawArray[sem6index]);
                        getExistStudent.CGPA = Convert.ToDecimal(RecordRawArray[CGPAindex]);
                        objStudents.std10PR = Convert.ToDecimal(RecordRawArray[std10PRindex]);
                        objStudents.std12PR = Convert.ToDecimal(RecordRawArray[std12PRindex]);
                        getExistStudent.PhoneNumber = Convert.ToInt64(RecordRawArray[PhoneNumberindex]);
                        getExistStudent.Residency = RecordRawArray[Residencyindex];
                        getExistStudent.Password = System.Web.Security.Membership.GeneratePassword(6, 2);
                        //objEProvisionEntities.Students.Attach(objStudents);
                        objEProvisionEntities.Entry(getExistStudent).State = System.Data.Entity.EntityState.Modified;
                       
                        sendemail(getExistStudent.Email, getExistStudent.Password);
                    }
                    else
                    {
                        objEProvisionEntities.Students.Add(objStudents);
                    }

                    objEProvisionEntities.SaveChanges();


                    // CsvData.Rows.Add(RecordRawArray);
                    myStringRow = sr.ReadLine();
                }
                sr.Close();
                sr.Dispose();

                objEProvisionEntities.Dispose();

                this.ShowMessage("Success", "Csv file uploaded successfully");
                return true;
            }
            catch (Exception e)
            {

            }
            return true;
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

        public ActionResult StudentList()
        {
            return View();
        }

        public JsonResult gatallstudentlist()
        {
            SvitTrainingPlacementEntities objsvitTrainingPlacementities = new SvitTrainingPlacementEntities();
            IQueryable<Student> query = objsvitTrainingPlacementities.Students;
            //var lststudent = query.Select(p => new { p.FirstName, p.LastName, p.Enrollment, p.intStudentId }).ToList();
            var lststudent = (from sa in objsvitTrainingPlacementities.Students
                              join bn in objsvitTrainingPlacementities.Branches on sa.BranchId equals bn.BranchId

                              select new
                              {
                                  FirstName = sa.FirstName,
                                  LastName=sa.LastName,
                                  Enrollment=sa.Enrollment,
                                  BranchName = bn.BranchName,
                                  intStudentId=sa.intStudentId

                              }).ToList();

            return Json(lststudent, JsonRequestBehavior.AllowGet);
        }


        public ActionResult StudentDetail(int id)
        {
            SvitTrainingPlacementEntities objsvitTrainingPlacementities = new SvitTrainingPlacementEntities();
            Student getStudent = objsvitTrainingPlacementities.Students.Where(p => p.intStudentId == id).FirstOrDefault();
            return View(getStudent);
        }

        [HttpPost]
        public ActionResult StudentDetailSubmit(Student Studentmodel)
        {
            if (Studentmodel != null)
            {
                using (SvitTrainingPlacementEntities objsvitTrainingPlacementities = new SvitTrainingPlacementEntities())
                {
                    int no = Convert.ToInt32(Studentmodel.intStudentId);
                    var employeeList = objsvitTrainingPlacementities.Students.Where(p => p.intStudentId == no).FirstOrDefault();
                    employeeList.FirstName = Studentmodel.FirstName;
                    employeeList.LastName = Studentmodel.LastName;
                    employeeList.Email = Studentmodel.Email;
                    employeeList.PhoneNumber = Studentmodel.PhoneNumber;
                    employeeList.Enrollment = Studentmodel.Enrollment;
                    employeeList.Residency = Studentmodel.Residency;
                    employeeList.CGPA = Studentmodel.CGPA;
                    employeeList.sem1 = Studentmodel.sem1;
                    employeeList.sem2 = Studentmodel.sem2;
                    employeeList.sem3 = Studentmodel.sem3;
                    employeeList.sem4 = Studentmodel.sem4;
                    employeeList.sem5 = Studentmodel.sem5;
                    employeeList.sem6 = Studentmodel.sem6;
                    employeeList.std10PR = Studentmodel.std10PR;
                    employeeList.std12PR = Studentmodel.std12PR;
                    objsvitTrainingPlacementities.SaveChanges();
                }
                this.ShowMessage("Success", "Student  Info Updated Successfully");
                return RedirectToAction("StudentList");
            }
            else
            {
                 this.ShowMessage("UnSuccess", "Student  Info Not Updated");
            }

            return View(Studentmodel);

            }

        }
    }

