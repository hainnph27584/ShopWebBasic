using Microsoft.EntityFrameworkCore;
using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class CartDetailsServices : ICartDetailsServices
    {
        ShopDbContext context;
        public CartDetailsServices()
        {
            context = new ShopDbContext();
        }
        public bool CreateCartDetails(CartDetails cartDetails)
        {
            try
            {
                context.CartDetails.Add(cartDetails);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCartDetails(Guid idUser, Guid idProduct)
        {
            try
            {
                var list =  context.CartDetails.ToList();
                var obj = list.FirstOrDefault(c => c.UserId == idUser && c.IdSp == idProduct);
                

                context.CartDetails.Remove(obj);
                 //Task.FromResult<CartDetails>(context.CartDetails.Update(obj).Entity);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CartDetails> GetAllCartDetails()
        {
            return context.CartDetails.ToList();
        }

        public CartDetails GetCartDetailsById(Guid id)
        {
            return context.CartDetails.FirstOrDefault(x => x.Id == id);
        }

        public List<CartDetails> GetCartDetailsByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCartDetails(CartDetails obj,Guid idUser,Guid idProduct)
        {
            //try
            //{
            //    var cartDetails = context.CartDetails.Find(cd.Id);
            //    cartDetails.Quantity = cd.Quantity;

            //    context.CartDetails.Update(cartDetails);
            //    context.SaveChanges();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}

            try
            {
                var listObj =  context.CartDetails.ToList();
                var objForUpdate = listObj.FirstOrDefault(c => c.UserId == idUser && c.IdSp == idProduct);

                objForUpdate.Quantity = obj.Quantity;
                //objForUpdate.Status = obj.Status;

                context.CartDetails.Update(objForUpdate);
                 context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
