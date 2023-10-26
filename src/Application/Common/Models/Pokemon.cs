using AutoMapper;
using Domain.Entities;
using Google.Cloud.Firestore;

namespace Application.Common.Models;

public class Pokemon : IFirestoreConverter<Pokemon>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int? EvolvesFrom { get; set; }
    public PokemonSprite? Sprites { get; set; }
    public List<PokemonType>? Types { get; set; }

    public Pokemon FromFirestore(object value)
    {
        var pokemon = (Dictionary<string, object>)value;

        return new Pokemon
        {
            Id = Convert.ToInt32(pokemon["Id"]),
            Name = pokemon["Name"]?.ToString(),
            Height = Convert.ToInt32(pokemon["Height"]),
            Weight = Convert.ToInt32(pokemon["Weight"]),
            EvolvesFrom = Convert.ToInt32(pokemon["EvolvesFrom"]),
            Sprites = new PokemonSprite
            {
                BackDefault = pokemon["Sprites.BackDefault"]?.ToString(),
                BackFemale = pokemon["Sprites.BackFemale"]?.ToString(),
                BackShiny = pokemon["Sprites.BackShiny"]?.ToString(),
                BackShinyFemale = pokemon["Sprites.BackShinyFemale"]?.ToString(),
                FrontDefault = pokemon["Sprites.FrontDefault"]?.ToString(),
                FrontFemale = pokemon["Sprites.FrontFemale"]?.ToString(),
                FrontShiny = pokemon["Sprites.FrontShiny"]?.ToString(),
                FrontShinyFemale = pokemon["Sprites.FrontShinyFemale"]?.ToString(),
                Others = new PokemonSprite.Other
                {
                    DreamWorld = new PokemonSprite.DreamWorld
                    {
                        FrontDefault = pokemon["Sprites.Others.DreamWorld.FrontDefault"]?.ToString(),
                        FrontFemale = pokemon["Sprites.Others.DreamWorld.FrontFemale"]?.ToString()
                    },
                    Home = new PokemonSprite.Home
                    {
                        FrontDefault = pokemon["Sprites.Others.Home.FrontDefault"]?.ToString(),
                        FrontFemale = pokemon["Sprites.Others.Home.FrontFemale"]?.ToString(),
                        FrontShiny = pokemon["Sprites.Others.Home.FrontShiny"]?.ToString(),
                        FrontShinyFemale = pokemon["Sprites.Others.Home.FrontShinyFemale"]?.ToString()
                    },
                    OfficialArtwork = new PokemonSprite.OfficialArtwork
                    {
                        FrontDefault = pokemon["Sprites.Others.OfficialArtwork.FrontDefault"]?.ToString(),
                        FrontShiny = pokemon["Sprites.Others.OfficialArtwork.FrontShiny"]?.ToString()
                    }
                }
            },
            Types = new List<PokemonType>
            {
                new PokemonType
                {
                    Slot = Convert.ToInt32(pokemon["Types.Slot"]),
                    Type = new PokemonType.TypeObject
                    {
                        Name = pokemon["Types.Type.Name"]?.ToString(),
                        Url = pokemon["Types.Type.Url"]?.ToString(),
                    }
                }
            }
        };

    }

    public object ToFirestore(Pokemon value)
    {
        return new Dictionary<string, object>
     {
         { "Id", value.Id },
         { "Name", value.Name! },
         { "Height", value.Height },
         { "Weight", value.Weight },
         { "EvolvesFrom", value.EvolvesFrom ?? 0 },
         { "Sprites", new Dictionary<string, object>
             {
                 { "BackDefault", value.Sprites?.BackDefault ?? "" },
                 { "BackFemale", value.Sprites?.BackFemale  ?? ""},
                 { "BackShiny", value.Sprites?.BackShiny  ?? ""},
                 { "BackShinyFemale", value.Sprites?.BackShinyFemale  ?? ""},
                 { "FrontDefault", value.Sprites?.FrontDefault  ?? ""},
                 { "FrontFemale", value.Sprites?.FrontFemale  ?? ""},
                 { "FrontShiny", value.Sprites?.FrontShiny  ?? ""},
                 { "FrontShinyFemale", value.Sprites?.FrontShinyFemale  ?? ""},
                 { "DreamWorldFrontDefault", value.Sprites?.Others?.DreamWorld?.FrontDefault  ?? ""},
                    { "DreamWorldFrontFemale", value.Sprites?.Others?.DreamWorld?.FrontFemale  ?? ""},
                    { "HomeFrontDefault", value.Sprites?.Others?.Home?.FrontDefault  ?? ""},
                    { "HomeFrontFemale", value.Sprites?.Others?.Home?.FrontFemale  ?? ""},
                    { "HomeFrontShiny", value.Sprites?.Others?.Home?.FrontShiny  ?? ""},
                    { "HomeFrontShinyFemale", value.Sprites?.Others?.Home?.FrontShinyFemale  ?? ""},
                    { "OfficialArtworkFrontDefault", value.Sprites?.Others?.OfficialArtwork?.FrontDefault  ?? ""},
                    { "OfficialArtworkFrontShiny", value.Sprites?.Others?.OfficialArtwork?.FrontShiny  ?? ""}

             }
         },
            { "Types", value.Types?.Select(t => new Dictionary<string, object>
                {
                    { "Slot", t.Slot },
                    { "Type", new Dictionary<string, object>
                        {
                            { "Name", t.Type?.Name ?? ""},
                            { "Url", t.Type?.Url ?? "" }
                        }
                    }
                }).ToList() ?? new List<Dictionary<string, object>>()
            }
        };
    }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PaginatedList<Pokemon>, PaginatedList<PokemonEntity>>().ReverseMap();
            CreateMap<Pokemon, PokemonEntity>().ReverseMap()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ExternalId))
                    .ForMember(dest => dest.Sprites, opt => opt.MapFrom(src => src.Sprites))
                    .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types));
        }
    }
}