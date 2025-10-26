using Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.DataContext;

public class SqliteDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }

    public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
    {
        
    }
}