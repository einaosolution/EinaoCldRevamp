using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Repository.Interface;

namespace IPORevamp.Repository.EventFeatures
{
    public class EventFeaturesRepository : IEventFeaturesRepository
    {
        private readonly IRepository<EventFeature> _repository;

        public EventFeaturesRepository(IRepository<EventFeature> repository)
        {
            _repository = repository;
        }

        public async Task DeleteEventFeature(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<EventFeature>> FetchAllEventFeatures()
        {
            var feature = await _repository.GetAllListAsync();
            return feature;
        }

        public List<EventFeature> FetchEventFeatures(int[] featuresId)
        {
            var features = _repository.GetAll().Where(x => featuresId.Contains(x.Id));
            return features.ToList();
        }

        public async Task<EventFeature> GetEventFeature(int id)
        {
            try
            {
                var eventFeature = await _repository.GetAsync(id);
                return eventFeature;
            }catch(Exception)
            {
                return null;
            }
            
        }

        public async Task<EventFeature> SaveEventFeature(EventFeature feature)
        {
            await _repository.InsertOrUpdateAsync(feature);
            await _repository.SaveChangesAsync();

            return feature;
        }
    }
}
