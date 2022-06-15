using ETicaret.EntityLayer;
using System.Collections.Generic;

namespace ETicaret.DataAccessLayer.Abstrack
{
    public  interface IImagesDal:IRepository<Images>
    {
        List<Images> GetImagesWithProductid(int productid);
        Images GetImageWithProductId(int productid);
    }
}
