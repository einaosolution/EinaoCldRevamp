using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Core.Utilities;
using IPORevamp.Data;
using IPORevamp.Data.Entities.AuditTrail;



using IPORevamp.Data.UserManagement.Model;


using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using System.Data.SqlClient;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities.Country;
using IPORevamp.Data.Entities.LGAs;
using IPORevamp.Data.Entities;

namespace IPORevamp.Repository.state
{
    public class StateRepository : IStateRepository
    {
        private IRepository<Data.Entities.Country.Country> _countryrepository;

        private IRepository<Data.Entities.State> _staterepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public StateRepository(IRepository<Data.Entities.Country.Country> countryrepository,
            IRepository<Data.Entities.State> staterepository,
            IRepository<Data.Entities.LGAs.LGA> lgarepository,           
            IAuditTrailManager<AuditTrail> auditTrailManager)
        {
            _countryrepository = countryrepository;
            _staterepository = staterepository;
            _auditTrailManager = auditTrailManager;

        }
        
        

        // Get State By Id
        public async Task<Data.Entities.State> GetStateById(int StateId, bool IncludeLGAs)
        {

            Data.Entities.State entity = new Data.Entities.State();
            if (IncludeLGAs == true)
            {
                entity = await _staterepository.GetAll().Include(a => a.LGA).FirstOrDefaultAsync(x => x.Id == StateId);
            }
            else
            {
                entity = await _staterepository.GetAll().FirstOrDefaultAsync(x => x.Id == StateId);
            }
          
            return entity;
        }


        // Get All States 
        public async Task<List<Data.Entities.State>> GetStates()
        {


            List<State> entity = new List<State>();

          //  entity = await _staterepository.GetAllListAsync();
            entity = await _staterepository.GetAll().Include(a => a.Country).ToListAsync(); ;


            return entity;

        }


        

     
        public async Task<Data.Entities.State> GetStateById(int StateId)
        {
            Data.Entities.State country = new Data.Entities.State();
            country = await _staterepository.GetAll().FirstOrDefaultAsync(x => x.Id == StateId);

            return country;
        }
        public async Task<Data.Entities.State> UpdateState(Data.Entities.State state)
        {
            var entity = await _staterepository.UpdateAsync(state);
            await _staterepository.SaveChangesAsync();

            return entity.Entity;
        }


        public async Task<Data.Entities.State> DeleteState(Data.Entities.State entity)
        {
            var saveContent = await _staterepository.UpdateAsync(entity);
            await _staterepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Data.Entities.State> SaveState(Data.Entities.State entity)
        {

                 var saveContent = await _staterepository.InsertAsync(entity);
                                   await _staterepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // Get Country By Name

        public async Task<Data.Entities.State> GetStateByName(string StateName, bool IncludeLGAs)
        {

            Data.Entities.State country = new Data.Entities.State();
            if (IncludeLGAs == true)
            {
                country = await _staterepository.GetAll().Include(a => a.LGA).FirstOrDefaultAsync(x => x.StateName.ToUpper() == StateName.ToUpper());
            }
            else
            {
                country = await _staterepository.GetAll().FirstOrDefaultAsync(x => x.StateName.ToUpper() == StateName.ToUpper());
            }

            return country;
        }

        public async Task<Data.Entities.State> CheckExistingState(string StateName)
        {
            Data.Entities.State content = new Data.Entities.State();
           
                content = await _staterepository.FirstOrDefaultAsync(x => x.StateName.ToUpper() == StateName.ToUpper());
            

            return content;
        }

       
    }
}
