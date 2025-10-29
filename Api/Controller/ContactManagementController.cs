using Api.Model;
using Api.ModelDTO;
using Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

public class ContactManagementController : BaseController
{
    private readonly IPaginationStorage _storage;
    
    public ContactManagementController(IPaginationStorage storage) => _storage = storage;

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
    public ActionResult<Contact> GetContactById(int id)
    {
        Contact? contact = _storage.GetContactById(id);
        if (contact is null)
        {
            return NotFound("Contact not found");
        }
        return Ok(contact);
    }

    [HttpGet("contacts/page")]
    public IActionResult GetContacts(int pageNumber = 1, int pageSize = 5)
    {
        (List<Contact> contacts, int total) = _storage.GetContacts(pageNumber, pageSize);
        var response = new { Contacts = contacts, TotalCount = total, CurrentPage = pageSize, PageSize = pageSize };
        return Ok(response);
    }
    
}