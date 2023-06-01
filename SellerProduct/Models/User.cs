namespace SellerProduct.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid RoleID { get; set; }
        public int? Stauts { get; set; }
        public virtual Role Role { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ICollection<Bill> Bill { get; set; }
    }
}
