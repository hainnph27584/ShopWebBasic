using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface IUserServices
    {
        public bool CreateUser(User user);
        public bool UpdateUser(User user);
        public bool DeleteUser(Guid id);
        public User GetUserById(Guid id);
        public int LogIn(string username, string password);
        public List<User> GetAllUsers();
        public List<User> GetUserByname(string email);
    }
}
