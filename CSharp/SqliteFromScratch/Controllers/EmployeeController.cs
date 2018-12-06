using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SqliteFromScratch.Models;

namespace SqliteFromScratch.Controllers {
    // MVC is handling the routing for you.
    [Route ("api/[Controller]")]
    public class EmployeeController : Controller {
        [HttpGet]
        public List<Employee> GetEmployees () {
            List<Employee> employees = new List<Employee> ();
            string dataSource = "Data Source =" + Path.GetFullPath ("chinook.db");
            using (SqliteConnection conn = new SqliteConnection (dataSource)) {
                conn.Open ();
                string sql = $"select * from employees where HireDate > \"2003-01-01 00:00:00\"";
                using (SqliteCommand command = new SqliteCommand (sql, conn)) {
                    using (SqliteDataReader reader = command.ExecuteReader ()) {
                    while (reader.Read ()) {
                    Employee newEmployee = new Employee () {
                    EmployeeId = reader.GetInt32 (0),
                    LastName = reader.GetString (1),
                    FirstName = reader.GetString (2),
                    Title = reader.GetString (3),
                    BirthDate = reader.GetString (5),
                    HireDate = reader.GetString (6),
                    Address = reader.GetString (7),
                    City = reader.GetString (8),
                    State = reader.GetString (9),
                    Country = reader.GetString (10),
                    PostalCode = reader.GetString (11),
                    Phone = reader.GetString (12),
                    Fax = reader.GetString (13),
                    Email = reader.GetString (14)
                            };
                            employees.Add (newEmployee);
                        }
                    }
                }
                conn.Close ();
            }

            return employees;
        }
    }
}