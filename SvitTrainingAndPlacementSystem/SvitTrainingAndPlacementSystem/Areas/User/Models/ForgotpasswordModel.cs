using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SvitTrainingAndPlacementSystem.Areas.User.Models
{
    public class ForgotpasswordModel
    {
        public string Email { get; set; }
        public SelectList branchselectlist { get; set; }
    }
}