using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoAPP.Models
{
    public enum Gender
    {
        Male, Female, Other
    }
    public enum Designation
    {
        Student, Teacher, Doctor, Engineer, [Display(Name = "Job Holder")] JobHolder, Nurse, [Display(Name = "Private Job Holder")] PrivateJob, Others
    }
}