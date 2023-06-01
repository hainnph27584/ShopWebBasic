using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface ICartDetailsServices
    {
        public bool CreateCartDetails(CartDetails cartDetails);
        public bool UpdateCartDetails(CartDetails obj, Guid idUser, Guid idProduct);
        public bool DeleteCartDetails(Guid idUser, Guid idProduct);
        public List<CartDetails> GetAllCartDetails();
        public List<CartDetails> GetCartDetailsByName(string name);
        public CartDetails GetCartDetailsById(Guid id);
    }
}
