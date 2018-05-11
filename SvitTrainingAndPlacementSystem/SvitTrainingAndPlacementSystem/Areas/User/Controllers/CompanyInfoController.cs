
using SvitTrainingAndPlacementSystem.Areas.User.Models;
using SvitTrainingAndPlacementSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SvitTrainingAndPlacementSystem.Areas.Admin.Controllers
{
    public class CompanyInfoController : BaseController
    {

        public ActionResult Index()
        {
            return View();

        }

        //For getting the all records from database.		
        public JsonResult getAll()
        {
            DbAccess.CompanyInfo objCompanyInfo = new DbAccess.CompanyInfo();
            DataSet dsCompanyInfo = DbAccess.CompanyInfo.CompanyInfoSelectAll();
            var lstofcompany = dsCompanyInfo.Tables[0].AsEnumerable().ToList();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            // return View(dsCompanyInfo.Tables[0]);
            var JsonResult =  Json(DataTableToJSONWithJavaScriptSerializer(dsCompanyInfo.Tables[0]), JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public JsonResult BranchSelectAll()
        {
            using (SvitTrainingPlacementEntities objSvitTrainingPlacementEntities = new SvitTrainingPlacementEntities())
            {
                objSvitTrainingPlacementEntities.Configuration.LazyLoadingEnabled = false;
                IQueryable<Branch> query = objSvitTrainingPlacementEntities.Branches;//.Include("CompanyBranches");

                var lstBranch = query.Select(p => new { p.BranchId, p.BranchName }).ToList();// objSvitTrainingPlacementEntities.Branches.ToList();
                return Json(lstBranch, JsonRequestBehavior.AllowGet);
            }
        }


        public string UpdateCompnayInfo(CompanyInfo Emp)
        {
            if (Emp != null)
            {
                using (SvitTrainingPlacementEntities dataContext = new SvitTrainingPlacementEntities())
                {
                    int no = Convert.ToInt32(Emp.CompanyId);
                    var employeeList = dataContext.CompanyInfoes.Where(x => x.CompanyId == no).FirstOrDefault();
                    employeeList.CompanyName = Emp.CompanyName;
                    employeeList.Domain = Emp.Domain;
                    employeeList.Address = Emp.Address;
                    employeeList.WebsiteName = Emp.WebsiteName;
                    employeeList.IsTrainingProvide = Emp.IsTrainingProvide;
                    employeeList.IsPlacementProvide = Emp.IsPlacementProvide;
                    employeeList.ModifiedDate = DateTime.Now;
                    dataContext.SaveChanges();
                    dataContext.CompanyBranches.RemoveRange(employeeList.CompanyBranches);
                    // dataContext.SaveChanges();
                    //}

                    //reinsert all branches now.

                    foreach (CompanyBranch item in Emp.CompanyBranches)
                    {
                        CompanyBranch objCompanyBranch = new CompanyBranch();
                        objCompanyBranch.CompanyId = employeeList.CompanyId;
                        objCompanyBranch.BranchId = item.BranchId;
                        dataContext.CompanyBranches.Add(objCompanyBranch);
                        dataContext.SaveChanges();
                    }



                    return "Company Info Updated Successfully";
                }
            }
            else
            {
                return "Invalid Company";
            }
        }

        public string AddCompnayInfo(CompanyInfo Emp)
        {
            if (Emp != null)
            {
                using (SvitTrainingPlacementEntities dataContext = new SvitTrainingPlacementEntities())
                {
                    CompanyInfo employee = new CompanyInfo();
                    employee.CompanyName = Emp.CompanyName;
                    employee.WebsiteName = Emp.WebsiteName;
                    employee.Domain = Emp.Domain;
                    employee.Address = Emp.Address;
                    employee.City = "Avad";
                    employee.Address = Emp.Address;
                    employee.IsTrainingProvide = Emp.IsTrainingProvide;
                    employee.IsPlacementProvide = Emp.IsPlacementProvide;
                    employee.CreatedDate = DateTime.Now;
                    employee.ModifiedDate = DateTime.Now;
                    dataContext.CompanyInfoes.Add(employee);
                    dataContext.SaveChanges();

                    //after companyinformation saved save compnaybranch info.


                    foreach (CompanyBranch item in Emp.CompanyBranches)
                    {
                        CompanyBranch objCompanyBranch = new CompanyBranch();
                        objCompanyBranch.CompanyId = employee.CompanyId;
                        objCompanyBranch.BranchId = item.BranchId;
                        dataContext.CompanyBranches.Add(objCompanyBranch);
                        dataContext.SaveChanges();
                    }

                    return "Company added Successfully";
                }
            }
            else
            {
                return "Addition of Employee unsucessfull !";
            }
        }

        //get company info from id.
        public ActionResult CompanyCriteriaManage(int id)
        {
            CompanyCriteriaModel objCompanyCriteriaModel = new CompanyCriteriaModel();
            objCompanyCriteriaModel.CompanyId = id;
            return View(objCompanyCriteriaModel);
        }

        [HttpPost]
        public ActionResult CompanyCriteriaManage(CompanyCriteriaModel objCompanyCriteriaModel)
        {
            using (SvitTrainingPlacementEntities objsvitTrainingPlacementities = new SvitTrainingPlacementEntities())
            {
                Companycriteria objCompanycriteria = new Companycriteria();
                objCompanycriteria.CompanyId = objCompanyCriteriaModel.CompanyId;
                objCompanycriteria.Max_Backlogs = objCompanyCriteriaModel.Max_Backlogs;
                objCompanycriteria.Min_CGPA = objCompanyCriteriaModel.Min_CGPA;
                objCompanycriteria.Min_Spi = objCompanyCriteriaModel.Min_Spi;
                objCompanycriteria.std10PR = objCompanyCriteriaModel.std10PR;
                objCompanycriteria.std12PR = objCompanyCriteriaModel.std12PR;


                objsvitTrainingPlacementities.Companycriterias.Add(objCompanycriteria);
                objsvitTrainingPlacementities.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public JsonResult getCompanyInfoById(string id)
        {
            try
            {
                using (SvitTrainingPlacementEntities dataContext = new SvitTrainingPlacementEntities())
                {
                    int no = Convert.ToInt32(id);
                    CompanyInfo employeeList = dataContext.CompanyInfoes.Find(no);
                    employeeList.CompanyBranches = employeeList.CompanyBranches.Select(p => new CompanyBranch
                    { BranchId = p.BranchId }).ToList();
                    return Json(employeeList, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exp)
            {
                return Json("Error in getting record !", JsonRequestBehavior.AllowGet);
            }

        }

        public object DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Deserialize<object>(jsSerializer.Serialize(parentRow));
        }


    }
}