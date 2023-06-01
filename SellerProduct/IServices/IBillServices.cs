using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface IBillServices
    {
        public bool CreateBill(Bill bill);
        public bool DeleteBill(Bill id);
        public bool UpdateBill(Bill bill);
        public List<Bill> GetAllBills();
        public Bill GetBillById(Guid id);
        public List<Bill> GetBillByStatus(int status);
    }
}
