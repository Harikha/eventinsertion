using System.Data.SqlClient;

namespace eventinsertion
{
    public class TelemetryWriter
    {
        public void InsertIntoDatabase(TelemetryEvent telemetryEvent)
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-20AOL490\\SQLEXPRESS;Initial Catalog=Employee;Integrated Security=True";
                string insertQuery = "INSERT INTO [Employee].[dbo].[tblEmployee] (Name, Salary, Department) VALUES (@Name, @Salary, @Department)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Use parameters to avoid SQL injection
                        command.Parameters.AddWithValue("@Name", telemetryEvent.Name);
                        command.Parameters.AddWithValue("@Salary", telemetryEvent.Salary);
                        command.Parameters.AddWithValue("@Department", telemetryEvent.Department);

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
                Console.WriteLine(ans); ;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
