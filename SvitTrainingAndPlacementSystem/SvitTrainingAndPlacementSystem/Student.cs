//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SvitTrainingAndPlacementSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public long intStudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Enrollment { get; set; }
        public int BranchId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<long> PhoneNumber { get; set; }
        public string Residency { get; set; }
        public decimal CGPA { get; set; }
        public decimal sem1 { get; set; }
        public decimal sem2 { get; set; }
        public decimal sem3 { get; set; }
        public decimal sem4 { get; set; }
        public decimal sem5 { get; set; }
        public decimal sem6 { get; set; }
        public Nullable<decimal> std10PR { get; set; }
        public Nullable<decimal> std12PR { get; set; }
    
        public virtual Branch Branch { get; set; }
    }
}