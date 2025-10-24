using Api.Model;
using Bogus;
using Microsoft.Data.Sqlite;

namespace Api.Seed;

public class FakerInitializer : IInitializer
{
    private readonly string _connectionString;

    public FakerInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Initialize(int numOfContacts = 20)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();

        SqliteCommand sqlCommand = connection.CreateCommand();

        string createTable =
            @" CREATE TABLE IF NOT EXISTS contacts (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                phone TEXT NOT NULL,
                email TEXT NOT NULL
                )";

        sqlCommand.CommandText = createTable;
        sqlCommand.ExecuteNonQuery();

        string countCommand = "SELECT COUNT(*) FROM contacts";
        sqlCommand.CommandText = countCommand;
        long count = (long)sqlCommand.ExecuteScalar();

        if (count == 0)
        {
            Faker<Contact> faker = new Faker<Contact>("ru")
                .RuleFor(p => p.Name, f => f.Name.FullName())
                .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber("7-###-###-####"))
                .RuleFor(p => p.Email, f => f.Person.Email);

            List<Contact> contacts = faker.Generate(numOfContacts);

            foreach (var contact in contacts)
            {
                string insertCommand = @"
            INSERT INTO contacts (name, phone, email) VALUES (@name, @phone, @email)
            ";
            
                sqlCommand.CommandText = insertCommand;
            
                sqlCommand.Parameters.Clear();
                
                sqlCommand.Parameters.AddWithValue("@name", contact.Name);
                sqlCommand.Parameters.AddWithValue("@phone", contact.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@email", contact.Email);
            
                sqlCommand.ExecuteNonQuery();
            }
        }
        
        
    }
}