using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Department
{
   class DepartmentRepository : IDepartmentRepository
    {
        private IRepository<Data.Entity.Interface.Entities.Department.Department> _departmentrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public DepartmentRepository(IRepository<Data.Entity.Interface.Entities.Department.Department> departmentrepository)
        {
            _departmentrepository = departmentrepository;


        }


       


        public async Task<Data.Entity.Interface.Entities.Department.Department> GetDepartmentById(int ProdId)
        {
            Data.Entity.Interface.Entities.Department.Department  department = new Data.Entity.Interface.Entities.Department.Department();
            department = await _departmentrepository.FirstOrDefaultAsync(x => x.Id == ProdId);

            return department;
        }

        public async Task<Data.Entity.Interface.Entities.Department.Department> CheckExistingDepartment(string Code)
        {
            Data.Entity.Interface.Entities.Department.Department  department = new Data.Entity.Interface.Entities.Department.Department();

            department = await _departmentrepository.FirstOrDefaultAsync(x => x.Code.ToUpper() == Code.ToUpper());


            return department;
        }

        public async Task<Data.Entity.Interface.Entities.Department.Department> SaveDepartment(Data.Entity.Interface.Entities.Department.Department department)
        {

            var saveContent = await _departmentrepository.InsertAsync(department);
            await _departmentrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entity.Interface.Entities.Department.Department> UpdateDepartment(Data.Entity.Interface.Entities.Department.Department department)
        {

            var saveContent = await _departmentrepository.UpdateAsync(department);
            await _departmentrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<List<Data.Entity.Interface.Entities.Department.Department>> GetDepartment()
        {

            List<Data.Entity.Interface.Entities.Department.Department> department = new List<Data.Entity.Interface.Entities.Department.Department>();
            department = await _departmentrepository.GetAllListAsync();
            return department;
        }


        public async Task<Data.Entity.Interface.Entities.Department.Department> DeleteDepartment(Data.Entity.Interface.Entities.Department.Department department)
        {
            var saveContent = await _departmentrepository.UpdateAsync(department);
            await _departmentrepository.SaveChangesAsync();

            return saveContent.Entity;
        }
    }
}
