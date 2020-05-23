using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop
{
    public interface IUserService
    {
        void LogInUser(User user);
        bool IsUserLoggedIn(int userId);
        Address GetShippingAddress(User user);
    }
}
