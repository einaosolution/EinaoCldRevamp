using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data.Entities.Modules;

namespace IPORevamp.Repository.EventFeatures
{
    public interface IEventFeaturesRepository: IAutoDependencyRegister
    {
        List<EventFeature> FetchEventFeatures(int[] featuresId);
        Task<List<EventFeature>> FetchAllEventFeatures();
        Task<EventFeature> SaveEventFeature(EventFeature feature);
        Task<EventFeature> GetEventFeature(int id);
        Task DeleteEventFeature(int id);

    }
}
