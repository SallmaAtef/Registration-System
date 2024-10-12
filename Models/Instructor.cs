using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [DisplayName("Instructor")]

        [Required(ErrorMessage = "You Have to provide a valid fullname.")]
        [MinLength(2, ErrorMessage = "Full name musn't be less than 2 charcters.")]
        [MaxLength(20, ErrorMessage = "Full name musn't excee 20 charcters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "You Have to provide a valid course name.")]
        [MinLength(2, ErrorMessage = "Course name musn't be less than 2 charcters.")]
        [MaxLength(20, ErrorMessage = "Course name musn't excee 20 charcters")]
        public string CourseName { get; set; }

        [DisplayName("BirthDate")]
        public DateTime BirthDate { get; set; }


        [DisplayName("JoinDate")]
        public DateTime JoinDate { get; set; }

        [DisplayName("IsActive")]
        public bool IsActive { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Select a valid Course Name")]
        [DisplayName("Faculty")]
        public int StudentId { get; set; }

        [ValidateNever]
        public Student student { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string ImagePath { get; set; }


    }
}
