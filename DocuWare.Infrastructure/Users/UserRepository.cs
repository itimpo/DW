using DocuWare.Abstractions.User;
using Microsoft.EntityFrameworkCore;

namespace DocuWare.Infrastructure.Users;

internal class UserRepository : IUserRepository
{
    private DwDbContext _db;
    public UserRepository(DwDbContext db)
    {
        _db = db;
    }

    public Task<UserDto?> GetUser(string email, string password)
    {
        return _db.Users
            .Where(q => q.Email == email && q.PasswordHash == password) //password hash :)
            .Select(q => new UserDto
            {
                Id = q.Id,
                Name = q.Name,
                Email = email
            })
            .FirstOrDefaultAsync();
    }
}
