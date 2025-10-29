using Api.DataContext;
using Api.Model;

namespace Api.Storage;

public class SqlitePaginationEfStorage : SqliteEfStorage, IPaginationStorage
{
    public SqlitePaginationEfStorage(SqliteDbContext context) 
        : base(context)
    {
        
    }

    public Contact? GetContactById(int id)
    {
        return _context.Contacts.Find(id);
    }

    public (List<Contact>, int TotalCount) GetContacts(int pageNumber, int pageSize)
    {
        int total = _context.Contacts.Count();

        List<Contact> contacts = _context.Contacts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        return (contacts, total);
    }
}