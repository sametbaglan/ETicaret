using Business.Layer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.IdentityModels.MainLayoutModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.Layer.Models.OrdersModels
{

    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public int OrderId { get; set; }
        public Shipmens Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public decimal Price { get; set; }
        public decimal? ReducedPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? CargoPrice { get; set; }

        public static decimal TotalPrice(int orderid)
        {
            DataContext db = new DataContext();
            var ds = db.shipmens.ToList();
            var shipmenItems = db.shipmenItems.ToList();
            decimal price = 0;
            foreach (var prices in shipmenItems)
            {
                if (prices.ReducedPrice > 0)
                    price += (decimal)prices.ReducedPrice * prices.Quantity;
                else
                    price += prices.Price * prices.Quantity;
                if (prices.CargoPrice > 0)
                    price += 50;
            }
            return price;
        }
        public List<ShipmenItem> ShipmenItems { get; set; }
        public static object ShipmenProductName(int orderid, AllLayoutModel model)
        {
            DataContext db = new DataContext();
            List<ShipmenItem> shipmenItems = new List<ShipmenItem>();
            var item = db.shipmenItems.Where(x => x.OrderId == orderid).ToList();
            foreach (var itms in item)
            {
                var entity = new ShipmenItem
                {
                    CargoPrice = itms.CargoPrice,
                    Id = itms.Id,
                    OrderId = itms.OrderId,
                    Order = itms.Order,
                    Price = itms.Price,
                    Product = db.Products.FirstOrDefault(x => x.ProductID == itms.ProductId),
                    ProductId = itms.ProductId,
                    Quantity = itms.Quantity,
                    ReducedPrice = itms.ReducedPrice,
                    State=itms.State,
                    beden=itms.beden,
                    renk=itms.renk,
                    CargoCode=itms.CargoCode
                };
                shipmenItems.Add(entity);
            }
            model.ShipmenList = shipmenItems;
            return null;
        }
        public static List<ShipmenItem> ShipmenItem(int orderid)
        {
            DataContext db = new DataContext();
            var getshipmenwithorderid = db.shipmenItems.Where(x => x.OrderId == orderid).ToList();
            return getshipmenwithorderid;
        }
        public static decimal toplam()
        {
            DataContext db = new DataContext();
            decimal totalprice = 0;
            bool cargo = false;
            var shipmen = db.shipmens.ToList();
            foreach (var item in shipmen)
            {
                cargo = false;
                var shipmenitem = db.shipmenItems.Where(x => x.OrderId == item.Id&&x.State==3).ToList();
                foreach (var shipmens in shipmenitem)
                {
                    if (shipmens.ReducedPrice > 0)
                    {
                        totalprice += (decimal)shipmens.ReducedPrice * shipmens.Quantity;
                    }
                    else
                    {
                        totalprice += shipmens.Price * shipmens.Quantity;
                    }
                    if (shipmens.CargoPrice > 0)
                    {
                        if (cargo == false)
                        {
                            totalprice += (decimal)shipmens.CargoPrice;
                            cargo = true;
                        }
                    }

                }
            }
            return totalprice;
        }
        public static decimal adetilefiyat(int id)
        {
            DataContext db = new DataContext();
            var result = db.shipmenItems.Where(x => x.OrderId == id).ToList();
            decimal fiyat = 0;
            bool cargo = false;
            foreach (var item in result)
            {
                if (item.ReducedPrice > 0)
                    fiyat += (decimal)item.ReducedPrice * item.Quantity;
                else
                    fiyat += item.Price * item.Quantity;
                if (cargo == false)
                {
                    fiyat += (decimal)item.CargoPrice;
                    cargo = true;
                }
                else { }
            }
            return fiyat;
        }
        public static string renk(int id)
        {
            var db = new DataContext();
            return db.ItemColor.Where(x => x.QuickID == id).FirstOrDefault().Color;
        }
        public static string beden(int id)
        {
            var db = new DataContext();
            return db.BodySizes.Where(x => x.BodyID == id).FirstOrDefault().BodyName;
        }
    }

}
