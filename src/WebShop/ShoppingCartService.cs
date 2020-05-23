using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserService _userService;
        private Dictionary<int, List<Product>> cart = new Dictionary<int, List<Product>>();

        public ShoppingCartService(IUserService userService)
        {
            _userService = userService;
        }

        public void AddToCart(int userId, Product product)
        {
            if (!_userService.IsUserLoggedIn(userId))
                throw new UserNotLoggedInException("User is not logged in");

            if (!cart.ContainsKey(userId))
            {
                cart[userId] = new List<Product> { product };
            }
            else
            {
                cart[userId].Add(product);
            }
        }

        public IEnumerable<Product> GetProductsByUserId(int userId)
        {
            if (!_userService.IsUserLoggedIn(userId))
                throw new UserNotLoggedInException("User is not logged in");

            return cart.ContainsKey(userId)
                ? cart[userId]
                : null;
        }
    }
}
