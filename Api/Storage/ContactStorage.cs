using Api.Model;
using Api.ModelDTO;
using Bogus;

namespace Api.Storage;

public class ContactStorage
{
    private List<Contact> Contacts { get; set; }
    
    public ContactStorage()
    {
        Contacts = new List<Contact>();
        Faker faker = new Faker("ru");
        
        for (int i = 0; i < 10; i++)
        {
            Contacts.Add(new Contact()
            {
                Id = i,
                Name = faker.Name.FullName(),
                PhoneNumber = faker.Phone.PhoneNumber("7-###-###-####"),
                Email = faker.Internet.Email(),
            });
        }
    }

    public List<Contact> GetContact()
    {
        return Contacts;
    }
    
    public bool Add(Contact contact)
    {
        if (!FindContactId(contact.Id, out int contactId))
        {
            Contacts.Add(contact);
            return true;
        }
        return false;
    }

    public bool Remove(int id)
    {
        bool isFind = FindContactId(id, out int contactId);
        if (isFind)
        {
            Contacts.RemoveAt(contactId);
            return true;
        }
        return false;
    }

    public bool UpdateContact(int id, ContactDto contactDto)
    {
        bool ifFind = FindContactId(id, out int contactId);
        if (ifFind)
        {
            Contact contact = Contacts[contactId];
            contact.Name = contactDto.Name;
            contact.Email = contactDto.Email;
            return true;
        }
        return false;
    }
    
    public bool FindContactId(int id, out int contactId)
    {
        for (int i = 0; i < Contacts.Count; i++)
        {
            if (Contacts[i].Id == id)
            {
                contactId = i;
                return true;
            }
        }
        contactId = -1;
        return false;
    }
}