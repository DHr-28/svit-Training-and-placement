using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SvitTrainingAndPlacementSystem.Models
{
    public class StudentModel
    {
        public HttpPostedFileBase uploads
        {
            get;
            set;
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }

        public String Enrollment { get; set; }


        public int BranchId { get; set; }

        public int CompanyId { get; set; }

        public String Email { get; set; }
        public String Password { get; set; }
        public String Residency { get; set; }
        public int PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets uploads
        /// </summary>
        public HttpPostedFileBase bulkuploads
        {
            get;
            set;
        }
    }
}