using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

public class AbilityEntity : BaseEntity
{
    public int ExternalId { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }
    [JsonIgnore]
    public IList<PokemonEntity>? Pokemons { get; set; }
}
