using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ETicaret.BusinessLayer.Abstrack
{
    public  interface IImagesService
    {
        Images GetImageWithProductId(int productid);
        List<Images> GetImagesWithProductid(int productid);
        List<Images> GetAll(int productid);
        Images GetById(int id);
        void Create(Images entity);
        void Update(Images entity);
        void Delete(Images entity);
    }
}
