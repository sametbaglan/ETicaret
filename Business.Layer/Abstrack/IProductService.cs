using ETicaret.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ETicaret.BusinessLayer.Abstrack
{
    public  interface IProductService
    {
        List<Product> GetByWithCategoriByProduct(int categoriaid);
        Product GetByFindProductName(string name);
        Product GetByProductDetail(int productid);
        #region Crud
        List<Product> GetAll();
        Product GetById(int id);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
        #endregion
    }
}
