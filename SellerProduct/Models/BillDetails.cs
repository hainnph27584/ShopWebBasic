using System.ComponentModel.DataAnnotations.Schema;

namespace SellerProduct.Models
{
    public class BillDetails
    {
        public Guid ID { get; set; }
        public Guid? IdSP { get; set; }
        public Guid? IdHD { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Bill Bill { get; set; }

       

    }
}
