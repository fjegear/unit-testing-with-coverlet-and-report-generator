using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebShop
{
    public class UserService : IUserService
    {
        private List<User> _loggedInUsers = new List<User>();

        public void LogInUser(User user)
        {
            _loggedInUsers.Add(user);
        }

        public bool IsUserLoggedIn(int userId)
        {
            return _loggedInUsers.Any(u => u.Id == userId);
        }

        public Address GetShippingAddress(User user)
        {
            if (!IsUserLoggedIn(user.Id))
                throw new UserNotLoggedInException("User is not logged in");

            return user.ShippingAddress;
        }
    }
}
