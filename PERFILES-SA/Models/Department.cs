using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PERFILES_SA.Models
{
    public class Department
    {
        public Department()
        {
            Employees = new List<Employee>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Nombre")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DisplayName("Activo")]
        public bool IsActive { get; set; } = true;

        public ICollection<Employee> Employees { get; set; }
    }
}