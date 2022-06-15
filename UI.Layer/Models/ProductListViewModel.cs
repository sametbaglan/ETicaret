using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using System.Collections.Generic;
using System.Linq;
namespace ETicaret.UILayer.Models
{
    public class ProductListViewModel
    {
        public List<Product> Products { get; set; }
        public List<Categoria> Categorias { get; set; }
        public static List<BodySizes> bodySizes { get; set; }
        public BodySizes bodySize { get; set; }
        public ItemColors ıtemColor { get; set; }
        public List<ItemColors> ıtemColors { get; set; }
        public Categoria categoria { get; set; }
        public Product Product { get; set; }
        public static string returnCategoryname(int categoryid)
        {
            DataContext db = new DataContext();
            var result = db.Categorias.Where(x => x.CategoriaID == categoryid);
            var deger = "";
            foreach (var item in result)
            {
                deger = item.CategoryName;
            }
            return deger;

        }
        public static string returnproduct(int productid)
        {
            DataContext db = new DataContext();
            var result = db.Products.Where(x => x.ProductID == productid);
            var deger = "";
            foreach (var item in result)
            {
                deger = item.ProductName;
            }
            return deger;

        }
        public static object ReturnBody(int bodyid)
        {
            ProductListViewModel.bodySizes = null;
            DataContext db = new DataContext();
            var body = db.BodySizes.Where(x => x.ItemColorid == bodyid).ToList();
            ProductListViewModel.bodySizes = body;
            return null;
        }
        public static string ReturnColor(int colorid)
        {
            DataContext db = new DataContext();
            var body = db.ItemColor.Where(x => x.QuickID == colorid).FirstOrDefault();
            return body.Color;
        }
        public static Shipmens shipmens { get; set; }

        public static List<BodySizes> tocolorbody(int colorid)
        {
            DataContext db = new DataContext();
            var bdy = db.BodySizes.Where(x => x.ItemColorid == colorid).ToList();
            
            return bdy;
        }
        public static Product product(int id)
        {
            DataContext db = new DataContext();
            var product = db.Products.FirstOrDefault(x => x.ProductID == id);
            return product;
        }
        public static string ColorName(int bodyid)
        {
            DataContext db = new DataContext();
            var color = db.ItemColor.FirstOrDefault(x => x.QuickID == bodyid);
            return color.Color;
        }
        public static object Shipmenıtems(int orderid)
        {
            var db = new DataContext();
            var shipmen = db.shipmens.Where(x => x.Id == orderid).FirstOrDefault();
            ProductListViewModel.shipmens =  shipmen;
            return null;
        }
        public static string BedenName(int bodyid)
        {
            DataContext db = new DataContext();
            var color = db.BodySizes.FirstOrDefault(x => x.BodyID == bodyid);
            return color.BodyName;
        }
    }
}
