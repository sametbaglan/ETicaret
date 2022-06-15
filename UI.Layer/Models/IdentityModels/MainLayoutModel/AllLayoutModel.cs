using Business.Layer.Entegration.Trendyol.GetShipmenPackages;
using Business.Layer.Entegration.Trendyol.ProductAddPost;
using Business.Layer.Entegration.Trendyol.TrendyolBrand;
using Business.Layer.Entegration.Trendyol.TrendyolCategoria;
using ETicaret.EntityLayer;
using ETicaret.UILayer.Models.OrdersModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using UI.Layer.Models.OrdersModels;
using UI.Layer.Models.UserModel;

namespace ETicaret.UILayer.Models.IdentityModels.MainLayoutModel
{
    public class AllLayoutModel
    {
        public ShipmenItem shipmens { get; set; }
        public List<Shipmens> Shipmens { get; set; }
        public List<ShipmenItem>  ShipmenList{ get; set; }
        public List<TrendyolCategoria> TrendyolCategoryList { get; set; }
        public InfoSendEmail ınfoSendEmail{ get; set; }
        public Images ımage { get; set; }
        public List<Images> ListImages { get; set; }
        public SubCategoria subCategoria{ get; set; }
        public Brand TrendyolBrad{ get; set; }
        public TrendyolProduct TrendyolProduct{ get; set; }
        public List<ShipmenContent> TrendyolShipmenList{ get; set; }
        public List<ShipmenContent> NewOrderListGet { get; set; }
        public Users UserInfo{ get; set; }
        public List<Product> Products { get; set; }
        public List<EntityLayer.Categoria> Categorias { get; set; }
        public List<BodySizes> bodySizes { get; set; }
        public BodySizes bodySize { get; set; }
        public ItemColors ıtemColor { get; set; }
        public List<ItemColors> ıtemColors { get; set; }
        public EntityLayer.Categoria categoria { get; set; }
        public Product Product { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<SelectListItem> SelectListItems{ get; set; }
        public RegisterModel RegisterModel { get; set; }
        public LoginModel LoginModel { get; set; }
        public OrderModel  orderModel{ get; set; }
        public OrderModel ModelOrder { get; set; }
        public string Token { get; set; }
        public string categoryname { get; set; }
        public decimal fiyat { get; set; }
    }
}
