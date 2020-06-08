using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDoAPP.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [Display(Name="Category Name")]
        public string Name { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<MyTask> MyTask { get; set; }

    }
    public class MyTask {
        public Guid Id { get; set; }
        [Display(Name = "Task Title"), Required, MinLength(3), MaxLength(50)]
        public string TaskName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Notes"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Complete")]
        public bool IsComplete { get; set; }
        [Display(Name = "Pending")]
        public bool IsPending { get; set; }
        [Required]
        [ ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public MyTask()
        {
            IsComplete = false;
            IsPending = true;
        }

    }
}