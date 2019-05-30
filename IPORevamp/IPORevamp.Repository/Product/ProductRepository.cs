using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Product
{
    class ProductRepository:IProductRepository
    {
        private IRepository<Data.Entity.Interface.Entities.Product.Product> _productrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public ProductRepository(IRepository<Data.Entity.Interface.Entities.Product.Product> productrepository)
        {
            _productrepository = productrepository;


    }

        public async Task<Data.Entity.Interface.Entities.Product.Product> GetProductById(int ProdId)
        {
            Data.Entity.Interface.Entities.Product.Product product = new Data.Entity.Interface.Entities.Product.Product();
            product = await _productrepository.FirstOrDefaultAsync(x => x.Id == ProdId);

            return product;
        }

        public async Task<Data.Entity.Interface.Entities.Product.Product> CheckExistingProduct(string Code)
        {
            Data.Entity.Interface.Entities.Product.Product product = new Data.Entity.Interface.Entities.Product.Product();

            product = await _productrepository.FirstOrDefaultAsync(x => x.Code.ToUpper() == Code.ToUpper());


            return product;
        }

        public async Task<Data.Entity.Interface.Entities.Product.Product>  SaveProduct(Data.Entity.Interface.Entities.Product.Product product)
        {

            var saveContent = await _productrepository.InsertAsync(product);
            await _productrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entity.Interface.Entities.Product.Product> UpdateProduct(Data.Entity.Interface.Entities.Product.Product product)
        {

            var saveContent = await _productrepository.UpdateAsync(product);
            await _productrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<List<Data.Entity.Interface.Entities.Product.Product>> GetProduct()
        {

            List < Data.Entity.Interface.Entities.Product.Product > product = new List< Data.Entity.Interface.Entities.Product.Product > ();
            product  = await _productrepository.GetAllListAsync();
            return product;
        }


        public async Task<Data.Entity.Interface.Entities.Product.Product> DeleteProduct(Data.Entity.Interface.Entities.Product.Product product)
        {
            var saveContent = await _productrepository.UpdateAsync(product);
            await _productrepository.SaveChangesAsync();

            return saveContent.Entity;
        }




    }
}
