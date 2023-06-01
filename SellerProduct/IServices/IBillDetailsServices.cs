using SellerProduct.Models;

namespace SellerProduct.IServices
{
    public interface IBillDetailsServices
    {
        public bool CreateBillDetails(BillDetails billDetails);
        public bool UpdateBillDetails(BillDetails billDetails);
        public bool DeleteBillDetails(Guid id);
        public List<BillDetails> GetAllBillDetails();
        public List<BillDetails> GetBillDetailsByName(string name);
        public BillDetails GetBillDetailsById(Guid id);

    }
}
