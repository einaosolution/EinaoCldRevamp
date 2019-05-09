using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IPORevamp.WebAPI.Models;

namespace IPORevamp.WebAPI.Filters
{
    public sealed class AttendeeAuthorizer : AuthorizationHandler<EventUserRequirement>
    {

        private readonly IActionContextAccessor _accessor;

        public AttendeeAuthorizer(IActionContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EventUserRequirement requirement)
        {
            if(requirement.EventUser == "EventAttendees")
            {
                var eventClaim = context.User.Claims.FirstOrDefault(x => x.Type == "EVENTID" && x.Type == "INVCODE" && x.Type == "ATTENDEEID");
                if (eventClaim != null)
                {
                    if (eventClaim.Value == _accessor.ActionContext.RouteData.Values["eventId"].ToString())
                    {                           
                        context.Succeed(requirement);
                    }
                }
            }

            if (requirement.EventUser == "Organizer")
            {
                var eventClaim = context.User.Claims.FirstOrDefault(x => x.Type == "OrganizedEvents");
                if(eventClaim != null)
                {
                    var organizedEvents = eventClaim.Value.Split(',');
                    string eventId = _accessor.ActionContext.RouteData.Values["eventId"].ToString();
                    if (organizedEvents.Contains(eventId))
                    {
                        context.Succeed(requirement);
                    }
                }
                    
                
            }

            return Task.CompletedTask;
        }
    }
}
