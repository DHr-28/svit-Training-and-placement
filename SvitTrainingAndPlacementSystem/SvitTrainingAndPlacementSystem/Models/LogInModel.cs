using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SvitTrainingAndPlacementSystem.Models
{
    public class LogInModel
    {
        /// <summary>
        /// Gets or sets the StrEmail value.
        /// </summary>
       [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
        ErrorMessage = "In Valid Email")]
       [Display(Name = "User ID")]
       [DataType(DataType.EmailAddress)]
        public string strEmail
        {
            get;
            set;
        }
        /// <summary>
        /// Get or Sets strPassword value
        /// </summary>
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(50)]
        public string strPassword { get; set; }
    }
}