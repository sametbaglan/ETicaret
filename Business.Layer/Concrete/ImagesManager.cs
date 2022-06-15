using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ETicaret.BusinessLayer.Concrete
{
    public class ImagesManager : IImagesService
    {
        private IImagesDal _ımagesDal;
        public ImagesManager(IImagesDal ımagesDal)
        {
            _ımagesDal = ımagesDal;
        }
        public void Create(Images entity)
        {
            _ımagesDal.Create(entity);
        }

        public void Delete(Images entity)
        {
            _ımagesDal.Delete(entity);
        }
        public List<Images> GetAll(int productid)
        {
            return _ımagesDal.GetAll(x=>x.ProductID==productid);
        }
        public Images GetById(int id)
        {
            return _ımagesDal.GetByID(id);
        }

        public List<Images> GetImagesWithProductid(int productid)
        {
            return _ımagesDal.GetImagesWithProductid(productid);
        }

        public Images GetImageWithProductId(int productid)
        {
            return _ımagesDal.GetImageWithProductId(productid);
        }

        public void Update(Images entity)
        {
            _ımagesDal.Update(entity);
        }
    }
}
