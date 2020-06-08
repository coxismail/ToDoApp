using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoAPP.Models
{
    public class UserDetails
    {
        [Key, ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Genders { get; set; }
        [Display(Name = "Date of Birth"), DataType(DataType.Date)]
        public DateTime? Dob { get; set; }
        [Display(Name = "Mobile Number"), Required]
        public string MobileNumber { get; set; }
        public int? RetryAttempt { get; set; }
        public bool? Status { get; set; }
        public bool? Islocked { get; set; }
        public Designation Designation { get; set; }
        public DateTime? LockedDateTime { get; set; }

        public DateTime? CreatedOn { get; set; }

        [DisplayName("Country/Region")]
        public string Country { get; set; }
        [DisplayName("State/Province")]
        public string State { get; set; }
        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Present Address"), DataType(DataType.MultilineText)]
        public string PresentAddress { get; set; }
        [DisplayName("Permanent Address"), DataType(DataType.MultilineText)]
        public string PermanentAddress { get; set; }
        [DisplayName("Photo"), DataType(DataType.ImageUrl)]
        public string PhotosUrl { get; set; }
        [DisplayName("Notes"), DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        [Display(Name="Upload Image")]
        public HttpPostedFileBase ImageUpload { get; set; }
        public UserDetails()
        {
            PhotosUrl = "~/Image/user_logo/user.png";
            CreatedOn = DateTime.Now;
        }
    }
}