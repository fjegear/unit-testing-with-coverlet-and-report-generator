using System.Collections.Generic;

namespace WebShop
{
    public interface IShoppingCartService
    {
        void AddToCart(int userId, Product product);
        IEnumerable<Product> GetProductsByUserId(int userId);
    }
}