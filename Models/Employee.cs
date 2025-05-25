using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Employee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es requerido")]
    [Display(Name = "Nombres")]
    public string Names { get; set; } = string.Empty;

    [Required(ErrorMessage = "El DPI es requerido")]
    [StringLength(13, MinimumLength = 13, ErrorMessage = "El DPI debe tener 13 dígitos")]
    public string DPI { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
    [Display(Name = "Fecha de Nacimiento")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "El género es requerido")]
    [Display(Name = "Género")]
    public string Gender { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de ingreso es requerida")]
    [Display(Name = "Fecha de Ingreso")]
    [DataType(DataType.Date)]
    public DateTime HireDate { get; set; }

    [Display(Name = "Dirección")]
    public string? Address { get; set; }

    [Display(Name = "NIT")]
    public string? NIT { get; set; }

    [Required(ErrorMessage = "El departamento es requerido")]
    [Display(Name = "Departamento")]
    public int DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public Department? Department { get; set; }

    [NotMapped]
    [Display(Name = "Edad")]
    public int Age => CalculateAge(DateOfBirth);

    [NotMapped]
    [Display(Name = "Tiempo Laborando")]
    public string TimeWorking => CalculateTimeWorking(HireDate);

    private int CalculateAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }

    private string CalculateTimeWorking(DateTime hireDate)
    {
        var timeSpan = DateTime.Today - hireDate;
        var years = timeSpan.Days / 365;
        var months = (timeSpan.Days % 365) / 30;
        return $"{years} años, {months} meses";
    }
}