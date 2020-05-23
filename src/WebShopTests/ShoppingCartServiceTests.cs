using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using WebShop;
using Xunit;

namespace WebShopTests
{
    public class ShoppingCartServiceTests
    {
        [Fact]
        void AddToCart_should_throw_exception_when_user_is_not_logged_in()
        {
            var user = CreateUser();
            var product = CreateProduct(1);
            var userService = Substitute.For<IUserService>();
            userService.IsUserLoggedIn(user.Id).Returns(false);

            var shoppingCartService = new ShoppingCartService(userService);

            shoppingCartService.Invoking(s => s.AddToCart(user.Id, product))
                .Should().Throw<UserNotLoggedInException>();
        }

        [Fact]
        void AddToCart_should_add_first_product()
        {
            var user = CreateUser();
            var product = CreateProduct(1);
            var userService = Substitute.For<IUserService>();
            userService.IsUserLoggedIn(user.Id).Returns(true);

            var shoppingCartService = new ShoppingCartService(userService);

            shoppingCartService.AddToCart(user.Id, product);

            shoppingCartService.GetProductsByUserId(user.Id)
                .Should().ContainEquivalentOf(product);
        }

        [Fact]
        void AddToCart_should_add_second_product()
        {
            var user = CreateUser();
            var product1 = CreateProduct(1);
            var product2 = CreateProduct(2);
            var userService = Substitute.For<IUserService>();
            userService.IsUserLoggedIn(user.Id).Returns(true);

            var shoppingCartService = new ShoppingCartService(userService);

            shoppingCartService.AddToCart(user.Id, product1);
            shoppingCartService.AddToCart(user.Id, product2);

            shoppingCartService.GetProductsByUserId(user.Id)
                .Should().ContainEquivalentOf(product2);
        }

        [Fact]
        void GetProductsByUserId_should_throw_exception_when_user_is_not_logged_in()
        {
            var user = CreateUser();
            var userService = Substitute.For<IUserService>();
            userService.IsUserLoggedIn(user.Id).Returns(false);

            var shoppingCartService = new ShoppingCartService(userService);

            shoppingCartService.Invoking(s => s.GetProductsByUserId(user.Id))
                .Should().Throw<UserNotLoggedInException>();
        }

        [Fact]
        void GetProductsByUserId_should_not_return_products()
        {
            var user = CreateUser();
            var userService = Substitute.For<IUserService>();
            userService.IsUserLoggedIn(user.Id).Returns(true);

            var shoppingCartService = new ShoppingCartService(userService);

            var products = shoppingCartService.GetProductsByUserId(user.Id);
            products.Should().BeNullOrEmpty();
        }

        [Fact]
        void GetProductsByUserId_should_return_products()
        {
            var user = CreateUser();
            var product = CreateProduct(1);
            var userService = Substitute.For<IUserService>();
            userService.IsUserLoggedIn(user.Id).Returns(true);

            var shoppingCartService = new ShoppingCartService(userService);
            shoppingCartService.AddToCart(user.Id, product);

            var products = shoppingCartService.GetProductsByUserId(user.Id);
            products.Should().ContainEquivalentOf(product);
        }

        private User CreateUser()
        {
            var user = new User
            {
                Id = 1,
                Name = "User1",
            };

            return user;
        }

        private Product CreateProduct(int productId)
        {
            return new Product
            {
                ProductId = productId,
                Name = "Product",
                Price = 44.5m
            };
        }
    }
}
