using Api.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

public class StorageController : BaseController
{
    private readonly DataContextOld _dataContext;
    
    public StorageController(DataContextOld dataContext)
    {
        _dataContext = dataContext;
    }
    
    [HttpGet("setstring/{value}")]
    public void SetString(string value) => _dataContext.Str  = value;
    
    [HttpGet("getstring")]
    public string GetString() => _dataContext.Str;
}