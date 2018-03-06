using System;
using AutoMapper;

namespace UnityContainerFilterAttribute.Infrastructure
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Guid, string>().ConvertUsing(c => c.ToString());
            CreateMap<int, string>().ConvertUsing(c => c.ToString());
            CreateMap<string, int>().ConvertUsing(int.Parse);
        }
    }
}