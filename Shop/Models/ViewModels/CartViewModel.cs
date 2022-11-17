namespace Shop.Models.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public decimal GrantTotal { get; set; }
    }
}
