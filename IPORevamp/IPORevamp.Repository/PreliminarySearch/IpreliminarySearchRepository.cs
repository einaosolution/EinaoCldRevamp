using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.PreliminarySearch
{
    public interface IpreliminarySearchRepository : IAutoDependencyRegister
    {

        Task<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> SavePreliminary(IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch preliminarySearch);
       

         Task<List<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch>> GetPreliminarySearch();

        Task<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> GetPrelimById(int prelimid);

        Task<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> UpdatePreliminary(IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch prelim);

    }
}
