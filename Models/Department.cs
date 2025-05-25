using System.ComponentModel.DataAnnotations;

public class Department
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del departamento es requerido")]
    [Display(Name = "Nombre del Departamento")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}