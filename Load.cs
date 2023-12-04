using ETLPipeline.Data;
using MySql.Data.MySqlClient;

namespace ETLPipeline
{
    public class Load
    {
        public string ConnectionString { get; set; } = string.Empty;

        public List<TransformedData> TransformedData { get; set; }

        public MySqlConnection Connect()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public void InsertData()
        {
            try
            {
                var connection = Connect();
                string query = "INSERT INTO usercar(UserId, UserName, Email, CarId, CarName, Review) VALUES (@UserID, @UserName, @Email, @CarId, @CarName, @Review)";
                using MySqlCommand command = connection.CreateCommand();
                command.CommandText = query;

                foreach (var item in TransformedData)
                {
                    command.Parameters.AddWithValue("@UserId", item.UserId);
                    command.Parameters.AddWithValue("@UserName", item.UserName);
                    command.Parameters.AddWithValue("@Email", item.Email);
                    command.Parameters.AddWithValue("@CarId", item.CarId);
                    command.Parameters.AddWithValue("@CarName", item.CarName);
                    command.Parameters.AddWithValue("@Review", item.Review);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    
                }
                Console.WriteLine("Transformed Data Added in Database");
                connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
