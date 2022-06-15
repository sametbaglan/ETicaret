using Business.Layer.Abstrack;
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using ETicaret.UILayer.Models.OrdersModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UI.Layer.Models;
using UI.Layer.Models.PaymentEntegre;
using UI.Layer.Models.PaymentEntegre.IyzicoModel;
namespace UI.Layer.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartDalService _cartDalService;
        private readonly IUsersService _usersService;
        private readonly ICategoriaService _categoriesService;
        private readonly IShipmenService _orderService;
        private readonly ICartItemService _cartItemService;
        private IShipmenItemsService _shipmenItemss;
        private IProductService _productService;
        private readonly IItemBodySizeService _ıtemBodySizeService;
        AllLayoutModel layoutModel = new AllLayoutModel();
        public CheckoutController(IItemBodySizeService ıtemBodySizeService, IProductService productService, ICartItemService cartItemService, IShipmenItemsService shipmenItemss, IShipmenService orderService, ICartDalService cartDalService, ICategoriaService categoriesService, IUsersService usersService)
        {
            _cartDalService = cartDalService;
            _categoriesService = categoriesService;
            _usersService = usersService;
            _orderService = orderService;
            _shipmenItemss = shipmenItemss;
            _cartItemService = cartItemService;
            _productService = productService;
            _ıtemBodySizeService = ıtemBodySizeService;
        }
        public IActionResult Checkout()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var user = _usersService.GetById(userid);
            layoutModel.UserInfo = user;
            var card = _cartDalService.GetCartByUserId(userid);
            if (card == null) return View(layoutModel);
            OrderModel orderModel = new OrderModel();
            decimal toplamprice = 00;
            foreach (var item in card.CartItems)
            {
                if (item.Product.ReducedPrice > 0)
                {
                    toplamprice += (decimal)item.Product.ReducedPrice * item.Quantity;
                }
                else
                {
                    toplamprice += item.Product.Price * item.Quantity;
                }
            }
            layoutModel.fiyat = toplamprice;
            orderModel.CartItems = card.CartItems;
            layoutModel.orderModel = orderModel;
            layoutModel.Categorias = _categoriesService.GetAll();
            return View(layoutModel);
        }
        [HttpGet]
        public IActionResult PaymentProccesing()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var user = _usersService.GetById(userid);
            layoutModel.UserInfo = user;
            var card = _cartDalService.GetCartByUserId(user.UserId);
            OrderModel orderModel = new OrderModel();
            orderModel.CartItems = card.CartItems;
            layoutModel.orderModel = orderModel;
            layoutModel.Categorias = _categoriesService.GetAll();
            return View(layoutModel);
        }
        [HttpPost]
        public IActionResult PaymentProccesing(AllLayoutModel model)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var user = _usersService.GetById(userid);
            model.orderModel.Emal = user.Email;
            var cart = _cartDalService.GetCartByUserId(user.UserId);
            model.Categorias = _categoriesService.GetAll();
            model.UserInfo = user;
            var payment = ReturnPayment.returnPayment(model, cart, OrderModel.singularprice);
            var entity = new PaymentResponseViewModel();
            if (payment.Payment.Status == "success")
            {
                model.orderModel.CartItems = cart.CartItems;
                SaveOrder(model, payment, user.UserId);
                ClearCart(user.UserId);
                 entity = new PaymentResponseViewModel
                {
                    ErrorMessage = "Ödeme İşlemi Başarılı.",
                    Result = true
                };
            }
            else
            {
                entity = new PaymentResponseViewModel
                {
                    ErrorMessage = payment.Payment.ErrorMessage,
                    Result = false
                };
            }
            return RedirectToAction("PaymentResponse",entity);
        }
        [HttpGet]
        public IActionResult PaymentResponse(PaymentResponseViewModel model)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
            {
                return Redirect("/Error/501");
            }
            var userid = TokenUserValueFunc.TokenGetValue(token);
            var user = _usersService.GetById(userid);
            var cart = _cartDalService.GetCartByUserId(user.UserId);
            if (model.Result == true)
            {
                ViewBag.Message = "Ödemeniz Başarıyla Alınmıştır.";
                ViewBag.messagecolor = "alert alert-success";
                ViewBag.State = "Success";
            }else
            {
                ViewBag.Message = model.ErrorMessage;
                ViewBag.messagecolor = "alert alert-danger";
                ViewBag.State = "Ödeme Başarısız";
            }
            return View(new AllLayoutModel
            {
                UserInfo=user,
               Categorias=_categoriesService.GetAll()
            });
        }
        private void ClearCart(int userId)
        {
            try
            {
                List<CartItem> carts = new List<CartItem>();
                var item = _cartDalService.GetCartByUserId(userId);
                carts = item.CartItems;
                while (item.CartItems.Count > 0)
                {
                    for (int i = 0; i < carts.Count; i++)
                    {
                        _cartItemService.Delete(carts[i]);
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        private void SaveOrder(AllLayoutModel model, ReturnPayment payment, int userId)
        {
            var order = new Shipmens();
            List<ShipmenItem> shipmenItems = new List<ShipmenItem>();
            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderState = "Complated";
            order.PaymentTypes = "CreditCart";
            order.PaymentId = payment.Payment.PaymentId;
            order.ConversationId = payment.Payment.ConversationId;
            order.OrderDate = DateTime.Now;
            order.FirstName = model.orderModel.FirstName;
            order.LastName = model.orderModel.LastName;
            order.Email = model.orderModel.Emal;
            order.Phone = model.orderModel.Phone;
            order.Address = model.orderModel.Address;
            order.UserId = userId.ToString();
            _orderService.Create(order);
            DataContext db = new DataContext();
            decimal singprice = OrderModel.SumCalculation(model.orderModel, false);
            var ship = db.shipmens.Where(x => x.ConversationId == order.ConversationId).FirstOrDefault();
            if (ship != null)
            {
                if (singprice < 100)
                {
                    foreach (var item in model.orderModel.CartItems)
                    {
                        var bedencount = _ıtemBodySizeService.GetById(Convert.ToInt32(item.beden));
                        bedencount.BodyCount = bedencount.BodyCount - item.Quantity;
                        _ıtemBodySizeService.Update(bedencount);
                        var orderitem = new ShipmenItem
                        {
                            Price = item.Product.Price,
                            ReducedPrice = item.Product.ReducedPrice,
                            CargoPrice=30,
                            Quantity = item.Quantity,
                            ProductId = item.ProductID,
                            OrderId = ship.Id,
                            renk =  item.color,
                            beden = item.beden,
                            State=1,
                        };
                        _shipmenItemss.Create(orderitem);
                    }
                }
                else
                {
                    foreach (var item in model.orderModel.CartItems)
                    {
                        var bedencount = _ıtemBodySizeService.GetById(Convert.ToInt32(item.beden));
                        bedencount.BodyCount = bedencount.BodyCount - item.Quantity;
                        _ıtemBodySizeService.Update(bedencount);
                        decimal calculationprice = 0;
                        if (item.reducedprice > 0)
                            calculationprice = item.reducedprice * item.Quantity;
                        else
                            calculationprice = item.price * item.Quantity;
                        var orderitem = new ShipmenItem
                        {
                            ReducedPrice = item.Product.ReducedPrice,
                            Price = item.Product.Price,
                            CargoPrice = 0,
                            Quantity = item.Quantity,
                            ProductId = item.ProductID,
                            OrderId = ship.Id,
                            renk = item.color,
                            beden = item.beden,
                            State=1
                        };
                        _shipmenItemss.Create(orderitem);
                    }
                }
            }
        }
        public void DeleteItemsCount(AllLayoutModel model)
        {
           foreach(var item in model.orderModel.CartItems)
            {
                var productquantity = _productService.GetById(item.ProductID);
               
            }
        }
    }
}
