using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AKwebapplication.Models
{
    public class Home
    {
        [Key]
        public int Id { get; set; }
      


        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Join date is required.")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FeePaidDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Occupation is required.")]
        [StringLength(50, ErrorMessage = "Occupation cannot be longer than 50 characters.")]
        public string Occupation { get; set; }

        [Required(ErrorMessage = "Gender is required.")]//radiobutton
        public string Gender { get; set; } 

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Sharing preference is required.")]
        public string Sharing { get; set; }//dropdownlist

        public bool IsActive { get; set; }
        
        [Required]
        public string FileName { get; set; }
    }

}
