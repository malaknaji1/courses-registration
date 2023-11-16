using courses_registration.Models;

namespace courses_registration.Interfaces
{
    public interface IUserRepository 
    {
        User GetUser(int id);
        bool UserExists(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        string GetPassword(string username);
        string HashPassword(string password);
        bool SoftDeleteUser(User user);
        bool IsPasswordCorrect(string password, string hashedPassword);
        bool Save();
    }
}
