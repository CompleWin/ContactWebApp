using Api.DataContext;
using Api.Model;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Api.Seed;

public class SqliteEfFakerInitializer : IInitializer
{
    private readonly SqliteDbContext _context;

    public SqliteEfFakerInitializer(SqliteDbContext context)
    {
        _context = context;
    }
    
    public void Initialize(int numOfContacts)
    {
        // Данная команда делает dotnet ef database update
        _context.Database.Migrate();

        if (!_context.Contacts.Any())
        {
            Faker<Contact> faker = new Faker<Contact>("ru")
                .RuleFor(c => c.Name, f => f.Name.FirstName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("7-###-###-####"));
            
            List<Contact> fakeContacts = faker.Generate(numOfContacts);
            _context.Contacts.AddRange(fakeContacts);
            _context.SaveChanges();
        }
    }
}