using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface ICartServices
    {
        public bool CreateCart(Cart cart);
        public bool UpdateCart(Cart cart);
        public  bool DeleteCart(Guid id);
        public List<Cart> GetAllCarts();
        public Cart GetCartById(Guid id);
        public List<Cart> GetCartsByDescription(string x);

    }
}
