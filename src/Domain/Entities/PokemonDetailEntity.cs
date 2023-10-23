using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

public class PokemonDetailEntity : BaseEntity
{
    [JsonIgnore]
    public Guid PokemonId { get; set; }
    [JsonIgnore]
    public virtual PokemonEntity? Pokemon { get; set; }
    public int ExternalId { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int EvolvesFromPokemonExternalId { get; set; }
}
