using IPORevamp.Repository.Interface;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace IPORevamp.Repository.PreliminarySearch
{
    class preliminarySearchRepository : IpreliminarySearchRepository
    {
        private IRepository<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> _preliminarySearch;

        public preliminarySearchRepository(IRepository<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> preliminarySearch)
        {
            _preliminarySearch = preliminarySearch;


        }

        public async Task<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> GetPrelimById(int prelimId)
        {
            IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch prelim = new IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch();
            prelim = await _preliminarySearch.GetAll().FirstOrDefaultAsync(x => x.Id == prelimId);

            return prelim;
        }

        public async Task<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> UpdatePreliminary(IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch prelimsearch)
        {
            var saveContent = await _preliminarySearch.UpdateAsync(prelimsearch);
            await _preliminarySearch.SaveChangesAsync();

            return saveContent.Entity;
        }



        public async Task<List<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch>> GetPreliminarySearch()
        {

         
            List<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> prelimSearch = new List<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch>();
            //  prelimSearch = await _preliminarySearch.Include(a => a.LGA).GetAllListAsync(x => x.status == "Submitted"); ;

            prelimSearch = await _preliminarySearch.GetAll().Include(a => a.Sector).Where(x => x.status == "Submitted").ToListAsync();
            return prelimSearch;
        }

        public async Task<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> SavePreliminary(IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch preliminarySearch)
        {

            var saveContent = await _preliminarySearch.InsertAsync(preliminarySearch);
            await _preliminarySearch.SaveChangesAsync();

            return saveContent.Entity;
        }



    }
}
