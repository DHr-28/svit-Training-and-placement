using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SvitTrainingAndPlacementSystem.Areas.User.Models
{
    public class CompanyCriteriaModel
    {
        
        public decimal Min_CGPA { get; set; }
       
        public decimal Min_Spi { get; set; }
        
        public int Max_Backlogs { get; set; }
        
        public decimal std10PR { get; set; }
        
        public decimal std12PR { get; set; }
        public int CompanyId { get; set; }


    }
}