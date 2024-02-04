using AutoMapper;
using WMS.Domain;

namespace WMS.Application;

public class EngineProfile : Profile
{
    public EngineProfile()
    {
        CreateMap<Engine, EngineDto>();
    }
}
