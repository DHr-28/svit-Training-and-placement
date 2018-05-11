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
    public class studentcompanyinfoController : BaseController
    {
        [HttpGet]
        // GET: User/studentcompanyinfo
        public ActionResult Index()
       {
            return View();
        }
        //For getting the all records from database.		
        public JsonResult getallcompany()
        {
            DbAccess.CompanyInfo objCompanyInfo = new DbAccess.CompanyInfo();
            DataSet dsCompanyInfo = DbAccess.CompanyInfo.CompanyInfoSelectAll();
            var lstofcompany = dsCompanyInfo.Tables[0].AsEnumerable().ToList();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            // return View(dsCompanyInfo.Tables[0]);
            var JsonResult = Json(DataTableToJSONWithJavaScriptSerializer(dsCompanyInfo.Tables[0]), JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
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