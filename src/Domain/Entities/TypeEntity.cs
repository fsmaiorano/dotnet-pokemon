using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

public class TypeEntity : BaseEntity
{
    public int ExternalId { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }
    [JsonIgnore]
    public virtual IList<PokemonEntity>? Pokemons { get; set; }
}
