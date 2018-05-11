using SvitTrainingAndPlacementSystem.Infrastructure;
using SvitTrainingAndPlacementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SvitTrainingAndPlacementSystem.Areas.Admin.Controllers
{
    public class PlacedstudentController : Controller
    {
        // GET: User/Placedstudent
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
                int Companyidindex = CsvData.Columns.IndexOf("CompanyId");

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
                    SvitTrainingAndPlacementSystem.Placedstudent objStudents = new SvitTrainingAndPlacementSystem.Placedstudent();
                    objStudents.Branchid = 1;
                    objStudents.Enrollment = RecordRawArray[Enrollmentindex];
                    objStudents.firstname = RecordRawArray[FirstNameindex];
                    objStudents.lastname = RecordRawArray[LastNameindex];
                    objStudents.Branchid = Convert.ToInt32(RecordRawArray[Branchidindex]);
                    objStudents.companyid = Convert.ToInt32(RecordRawArray[Companyidindex]);



                   
                        objEProvisionEntities.Placedstudents.Add(objStudents);

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


        [HttpGet]
        public ActionResult PlacedStudentList()
        {
            return View();
        }

        public JsonResult gatallplacedstudentlist()
        {
            SvitTrainingPlacementEntities objsvitTrainingPlacementities = new SvitTrainingPlacementEntities();
            IQueryable<Placedstudent> query = objsvitTrainingPlacementities.Placedstudents;
            //var lststudent = query.Select(p => new { p.FirstName, p.LastName, p.Enrollment, p.intStudentId }).ToList();
            var lststudent = (from sa in objsvitTrainingPlacementities.Placedstudents
                              join bn in objsvitTrainingPlacementities.Branches on sa.Branchid equals bn.BranchId
                              join ci in objsvitTrainingPlacementities.CompanyInfoes on sa.companyid equals ci.CompanyId
                              select new
                              {    
                                  FirstName = sa.firstname,
                                  LastName = sa.lastname,
                                  Enrollment = sa.Enrollment,
                                  BranchName = bn.BranchName,
                                  CompanyName = ci.CompanyName,
                                  intStudentId = sa.intstudentid
                                  
                              }).ToList();

            return Json(lststudent, JsonRequestBehavior.AllowGet);
        }


    }
}