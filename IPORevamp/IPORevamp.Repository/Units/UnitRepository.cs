using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IPORevamp.Repository.Units
{
   class UnitRepository : IunitRepository
    {
        private IRepository<Data.Entity.Interface.Entities.Unit.Units> _unitrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public UnitRepository(IRepository<Data.Entity.Interface.Entities.Unit.Units> unitrepository)
        {
            _unitrepository = unitrepository;


        }





        public async Task<Data.Entity.Interface.Entities.Unit.Units> GetUnitById(int ProdId)
        {
            Data.Entity.Interface.Entities.Unit.Units  unit = new Data.Entity.Interface.Entities.Unit.Units();
            unit = await _unitrepository.FirstOrDefaultAsync(x => x.Id == ProdId);

            return unit;
        }

        public async Task<Data.Entity.Interface.Entities.Unit.Units> CheckExistingUnit(string   Code)
        {
            Data.Entity.Interface.Entities.Unit.Units unit = new Data.Entity.Interface.Entities.Unit.Units();

            unit = await _unitrepository.FirstOrDefaultAsync(x => x.Description.ToUpper() == Code.ToUpper());


            return unit;
        }

        public async Task<Data.Entity.Interface.Entities.Unit.Units> SaveUnit(Data.Entity.Interface.Entities.Unit.Units unit)
        {

            var saveContent = await _unitrepository.InsertAsync(unit);
            await _unitrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entity.Interface.Entities.Unit.Units> UpdateUnit(Data.Entity.Interface.Entities.Unit.Units unit)
        {

            var saveContent = await _unitrepository.UpdateAsync(unit);
            await _unitrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<List<Data.Entity.Interface.Entities.Unit.Units>> GetUnits()
        {

            List<Data.Entity.Interface.Entities.Unit.Units> unit = new List<Data.Entity.Interface.Entities.Unit.Units>();
            unit = await _unitrepository.GetAll().Include(a => a.Department).ToListAsync();
          //  unit  = await _unitrepository.GetAllListAsync().;
            return unit;
        }


        public async Task<Data.Entity.Interface.Entities.Unit.Units> DeleteUnit(Data.Entity.Interface.Entities.Unit.Units unit)
        {
            var saveContent = await _unitrepository.UpdateAsync(unit);
            await _unitrepository.SaveChangesAsync();

            return saveContent.Entity;
        }
    }
}

