using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel; // Added for DisplayName

namespace PERFILES_SA.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Nombres")] // Translated
        public string Names { get; set; } = string.Empty;

        [Required]
        [StringLength(13)]
        [DisplayName("DPI")] // Translated
        public string DPI { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Fecha de Nacimiento")] // Translated
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(1)]
        [DisplayName("Género")] // Translated
        public string Gender { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Fecha de Contratación")] // Translated
        public DateTime HireDate { get; set; }

        [Required]
        [DisplayName("Departamento")] // Translated
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }

        [Required]
        [DisplayName("Activo")] // Translated
        public bool IsActive { get; set; } = true;
    }
}