using Api.Model;
using Api.ModelDTO;
using Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

public class ContactManagementController : BaseController
{
    private readonly IStorage _storage;
    
    public ContactManagementController(IStorage storage) => _storage = storage;

    [HttpPost("contacts")]
    //                       Брать информацию из тела запроса, а не из строки
    public IActionResult CreateContact([FromBody] Contact contact)
    {
        Contact? newContact = _storage.Add(contact);
        if (newContact == null)
        { 
            return Conflict("Contact already exists");
        }
        return Ok(newContact);
    }


    [HttpGet("contacts")]
    public ActionResult<List<Contact>> GetContacts()
    {
        return Ok(_storage.GetContacts());
    }

    [HttpDelete("contacts/{id}")]
    public IActionResult DeleteContact(int id)
    {
        bool res = _storage.Remove(id);
        if (res)
        {
            return NoContent();
        }
        return BadRequest("Id not found");
    }

    
    
    [HttpPut("contacts/{id}")]
    public IActionResult UpdateContact(int id, [FromBody] ContactDto contactDto)
    {
        bool res = _storage.UpdateContact(id, contactDto);
        if (res)
        {
            return Ok();
        }
        return Conflict("Id not found");
        
    }

    [HttpGet("contacts/{id}")]
    public ActionResult<Contact> GetContactId(int id)
    {
        Console.WriteLine(id);
        
        if (_storage.FindContactId(id, out int contactId))
        {
            return Ok(_storage.GetContacts()[contactId]);
        }
        return NotFound("Contact not found");
    }
}