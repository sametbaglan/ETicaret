using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.CodeFirst;
using ETicaret.EntityLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ETicaret.DataAccessLayer.Concrete
{

    public class EfImagesDal : EfRepository<Images, DataContext>, IImagesDal
    {
        public List<Images> GetImagesWithProductid(int productid)
        {
            using(var db=new DataContext())
            {
           return    db.Images.Include(x => x.Product).ThenInclude(x => x.Images).Where(x => x.ProductID == productid).ToList();
            }
        }

        public Images GetImageWithProductId(int productid)
        {
            using (var db = new DataContext())
            {
                return db.Images.Include(x => x.Product).ThenInclude(x => x.Images).Where(x => x.ProductID == productid).FirstOrDefault();
            }
        }
    }
}
