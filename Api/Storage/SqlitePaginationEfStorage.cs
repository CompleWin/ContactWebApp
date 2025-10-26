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
}