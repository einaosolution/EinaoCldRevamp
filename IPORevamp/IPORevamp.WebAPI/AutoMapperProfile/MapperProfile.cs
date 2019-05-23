using AutoMapper;
using System.Linq;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data;
using IPORevamp.Data.UserManagement.Model;

namespace NACC.Web.AutoMapperProfile
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<QuoteModel, PriceQuote>().ReverseMap();
            CreateMap<AuditTrail, AuditVm>()
                
            .ReverseMap();


            CreateMap<UserModel, ApplicationUser>().ReverseMap()

            .ReverseMap();
            
        }
    }
}
