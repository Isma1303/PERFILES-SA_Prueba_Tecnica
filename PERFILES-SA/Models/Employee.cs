using System;
using System.ComponentModel.DataAnnotations;

namespace PERFILES_SA.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Names { get; set; } = string.Empty;

        [Required]
        [StringLength(13)]
        public string DPI { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(1)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}