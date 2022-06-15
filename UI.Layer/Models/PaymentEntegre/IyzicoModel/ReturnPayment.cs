using ETicaret.EntityLayer;
using ETicaret.UILayer.Models;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using ETicaret.UILayer.Models.OrdersModels;
using IyzipayCore;
using IyzipayCore.Model;
using IyzipayCore.Request;
using System;
using System.Collections.Generic;

namespace UI.Layer.Models.PaymentEntegre.IyzicoModel
{
    public class ReturnPayment
    {
        public List<BasketItem> BasketItems { get; set; }
        public Payment Payment { get; set; }
        public static ReturnPayment returnPayment(AllLayoutModel model, Cart cart, decimal price)
        {
            Options options = new Options();
            //options.ApiKey = "sandbox-gzQb2YHmNZQTCZb4mYwO0MT1ctxROYTA";
            //options.SecretKey = "sandbox-OAqqQ0fNhREBnuL0hrElS2Eavt28yLjR";
            //options.BaseUrl = "https://sandbox-api.iyzipay.com/";
            options.ApiKey = "F4B9xSc3ms9dlNiMpZbCezVtPTatQ4eC";
            options.SecretKey = "Y7yYs9ek4VhdaXX3OcJ3TIb9ZXmDfiBz";
            options.BaseUrl = "https://api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            decimal calculationprice = OrderModel.ReturnCalculation(cart.CartItems);
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = calculationprice.ToString().Split(",")[0];
            request.PaidPrice =  calculationprice.ToString().Split(",")[0];
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = cart.CartID.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.orderModel.CartdName;
            paymentCard.CardNumber = model.orderModel.CartNumber;
            paymentCard.ExpireMonth = model.orderModel.ExpirationMonth;
            paymentCard.ExpireYear = model.orderModel.ExpirationYear;
            paymentCard.Cvc = model.orderModel.CVV;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            //PaymentCard paymentCard = new PaymentCard();
            //paymentCard.CardHolderName = "John Doe";
            //paymentCard.CardNumber = "5528790000000008";
            //paymentCard.ExpireMonth = "12";
            //paymentCard.ExpireYear = "2030";
            //paymentCard.Cvc = "123";
            //paymentCard.RegisterCard = 0;
            //request.PaymentCard = paymentCard;

            //Buyer buyer = new Buyer();
            //buyer.Id = "BY789";
            //buyer.Name = "John";
            //buyer.Surname = "Doe";
            //buyer.GsmNumber = "+905350000000";
            //buyer.Email = "email@email.com";
            //buyer.IdentityNumber = "74300864791";
            //buyer.LastLoginDate = "2015-10-05 12:43:35";
            //buyer.RegistrationDate = "2013-04-21 15:12:09";
            //buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            //buyer.Ip = "85.34.78.112";
            //buyer.City = "Istanbul";
            //buyer.Country = "Turkey";
            //buyer.ZipCode = "34732";
            //request.Buyer = buyer;


            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = model.orderModel.FirstName;
            buyer.Surname = model.orderModel.LastName;
            buyer.GsmNumber = model.orderModel.Phone;
            buyer.Email = model.orderModel.Emal;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = model.orderModel.Address;
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            //Address shippingAddress = new Address();
            //shippingAddress.ContactName = "Jane Doe";
            //shippingAddress.City = "Istanbul";
            //shippingAddress.Country = "Turkey";
            //shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            //shippingAddress.ZipCode = "34742";
            //request.ShippingAddress = shippingAddress;

            //Address billingAddress = new Address();
            //billingAddress.ContactName = "Jane Doe";
            //billingAddress.City = "Istanbul";
            //billingAddress.Country = "Turkey";
            //billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            //billingAddress.ZipCode = "34742";
            //request.BillingAddress = billingAddress;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "";
            shippingAddress.City = model.orderModel.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = model.orderModel.Address;
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "";
            billingAddress.City = model.orderModel.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = model.orderModel.Address;
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;
            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem basket;
            var justitemsprice = OrderModel.JustSumItemsPrice(cart.CartItems);
            foreach (var item in cart.CartItems)
            {
                if (justitemsprice < 100)
                {
                    justitemsprice = justitemsprice + 30;
                    basket = new BasketItem();
                    basket.Id = item.ProductID.ToString();
                    basket.Name = item.Product.ProductName;
                    basket.Category1 = ProductListViewModel.returnCategoryname(item.Product.CategoryID);
                    basket.ItemType = BasketItemType.PHYSICAL.ToString();
                    basket.Price = justitemsprice.ToString().Split(",")[0];
                    //basket.Price = "1";
                    basketItems.Add(basket);
                }
                else
                {
                    decimal itemprice = 0;
                    if (item.reducedprice > 0)
                        itemprice = item.reducedprice * item.Quantity;
                    else
                        itemprice = item.price * item.Quantity; 
                    basket = new BasketItem();
                    basket.Id = item.ProductID.ToString();
                    basket.Name = item.Product.ProductName;
                    basket.Category1 = ProductListViewModel.returnCategoryname(item.Product.CategoryID);
                    basket.ItemType = BasketItemType.PHYSICAL.ToString();
                    basket.Price = itemprice.ToString().Split(",")[0];
                    basketItems.Add(basket);
                }
            }
            request.BasketItems = basketItems;
            var payment = Payment.Create(request, options);
            var entity = new ReturnPayment
            {
                Payment = payment,
                BasketItems = basketItems
            };
            return entity;
        }
    }
}
