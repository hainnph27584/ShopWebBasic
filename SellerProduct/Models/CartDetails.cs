﻿namespace SellerProduct.Models
{
    public class CartDetails
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid IdSp { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
