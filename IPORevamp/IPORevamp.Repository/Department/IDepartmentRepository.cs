
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;


namespace IPORevamp.Repository.Department
{
 public    interface IDepartmentRepository : IAutoDependencyRegister
    {
        #region Department Respository


        Task<Data.Entity.Interface.Entities.Department.Department> SaveDepartment(Data.Entity.Interface.Entities.Department.Department department);
        Task<Data.Entity.Interface.Entities.Department.Department> UpdateDepartment(Data.Entity.Interface.Entities.Department.Department department);

        Task<List<Data.Entity.Interface.Entities.Department.Department>> GetDepartment();

        Task<Data.Entity.Interface.Entities.Department.Department> CheckExistingDepartment(string Code);
        Task<Data.Entity.Interface.Entities.Department.Department> GetDepartmentById(int product);


        Task<Data.Entity.Interface.Entities.Department.Department> DeleteDepartment(Data.Entity.Interface.Entities.Department.Department department);
        #endregion
    }
}
