using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SvitTrainingAndPlacementSystem.Areas.User.Models
{
    public class CordinatorModel
    {
        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please EnterLast Name")]
        [Display(Name = "Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        [EmailAddress(ErrorMessage = "please enter in a proper format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile No")]
        [Display(Name = "Mobile")]
        [StringLength(10, ErrorMessage = "The Mobile must contains 10 characters", MinimumLength = 10)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "enter mobile number in digits")]

        public string PhoneNumber { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Address")]
        [MaxLength(30, ErrorMessage = "Address upto 30 character is allowed ")]
        public string Residency { get; set; }


        [Required(ErrorMessage = "Please Enter BranchName")]
        public int BranchId { get; set; }
        [Required(ErrorMessage = "Select Your Gender")]
        public string gender { get; set; }

        public SelectList branchselectlist { get; set; }


    }
    public class MailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}