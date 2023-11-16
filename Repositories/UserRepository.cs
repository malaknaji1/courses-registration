using courses_registration.Data;
using courses_registration.Interfaces;
using courses_registration.Models;
using Microsoft.EntityFrameworkCore;

namespace courses_registration.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository( AppDbContext context )
        {
            _context = context;
        }
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public string GetPassword(string username)
        {
            return _context.Users
                    .Where(u => u.Username == username && !u.IsDeleted)
                    .Select(u => u.PasswordHash)
                    .FirstOrDefault();
        }
        public string HashPassword(string password) 
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        
        public bool IsPasswordCorrect(string password,string hashedPassword) 
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.UserId == id && !u.IsDeleted).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SoftDeleteUser(User user)
        {
            user.IsDeleted = true;
            _context.Update(user);
            return Save();
        }


        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserId == id && !u.IsDeleted);
        }

       
    }
}
