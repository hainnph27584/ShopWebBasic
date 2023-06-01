using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface IRoleServices
    {
        public bool CreateRole(Role role);
        public bool DeleteRole(Guid id);
        public bool UpdateRole(Role role);
        public List<Role> GetAllRoles();
        public Role GetRoleById(Guid id);
        public List<Role> GetRoleByName(string name);
    }
}
