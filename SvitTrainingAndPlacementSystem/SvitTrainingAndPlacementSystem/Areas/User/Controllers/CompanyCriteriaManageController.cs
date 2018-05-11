using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SvitTrainingAndPlacementSystem.Areas.Admin.Controllers
{
    public class CompanyCriteriaManageController : Controller
    {
        // GET: User/CompanyCriteriaManage
        public ActionResult Index()
        {
            return View();
        }
        //For getting the all records from database.		
        public JsonResult getallCompanycriteria()
        {
            SvitTrainingPlacementEntities objsvitTrainingPlacementities = new SvitTrainingPlacementEntities();
            IQueryable<CompanyInfo> query = objsvitTrainingPlacementities.CompanyInfoes;
            var lstofcompany = (from cc in objsvitTrainingPlacementities.Companycriterias
                                join ci in objsvitTrainingPlacementities.CompanyInfoes on cc.CompanyId equals ci.CompanyId
                                join cbn in objsvitTrainingPlacementities.CompanyBranches on ci.CompanyId equals cbn.CompanyId
                                join bn in objsvitTrainingPlacementities.Branches on cbn.BranchId equals bn.BranchId

                                select new
                                {
                                    CompanyName = ci.CompanyName,
                                    Max_Backlogs=cc.Max_Backlogs,
                                    Min_CGPA=cc.Min_CGPA,
                                    Min_Spi = cc.Min_Spi,
                                    std10PR = cc.std10PR,
                                    std12PR = cc.std12PR,
                                    BranchName = bn.BranchName

                                }).ToList();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            // return View(dsCompanyInfo.Tables[0]);
           // var JsonResult = Json(DataTableToJSONWithJavaScriptSerializer(dsCompanycriteria.Tables[0]), JsonRequestBehavior.AllowGet);
            //JsonResult.MaxJsonLength = int.MaxValue;
            //return JsonResult;
            return Json(lstofcompany, JsonRequestBehavior.AllowGet);
        }

        private object DataTableToJSONWithJavaScriptSerializer(object p)
        {
            throw new NotImplementedException();
        }

        [HttpPost]



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