
using IPORevamp.Data.Entities.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data.Entity.Interface.Entities.Unit;
using IPORevamp.Repository.Interface;

namespace IPORevamp.Repository.Units
{
    public  interface IunitRepository : IAutoDependencyRegister
    {
        #region

        Task<Data.Entity.Interface.Entities.Unit.Units> SaveUnit(Data.Entity.Interface.Entities.Unit.Units unit);
        Task<Data.Entity.Interface.Entities.Unit.Units> UpdateUnit(Data.Entity.Interface.Entities.Unit.Units unit);

        Task<List<Data.Entity.Interface.Entities.Unit.Units>> GetUnits();

        Task<Data.Entity.Interface.Entities.Unit.Units> CheckExistingUnit(string Code);
        Task<Data.Entity.Interface.Entities.Unit.Units> GetUnitById(int product);


        Task<Data.Entity.Interface.Entities.Unit.Units> DeleteUnit(Data.Entity.Interface.Entities.Unit.Units unit);

        #endregion
    }
}
