using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace IPORevamp.WebAPI.Filters
{
    public class EventUserRequirement:IAuthorizationRequirement
    {
        public string EventUser { get;}

        public EventUserRequirement(string eventUser)
        {
            EventUser = eventUser;
        }
    }
}