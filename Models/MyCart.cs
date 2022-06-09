namespace MyEshop.Models
{
    [Serializable]
    public class MyCart
    {
        public Products Products { get; set; }
        public int Quantity { get; set; }

        public MyCart(Products products, int quantity)
        {
            Products = products;
            Quantity = quantity;
        }

    }
}
