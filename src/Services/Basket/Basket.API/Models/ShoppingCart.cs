namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice
    {
        get
        {
            return Items.Sum(x => x.Price * x.Quantity);
        }
    }

    //mapping
    public ShoppingCart()
    {
    }  
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
}
