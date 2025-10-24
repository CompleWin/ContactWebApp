using Api.Model;
using Api.ModelDTO;

namespace Api.Storage;

public interface IStorage
{
    List<Contact> GetContacts();
    Contact? Add(Contact contact);
    bool Remove(int id);
    bool UpdateContact(int id, ContactDto contactDto);
    public bool FindContactId(int id, out int contactId);
}