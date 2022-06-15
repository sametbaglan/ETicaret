using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;

namespace ETicaret.UILayer.Models.OrdersModels
{
    public class OrderModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Emal { get; set; }
        public string CartdName { get; set; }
        public string CartNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }
        public List<CartItem> CartItems { get; set; }
        public static decimal totalprice { get; set; }
        public static decimal singularprice { get; set; }
        public static decimal SumCalculation(OrderModel cartItems, bool cargo)
        {
            decimal price = 0;
            singularprice = 0;
            if (cargo == false)
            {
                foreach (var item in cartItems.CartItems)
                {
                    if (item.Product.ReducedPrice > 0)
                    {
                        price += item.Quantity * (decimal)item.Product.ReducedPrice;
                    }
                    else
                    {
                        price += item.Quantity * item.Product.Price;
                    }
                }
                return price;
            }
            else
            {
                foreach (var item in cartItems.CartItems)
                {
                    if (item.Product.ReducedPrice > 0)
                    {
                        price += item.Quantity * (decimal)item.Product.ReducedPrice;
                    }
                    else
                    {
                        price += item.Quantity * item.Product.Price;
                    }

                }
                return price + 30;
            }
        }
        public static decimal ReturnCalculation(List<CartItem> cartitems)
        {
            decimal price = 0;
            foreach (var item in cartitems)
            {
                if (item.Product.ReducedPrice > 0)
                {
                    price += (decimal)item.Product.ReducedPrice * item.Quantity;
                }
                else
                {
                    price += item.Product.Price * item.Quantity;
                }
            }
            if (price < 100)
                price = price + 30;
            return price;
        }
        public static decimal JustSumItemsPrice(List<CartItem> cartItems)
        {
            decimal price = 0;
            foreach (var item in cartItems)
            {
                if (item.Product.ReducedPrice > 0)
                {
                    price += (decimal)item.Product.ReducedPrice * item.Quantity;
                }
                else
                {
                    price += item.Product.Price * item.Quantity;
                }
            }
            return price;
        }
    }
}
