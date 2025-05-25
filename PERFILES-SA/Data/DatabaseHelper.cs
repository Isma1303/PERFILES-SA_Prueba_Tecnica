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

        #region Employee Operations
        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Employees", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Names = reader.GetString(reader.GetOrdinal("Names")),
                                DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId"))
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Employees (Names, DepartmentId) VALUES (@Names, @DepartmentId)", connection))
                {
                    command.Parameters.AddWithValue("@Names", employee.Names);
                    command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Employees SET Names = @Names, DepartmentId = @DepartmentId WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Names", employee.Names);
                    command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmployee(int id)
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
        #endregion

        #region Department Operations
        public List<Department> GetAllDepartments()
        {
            var departments = new List<Department>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Departments", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            departments.Add(new Department
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            });
                        }
                    }
                }
            }
            return departments;
        }

        public void AddDepartment(Department department)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Departments (Name) VALUES (@Name)", connection))
                {
                    command.Parameters.AddWithValue("@Name", department.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateDepartment(Department department)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Departments SET Name = @Name WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", department.Id);
                    command.Parameters.AddWithValue("@Name", department.Name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDepartment(int id)
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
        #endregion
    }
}