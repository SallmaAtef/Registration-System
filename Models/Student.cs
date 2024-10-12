using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Student
    {
        
        public int Id { get; set; }
        
        
        [DisplayName("Student")]

        [Required(ErrorMessage = "You Have to provide a valid fullname.")]
        [MinLength(2, ErrorMessage = "Full name musn't be less than 2 charcters.")]
        [MaxLength(20, ErrorMessage = "Full name musn't excee 20 charcters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "You Have to provide a valid faculty.")]
        [MinLength(2, ErrorMessage = "faculty name musn't be less than 2 charcters.")]
        [MaxLength(20, ErrorMessage = "faculty name musn't excee 20 charcters")]

       
        public string Faculty { get; set; }

        [DisplayName("BirthDate")]
        public DateTime BirthDate { get; set; }

        [DisplayName("JoinDate")]
        public DateTime JoinDate { get; set; }

        [DisplayName("IsActive")]
        public bool IsActive { get; set; }
        
        [ValidateNever]
        public List<Instructor> Instructors { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }

    }
}