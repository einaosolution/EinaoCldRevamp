using AutoMapper;
using System.Linq;
using System;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data;
using System.Collections.Generic;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Data.Entities.Modules;

namespace NACC.Web.AutoMapperProfile
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<QuoteModel, PriceQuote>().ReverseMap();
            CreateMap<AuditTrail, AuditVm>();
            CreateMap<EventInfo, EventModel>()
                .ForMember(x => x.RegisteredAttendees, y => { y.MapFrom(x => x.Attendees.Count()); })
                .ForMember(x => x.OrganizerDescription, y => { y.MapFrom(x => x.Organizer.Bio); })
                
                .ForMember(x=>x.Organizer, y => { y.MapFrom(x => string.IsNullOrEmpty(x.Organizer.EmployerName) ? $"{x.Organizer.FirstName} {x.Organizer.LastName}" : x.Organizer.EmployerName); })
            .ReverseMap();

            CreateMap<AttendeeTypeModel, AttendeeType>()
                .ForMember(x=>x.RegistrationFee, y => { y.MapFrom(x => x.RegistrationFee.ToString()); })
                .ReverseMap();

            CreateMap<UserModel, ApplicationUser>().ReverseMap();

            CreateMap<EventFeature, FeaturesModel>().ForMember(x=>x.MachineName, y => { y.MapFrom(x => x.Name.Replace(' ', '_')); }).ReverseMap();

            CreateMap<Attendee, AttendeeViewModel>()
                .ForMember(x=>x.AttendeeTypeId, y=> { y.MapFrom(x => x.AttendeeType.Id); })
                .ForMember(x=>x.AttendeeTypeName, y => { y.MapFrom(x => x.AttendeeType.Name); })
                .ForMember(x=>x.RegistrationBillRefNo,  y => { y.MapFrom(x => x.RegistrationBill.BillRefenceNo); })
                .ForMember(x=>x.EventName, y => { y.MapFrom(x => x.Event.EventName); })

            .ReverseMap();
            
        }
    }
}
