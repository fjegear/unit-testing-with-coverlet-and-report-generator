using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop;
using Xunit;

namespace WebShopTests
{
    public class UserServiceTests
    {
        [Fact]
        void GetShippingAddress_should_throw_exception_when_not_logged_in_user()
        {
            var userService = new UserService();
            var user = new User();

            userService.Invoking(u => u.GetShippingAddress(user))
                .Should().Throw<UserNotLoggedInException>();
        }

        [Fact]
        void GetShippingAddress_should_return_shipping_address()
        {
            var user = CreateUser(true);
            var userService = new UserService();

            userService.LogInUser(user);

            var shippingAddress = userService.GetShippingAddress(user);
            shippingAddress.Should().Be(user.ShippingAddress);
        }

        [Fact]
        void GetShippingAddress_should_return_null_when_no_shipping_address()
        {
            var user = CreateUser(false);
            var userService = new UserService();

            userService.LogInUser(user);

            var shippingAddress = userService.GetShippingAddress(user);
            shippingAddress.Should().Be(null);
        }

        private User CreateUser(bool withShippingAddress)
        {
            var user = new User
            {
                Id = 1,
                Name = "User1",
            };

            var shippingAddress = new Address
            {
                Street = "Street",
                ZipCode = "28001"
            };

            user.ShippingAddress = withShippingAddress
                ? shippingAddress
                : null;

            return user;
        }
    }
}