using Examination.Core.Requests;
using Examination.Persistence;
using Examination.Services.Exceptions;

namespace Examination.Services;

public sealed class UserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public object Register(CreateUserRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) ||
            string.IsNullOrEmpty(request.Name))
            throw new ConflictException("Invalid data");

        var exists = _dbContext.Users
            .Any(u => u.Email == request.Email);

        if (exists)
            throw new ConflictException("User already exists");

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role ?? "Student"
        };

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return new
        {
            user.Id,
            user.Name,
            user.Email,
            user.Role
        };
    }

    public object Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            throw new ConflictException("Invalid credentials");

        var user = _dbContext.Users
            .FirstOrDefault(u => u.Email == email && u.Password == password);

        if (user == null)
            throw new ConflictException("Invalid email or password");

        return new
        {
            user.Id,
            user.Name,
            user.Email,
            user.Role
        };
    }
}