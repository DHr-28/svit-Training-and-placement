using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvitTrainingAndPlacementSystem.helper
{
    public class ProjectSession
    {
        /// <summary>
        /// Get or Sets intUserID value
        /// </summary>
        public static int intUserID
        {
            get
            {
                if (HttpContext.Current.Session["UserID"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                }
            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }

        public static int intUserType
        {
            get
            {
                if (HttpContext.Current.Session["intUserType"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Session["intUserType"]);
                }
            }
            set
            {
                HttpContext.Current.Session["intUserType"] = value;
            }
        }

        public static int intBranchType
        {
            get
            {
                if (HttpContext.Current.Session["intBranchType"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(HttpContext.Current.Session["intBranchType"]);
                }
            }
            set
            {
                HttpContext.Current.Session["intBranchType"] = value;
            }
        }

        public static string strEmail
        {
            get
            {
                if (HttpContext.Current.Session["strEmail"] == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToString(HttpContext.Current.Session["strEmail"]);
                }
            }
            set
            {
                HttpContext.Current.Session["strEmail"] = value;
            }
        }
    }
}