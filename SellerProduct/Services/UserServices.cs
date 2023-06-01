using SellerProduct.IServices;
using SellerProduct.Models;
using System.Xml.Linq;

namespace SellerProduct.Services
{
    public class UserServices : IUserServices
    {
        ShopDbContext context;
        public UserServices()
        {
            context = new ShopDbContext();
        }
        public bool CreateUser(User user)
        {
            try
            {
                context.User.Add(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteUser(Guid id)
        {
            try
            {
                dynamic user = context.User.Find(id);
                context.User.Remove(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<User> GetAllUsers()
        {
            return context.User.ToList();
        }

        public User GetUserById(Guid id)
        {
            return context.User.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetUserByname(string name)
        {
            return context.User.Where(x => x.UserName.Contains(name)).ToList();
        }

        public int LogIn(string username, string password)
        {

            var tk = context.User.FirstOrDefault(x => x.UserName == username);
            if (tk == null)
            {
                return -2; // tk k tồn tại

            }
            else
            {
                if (tk.Stauts == 0)
                {
                    return 0; /// vô hhoa
                }
                else
                {
                    if (tk.Password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2; // sai mk
                    }
                }
            }
        }

        public bool UpdateUser(User u)
        {
            try
            {
                var user = context.User.Find(u.Id);
                user.UserName = u.UserName;
                user.Password = u.Password;
                user.RoleID = u.RoleID;
                user.Stauts = u.Stauts;
                context.User.Update(user);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
