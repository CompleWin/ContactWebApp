using Api.Model;
using Api.ModelDTO;
using Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

public class ContactManagementController : BaseController
{
    private readonly ContactStorage _storage;
    
    public ContactManagementController(ContactStorage storage) => _storage = storage;

    [HttpPost("contacts")]
    //                       Брать информацию из тела запроса, а не из строки
    public IActionResult CreateContact([FromBody] Contact contact)
    {
        bool isAdd = _storage.Add(contact);
        if (!isAdd)
        { 
            return Conflict("Contact already exists");
        }
        return Created();
    }


    [HttpGet("contacts")]
    public ActionResult<List<Contact>> GetContacts()
    {
        return Ok(_storage.GetContact());
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
            return Ok(_storage.GetContact()[contactId]);
        }
        return NotFound("Contact not found");
    }
}