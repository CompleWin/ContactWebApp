using Api.DataContext;
using Api.Model;
using Api.ModelDTO;

namespace Api.Storage;

public class SqliteEfStorage : IStorage
{
    protected readonly SqliteDbContext _context;

    public SqliteEfStorage(SqliteDbContext context)
    {
        _context = context;
    }


    public List<Contact> GetContacts()
    {
        return _context.Contacts.ToList();
    }

    public Contact? Add(Contact contact)
    {
        _context.Contacts.Add(contact);
        _context.SaveChanges();
        return contact;
    }

    public bool Remove(int id)
    {
        Contact contact = _context.Contacts.Find(id);
        if (contact is null)
        {
            return false;
        }
        _context.Contacts.Remove(contact);
        _context.SaveChanges();
        return true;
    }

    public bool UpdateContact(int id, ContactDto contactDto)
    {
        Contact contact = _context.Contacts.Find(id);
        if (contact is null)
        {
            return false;
        }
        contact.Name = contactDto.Name;
        contact.Email = contactDto.Email;
        _context.SaveChanges();
        return true;
    }

    public bool FindContactId(int id, out int contactId)
    {
        throw new NotImplementedException();
    }
}