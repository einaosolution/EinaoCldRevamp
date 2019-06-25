using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.PreliminarySearch
{
    public interface IpreliminarySearch : IAutoDependencyRegister
    {

        Task<IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch> SaveUnit(IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearch preliminarySearch);

    }
}
