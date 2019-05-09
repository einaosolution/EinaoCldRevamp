using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.AutoMapperProfile
{
    public interface IAutoMapperConfiguration
    {
        void Configure(IMapperConfigurationExpression config);
    }
}
