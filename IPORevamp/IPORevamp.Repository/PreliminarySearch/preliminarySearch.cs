using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.PreliminarySearch
{
    class preliminarySearch : IpreliminarySearch
    {
        private IRepository<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> _preliminarySearch;

        public preliminarySearch(IRepository<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> preliminarySearch)
        {
            _preliminarySearch = preliminarySearch;


        }


        public async Task<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> SaveUnit(IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch preliminarySearch)
        {

            var saveContent = await _preliminarySearch.InsertAsync(preliminarySearch);
            await _preliminarySearch.SaveChangesAsync();

            return saveContent.Entity;
        }



    }
}
