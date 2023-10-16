using AutoMapper;
using Domain.Entities;

namespace Application.Common.Models;

public class GenericContent
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public GenericContent(string name, string url)
    {
        Name = name;
        Url = url;
        ExternalId = int.Parse(url.Split('/').Reverse().Skip(1).First());
    }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<GenericContent, AbilityEntity>().ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId));
            CreateMap<GenericContent, PokemonEntity>().ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId));
            CreateMap<GenericContent, TypeEntity>().ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId));
            CreateMap<GenericContent, MoveEntity>().ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.ExternalId));
        }
    }
}
