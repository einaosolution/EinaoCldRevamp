
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
namespace IPORevamp.Repository.Ministry
{
   public  interface IministryRepository : IAutoDependencyRegister
    {
        #region

        Task<Data.Entity.Interface.Entities.Ministry.Ministry> SaveUnit(Data.Entity.Interface.Entities.Ministry.Ministry ministry);
        Task<Data.Entity.Interface.Entities.Ministry.Ministry> UpdateUnit(Data.Entity.Interface.Entities.Ministry.Ministry ministry);

        Task<List<Data.Entity.Interface.Entities.Ministry.Ministry>> GetMinistry();

        Task<Data.Entity.Interface.Entities.Ministry.Ministry> CheckExistingMinistry(string Code);
        Task<Data.Entity.Interface.Entities.Ministry.Ministry> GetMinistryById(int product);


        Task<Data.Entity.Interface.Entities.Ministry.Ministry> DeleteMinistry(Data.Entity.Interface.Entities.Ministry.Ministry ministry);

        #endregion
    }
}
