using Api.Model;
using Api.ModelDTO;
using Microsoft.Data.Sqlite;

namespace Api.Storage;

public class SqliteStorage : IStorage
{
    private readonly string _connectionString;

    public SqliteStorage(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public List<Contact> GetContacts()
    {
        List<Contact> contacts = new List<Contact>();
        
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        SqliteCommand command = connection.CreateCommand();
        
        command.CommandText = "SELECT * FROM contacts";

        // ExecuteScalar -- для операций с агрегатными функциями (когда получается 1 столбец и 1 строка)
        // ExecuteReader -- когда получаем таблицу
        // ExecuteNonQuery -- для создания таблицы
        
        using SqliteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            contacts.Add(new Contact()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                PhoneNumber = reader.GetString(2),
                Email = reader.GetString(3)
            });
        }
        
        
        
        
        return contacts;
    }

    public Contact? Add(Contact contact)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        SqliteCommand command = connection.CreateCommand();

        // Старая версия
        // string insertCommand = $"INSERT INTO contacts(name, phone, email) " +
        //                        $"VALUES (\'{contact.Name}\',  \'{contact.PhoneNumber}\', \'{contact.Email}\')";

        string insertCommand = @"INSERT INTO contacts(name, phone, email) VALUES (@name, @phone, @email);
                                 SELECT last_insert_rowid();
                                ";
        command.CommandText = insertCommand;
        command.Parameters.AddWithValue("@name", contact.Name);
        command.Parameters.AddWithValue("@phone", contact.PhoneNumber);
        command.Parameters.AddWithValue("@email", contact.Email);
        
        contact.Id = Convert.ToInt32(command.ExecuteScalar());
        return contact;
        
    }

    public bool Remove(int id)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        
        SqliteCommand command = connection.CreateCommand();
        
        string deleteCommand = "DELETE FROM contacts WHERE id = @id";
        command.CommandText = deleteCommand;
        command.Parameters.AddWithValue("@id", id);

        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateContact(int id, ContactDto contactDto)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        connection.Open();
        
        SqliteCommand command = connection.CreateCommand();

        string updateCommand = "UPDATE contacts SET name = @name, email = @email WHERE id = @id";
        command.CommandText = updateCommand;
        command.Parameters.AddWithValue("@name", contactDto.Name);
        command.Parameters.AddWithValue("@email", contactDto.Email);
        command.Parameters.AddWithValue("@id", id);
        
        return command.ExecuteNonQuery() > 0;
    }

    public bool FindContactId(int id, out int contactId)
    {
        throw new NotImplementedException();
    }
}