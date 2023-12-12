using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
namespace eventinsertion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost("insertData")]
        public IActionResult InsertData([FromBody] string insertionValues)
        {
            // Process the insertionValues
            // Example: Call a method to insert into the database
            // InsertIntoDatabase(insertionValues);

            // Send a response (optional)
            return Ok("Data received successfully.");
        }

        // Example method for inserting into the database
        private void InsertIntoDatabase(string values)
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-20AOL490\\SQLEXPRESS;Initial Catalog=Employee;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Random random = new Random();
                    // Any random integer   
                    int num = random.Next();
                    string insertQuery = "INSERT INTO [Employee].[dbo].[tblEmployee] (Name,Salary, Department) VALUES ('bob',10000,1)";
                    // Replace with your table name and column names

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {


                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} row(s) inserted.");
                    }
                }
                string ans = "";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM [Employee].[dbo].[tblEmployee]"; // Replace with your table name

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Access data using reader["ColumnName"]
                                ans += $"ID: {reader["id"]}, Name: {reader["Name"]}\n";
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

   
    

        

        /*public void Index()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-20AOL490\\SQLEXPRESS;Initial Catalog=Employee;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Random random = new Random();
                    // Any random integer   
                    int num = random.Next();
                    string insertQuery = "INSERT INTO [Employee].[dbo].[tblEmployee] (Name,Salary, Department) VALUES ('bob',10000,1)";
                    // Replace with your table name and column names

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {


                        int rowsAffected = command.ExecuteNonQuery();

                        Console.WriteLine($"{rowsAffected} row(s) inserted.");
                    }
                }
                string ans = "";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM [Employee].[dbo].[tblEmployee]"; // Replace with your table name

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Access data using reader["ColumnName"]
                                ans+=$"ID: {reader["id"]}, Name: {reader["Name"]}\n";
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }*/


        
    }
