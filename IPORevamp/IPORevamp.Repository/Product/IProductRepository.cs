using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;



namespace IPORevamp.Repository.Product
{
   public  interface IProductRepository : IAutoDependencyRegister
    {
        #region Product Respository

     
        Task<Data.Entity.Interface.Entities.Product.Product> SaveProduct(Data.Entity.Interface.Entities.Product.Product product);
        Task<Data.Entity.Interface.Entities.Product.Product> UpdateProduct(Data.Entity.Interface.Entities.Product.Product product);
       
        Task<List<Data.Entity.Interface.Entities.Product.Product>> GetProduct();

        Task<Data.Entity.Interface.Entities.Product.Product> CheckExistingProduct(string Code);
        Task<Data.Entity.Interface.Entities.Product.Product> GetProductById(int product);


        Task<Data.Entity.Interface.Entities.Product.Product> DeleteProduct(Data.Entity.Interface.Entities.Product.Product product);
        #endregion
    }
}
