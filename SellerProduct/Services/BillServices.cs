using SellerProduct.IServices;
using SellerProduct.Models;

namespace SellerProduct.Services
{
    public class BillServices : IBillServices
    {
        ShopDbContext context;
        public BillServices()
        {
            context = new ShopDbContext();
        }
        public bool CreateBill(Bill bill)
        {
            try
            {
                context.Bill.Add(bill);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteBill(Bill id)
        {
            try
            {
               dynamic bill = context.Bill.Find(id);
                context.Bill.Remove(bill);
                context.SaveChanges();
                return true;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public List<Bill> GetAllBills()
        {
            return context.Bill.ToList();
        }

        public List<Bill> GetBillByStatus(int status)
        {
            return context.Bill.Where(b => b.Status == status).ToList();
        }

        public Bill GetBillById(Guid id)
        {
            return context.Bill.FirstOrDefault(x=>x.Id == id);
        }

        public bool UpdateBill(Bill b)
        {
            try
            {
                var bill = context.Bill.Find(b.Id);
                bill.CreateDate = b.CreateDate;
                bill.Status = b.Status;
                context.Bill.Update(bill);
                context.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
