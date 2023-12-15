/*using System;
using System.Net.Http;
using System.Text;
public class HomeController
{
    public void Index()
    {
            using (HttpClient client = new HttpClient())
            {
                // Example values to be inserted into the table
                string insertionValues = "'jin',10202,2";

                StringContent content = new StringContent(insertionValues, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(url, content).Result;
            

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Data sent successfully.");
                }
                else
                {
                    Console.WriteLine($"Error sending data. Status code: {response.StatusCode}");
                   response.EnsureSuccessStatusCode();

                 }
        }
        }
    }
*/
