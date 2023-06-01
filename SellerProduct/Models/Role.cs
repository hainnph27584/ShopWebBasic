namespace SellerProduct.Models
{
    public class Role
    {
        public Guid RoleID { get; set; }
        public string RolenName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public virtual List<User> Users { get; set; }
       
    }
}
