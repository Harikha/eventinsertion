using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;

namespace eventinsertion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly ILogger<EventController> _logger;
        private readonly TelemetryWriter telemetryWriter;
        public EventController(ILogger<EventController> logger, TelemetryWriter writer)
        {
            _logger = logger;

            telemetryWriter = writer;

        }

        public void Post(TelemetryEvent telemetryEvent)
        {
            telemetryWriter.InsertIntoDatabase(telemetryEvent);
        }
        [HttpGet]
     public void Index()
        {
            Console.Write("gfdfgdbd");
        }

        // Example method for inserting into the database
      
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
