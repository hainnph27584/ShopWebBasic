using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class RoleServices : IRoleServices
    {
        ShopDbContext context;
        public RoleServices()
        {
            context = new ShopDbContext();
        }
        public bool CreateRole(Role role)
        {
            try
            {
                context.Role.Add(role);
                context.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteRole(Guid id)
        {
            try
            {
                dynamic role = context.Role.Find(id);
                context.Role.Remove(role);
                context.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                  return true;
            }
        }

        public List<Role> GetAllRoles()
        {
           return context.Role.ToList();
        }

        public Role GetRoleById(Guid id)
        {
          return context.Role.FirstOrDefault(x=>x.RoleID == id);
        }

        public List<Role> GetRoleByName(string name)
        {
            return context.Role.Where(x=>x.RolenName.Contains(name)).ToList();
        }

        public bool UpdateRole(Role r)
        {
            try
            {
                var role = context.Role.Find(r.RoleID);
                role.Description = r.Description;
                role.RolenName = r.RolenName;
                role.Status = r.Status;
                context.Role.Update(role);
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
