using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class CartServices : ICartServices
    {
        ShopDbContext context;
        public CartServices()
        {
            context = new ShopDbContext();
        }
        public bool CreateCart(Cart cart)
        {
            try
            {
                context.Cart.Add(cart);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCart(Guid id)
        {
            try
            {
                dynamic cart = context.Cart.Find(id);
                context.Cart.Remove(cart);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Cart> GetAllCarts()
        {
           return context.Cart.ToList();
        }

        public Cart GetCartById(Guid id)
        {
            
            return context.Cart.FirstOrDefault(x=>x.UserId == id);
        }

        public List<Cart> GetCartsByDescription(string x)
        {
            return context.Cart.Where(p=>p.Description.Contains(x)).ToList();
        }

        public bool UpdateCart(Cart c)
        {
            try
            {
                var cart = context.Cart.Find(c.UserId);
                cart.Description = c.Description;
                context.Cart.Update(cart);
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
