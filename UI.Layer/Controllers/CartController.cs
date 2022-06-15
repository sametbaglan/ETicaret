using ETicaret.BusinessLayer.Abstrack;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using ETicaret.UILayer.Models.OrdersModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UI.Layer.Models;
namespace ETicaret.UILayer.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartDalService _cartDalService;
        private readonly ICartItemService _cartItemService;
        private readonly IUsersService _usersService;
        private readonly IProductService _productService;
        private readonly IItemBodySizeService _ıtemBodySizeService;
        public CartController(IItemBodySizeService ıtemBodySizeService, ICartDalService cartDalService, IProductService productService, ICartItemService cartItemService, IUsersService usersService)
        {
            _cartItemService = cartItemService;
            _cartDalService = cartDalService;
            _usersService = usersService;
            _productService = productService;
            _ıtemBodySizeService = ıtemBodySizeService;
        }
        AllLayoutModel model = new AllLayoutModel();
        public IActionResult CartItemView()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            Users users = _usersService.GetById(userid);
            model.UserInfo = users;
            var ordermodel = new OrderModel();
            var cart = _cartDalService.GetCartByUserId(userid);
            if (cart == null)
                return View(new AllLayoutModel());
            ordermodel.CartItems = cart.CartItems;
            return View(new AllLayoutModel()
            {
                Token = token,
                CartItems = ordermodel.CartItems,
            });
        }
        public IActionResult AddToCart(int productid, int quantity, string beden, string color)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Account/Login");
            }
            List<CartItem> listCartItem = new List<CartItem>();
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var cartid = _cartDalService.GetCartByUserId(userid);
            var product = _productService.GetById(productid);
            var productitem = _cartItemService.GetByProductId(productid);
            if (string.IsNullOrEmpty(beden) || string.IsNullOrEmpty(color))
            {
                string message = "Lütfen attirbute seçimlerinizi yapınız";
                HttpContext.Session.SetString("message", message);
                return Redirect("/Product/ProductDetail/" + productid);
            }
            var body = _ıtemBodySizeService.GetById(Convert.ToInt32(beden));
            if (quantity<=body.BodyCount)
            {
                decimal price = 0;
                if (product.ReducedPrice > 0)
                    price = (decimal)product.ReducedPrice;
                else
                    price = product.Price;
                if (productitem != null)
                {
                    if (productitem.beden == beden && productitem.color == color)
                    {
                        productitem.Quantity += quantity;
                        productitem.Cart = cartid;
                        productitem.totalprice = productitem.Quantity * price;
                        productitem.price = product.Price;
                        productitem.reducedprice = (decimal)product.ReducedPrice;
                        listCartItem.Add(productitem);
                        cartid.CartItems = listCartItem;
                        _cartItemService.CartItemUpdate(productitem);
                    }
                    else
                    {
                        var tutar = quantity * price;
                        productitem = new CartItem()
                        {
                            Product = product,
                            ProductID = productid,
                            beden = beden,
                            Cart = cartid,
                            CartID = cartid.CartID,
                            color = color,
                            Quantity = quantity,
                            totalprice = tutar,
                            price = product.Price,
                            reducedprice = (decimal)product.ReducedPrice
                        };
                        listCartItem.Add(productitem);
                        var entity = new Cart()
                        {
                            CartID = cartid.CartID,
                            CartItems = listCartItem,
                            UserId = userid
                        };
                        _cartDalService.Update(entity);
                    }
                }
                else
                {
                    var tutar = quantity * price;
                    productitem = new CartItem()
                    {
                        Product = product,
                        ProductID = productid,
                        beden = beden,
                        Cart = cartid,
                        CartID = cartid.CartID,
                        color = color,
                        Quantity = quantity,
                        totalprice = tutar,
                        price = product.Price,
                        reducedprice = (decimal)product.ReducedPrice
                    };
                    listCartItem.Add(productitem);
                    var entity = new Cart()
                    {
                        CartID = cartid.CartID,
                        CartItems = listCartItem,
                        UserId = userid
                    };
                    _cartDalService.Update(entity);
                }
            }
            else
            {
                string message = "Seçmiş olduğunuz miktar, beden miktarından büyük olamaz ";
                HttpContext.Session.SetString("message", message);
            }
            return Redirect("/Product/ProductDetail/" + productid);

        }
        public IActionResult DeleteCartItem(int productid)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var cartitem = _cartItemService.DeleteCartItemByProductid(userid, productid);
            if (cartitem != null)
            {
                _cartItemService.Delete(cartitem);
            }
            return RedirectToAction("Checkout", "Checkout");
        }
    }
}
