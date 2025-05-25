using Microsoft.Data.SqlClient;
using PERFILES_SA.Models;
using System.Data;

namespace PERFILES_SA.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = "Server=uspg.database.windows.net;Database=practice_db;User Id=upsgadmin;Password=1234abcd_;TrustServerCertificate=True";
        }

        public void SetupDatabase()
        {
            try
            {
                Console.WriteLine("Configurando base de datos...");

                var sqlCommands = new List<string>
                {
                    // "IF OBJECT_ID('Employees', 'U') IS NOT NULL DROP TABLE Employees;",
                    // "IF OBJECT_ID('Departments', 'U') IS NOT NULL DROP TABLE Departments;",

                    // @"IF NO EXIST CREATE TABLE Departments (
                    //     Id INT IDENTITY(1,1) PRIMARY KEY,
                    //     Name NVARCHAR(50) NOT NULL,
                    //     IsActive BIT NOT NULL DEFAULT 1
                    // );",

                    // @"IF NO EXISTCREATE TABLE Employees (
                    //     Id INT IDENTITY(1,1) PRIMARY KEY,
                    //     Names NVARCHAR(100) NOT NULL,
                    //     DPI NVARCHAR(13) NOT NULL,
                    //     DateOfBirth DATE NOT NULL,
                    //     Gender NVARCHAR(1) NOT NULL,
                    //     HireDate DATE NOT NULL,
                    //     DepartmentId INT NOT NULL,
                    //     IsActive BIT NOT NULL DEFAULT 1,
                    //     CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
                    // );",

                };

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database connection opened");

                    foreach (var sql in sqlCommands)
                    {
                        try
                        {
                            using (var command = new SqlCommand(sql, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                            Console.WriteLine("Comando SQL ejecutado exitosamente"); // Translated
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al ejecutar comando SQL: {ex.Message}"); // Translated
                            Console.WriteLine($"Comando: {sql}");
                        }
                    }
                }

                Console.WriteLine("Configuración de base de datos completada"); // Translated
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al configurar base de datos: {ex.Message}"); // Translated
            }
        }

        #region Employee Operations
        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT e.*, d.Name AS DepartmentName FROM Employees e JOIN Departments d ON e.DepartmentId = d.Id", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var employee = new Employee
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Names = reader.GetString(reader.GetOrdinal("Names")),
                                    DPI = reader.GetString(reader.GetOrdinal("DPI")),
                                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                    Gender = reader.GetString(reader.GetOrdinal("Gender")),
                                    HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate")),
                                    DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    Department = new Department { Name = reader.GetString(reader.GetOrdinal("DepartmentName")) }
                                };


                                if (HasColumn(reader, "DPI"))
                                    employee.DPI = reader.GetString(reader.GetOrdinal("DPI"));

                                if (HasColumn(reader, "DateOfBirth"))
                                    employee.DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));

                                if (HasColumn(reader, "Gender"))
                                    employee.Gender = reader.GetString(reader.GetOrdinal("Gender"));

                                if (HasColumn(reader, "HireDate"))
                                    employee.HireDate = reader.GetDateTime(reader.GetOrdinal("HireDate"));

                                if (HasColumn(reader, "IsActive"))
                                    employee.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));

                                employees.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener empleados: {ex.Message}"); // Translated

            }
            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"INSERT INTO Employees 
                        (Names, DPI, DateOfBirth, Gender, HireDate, DepartmentId, IsActive) 
                        VALUES 
                        (@Names, @DPI, @DateOfBirth, @Gender, @HireDate, @DepartmentId, @IsActive)", connection))
                    {
                        command.Parameters.AddWithValue("@Names", employee.Names);
                        command.Parameters.AddWithValue("@DPI", employee.DPI);
                        command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", employee.Gender);
                        command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                        command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                        command.Parameters.AddWithValue("@IsActive", employee.IsActive);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar empleado: {ex.Message}"); // Translated

            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(@"UPDATE Employees SET 
                        Names = @Names, 
                        DPI = @DPI,
                        DateOfBirth = @DateOfBirth,
                        Gender = @Gender,
                        HireDate = @HireDate,
                        DepartmentId = @DepartmentId,
                        IsActive = @IsActive 
                        WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", employee.Id);
                        command.Parameters.AddWithValue("@Names", employee.Names);
                        command.Parameters.AddWithValue("@DPI", employee.DPI);
                        command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", employee.Gender);
                        command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                        command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                        command.Parameters.AddWithValue("@IsActive", employee.IsActive);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar empleado: {ex.Message}"); // Translated
            }
        }

        public void DeleteEmployee(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Employees WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
            }
        }
        #endregion

        #region Department Operations
        public List<Department> GetAllDepartments()
        {
            var departments = new List<Department>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT * FROM Departments", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var department = new Department
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                };

                                if (HasColumn(reader, "IsActive"))
                                    department.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));

                                departments.Add(department);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener departamentos: {ex.Message}"); // Translated
            }
            return departments;
        }

        public void AddDepartment(Department department)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("INSERT INTO Departments (Name, IsActive) VALUES (@Name, @IsActive)", connection))
                    {
                        command.Parameters.AddWithValue("@Name", department.Name);
                        command.Parameters.AddWithValue("@IsActive", department.IsActive);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar departamento: {ex.Message}"); // Translated
            }
        }

        public void UpdateDepartment(Department department)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("UPDATE Departments SET Name = @Name, IsActive = @IsActive WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", department.Id);
                        command.Parameters.AddWithValue("@Name", department.Name);
                        command.Parameters.AddWithValue("@IsActive", department.IsActive);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar departamento: {ex.Message}"); // Translated
            }
        }

        public void DeleteDepartment(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("DELETE FROM Departments WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar departamento: {ex.Message}"); // Translated
            }
        }
        #endregion

        private bool HasColumn(SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
