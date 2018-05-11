using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SvitTrainingAndPlacementSystem.Areas.User.Models
{
    public class Aptitude_TestModel
    {

        public int Aptitude_Test_id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Date")]
        public DateTime Date { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Branch")]
        public int BranchId { get; set; }

        public SelectList branchselectlist { get; set; }
    }
}