using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class BillDetailsServices : IBillDetailsServices
    {
        ShopDbContext context;
        public BillDetailsServices()
        {
            context = new ShopDbContext();
        }
        public bool CreateBillDetails(BillDetails billDetails)
        {
            try
            {
                context.BillDetails.Add(billDetails);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteBillDetails(Guid id)
        {
            try
            {
                dynamic billDetails = context.BillDetails.Find(id);
                context.BillDetails.Remove(billDetails);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<BillDetails> GetAllBillDetails()
        {
            return context.BillDetails.ToList();
        }

        public BillDetails GetBillDetailsById(Guid id)
        {
            return context.BillDetails.FirstOrDefault(x => x.ID == id);
        }

        public List<BillDetails> GetBillDetailsByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBillDetails(BillDetails bd)
        {
            try
            {
                var billDetails = context.BillDetails.Find(bd.ID);
                billDetails.IdSP = bd.IdSP;
                billDetails.Quantity = bd.Quantity;
                billDetails.Price = bd.Price;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
