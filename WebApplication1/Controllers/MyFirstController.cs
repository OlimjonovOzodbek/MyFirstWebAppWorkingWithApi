using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebApplication1.NewFolder;

namespace WebApplication1.Controllers
{
    [Route("api/[controller],[action]")]
    [ApiController]
    public class MyFirstController : ControllerBase
    {
        private readonly string connectionString = "Server=127.0.0.1;Port=5432;Database=MyData;User Id=postgres;" +
            "Password=admin;";
        public string TableName = "Data1";
        [HttpGet]
        public List<MyModel> Get()
        {
            List<MyModel> myModels = new List<MyModel>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"select * from {TableName};";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    myModels.Add(new MyModel
                    {
                        Id = (int)reader["id"],
                        Name = (string)reader["name"]
                    });
                }
                reader.Close();
                return myModels;
            }
        }
        [HttpPost]
        public string Post(string username,string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = $"insert into Data1(name, password) values('{username}','{password}');";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            return "Qoshildi";
        }
    }
}