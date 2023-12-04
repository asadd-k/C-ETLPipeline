using CsvHelper;
using ETLPipeline.Data;
using System.Globalization;
using System.Net;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Dapper;

namespace ETLPipeline
{
    public static class Extract
    {
        public async static Task<IEnumerable<CarReviews>> ExtractingFromCSV(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<CarReviews>().ToList();
                    return records;
                }
            } 
        }
        public async static Task<IEnumerable<Users>> HittingApi(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var request = await httpClient.GetAsync(url);
                if (request.StatusCode == HttpStatusCode.OK)
                {
                    var result = await request.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<Users>>(result);
                    return users;
                }
                else
                {
                    return new List<Users>();
                }
            }
        }
        public async static Task<IEnumerable<Cars>> ReadFromDatabase(string connectionString)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CARS";
                var result = await sqlConnection.QueryAsync<Cars>(query);
                return result;
            }
        }
    }
}
