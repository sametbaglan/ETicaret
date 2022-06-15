
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ETicaret.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public void Create(Product entity)
        {
            _productDal.Create(entity);
        }

        public void Delete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productDal.GetAll(x=>x.IsDeleted==false&&x.IsActive==true);
        }

        public Product GetByFindProductName(string name)
        {
            return _productDal.GetByFindProductName(name);
        }

        public Product GetById(int id)
        {
            return _productDal.GetByID(id);
        }

        public Product GetByProductDetail(int productid)
        {
            return _productDal.GetByProductDetail(productid);
        }

        public List<Product> GetByWithCategoriByProduct(int categoriaid)
        {
            return _productDal.GetByWithCategoriByProduct(categoriaid, x => x.IsActive == true&&x.IsDeleted==false);
        }

        public void Update(Product entity)
        {
            _productDal.Update(entity);
        }
    }
}
