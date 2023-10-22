using Domain.Common;

namespace Domain.Entities;

public class PokemonDetailEntity : BaseEntity
{
    public Guid PokemonId { get; set; }
    public virtual PokemonEntity? Pokemon { get; set; }
    public int ExternalId { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int EvolvesFromPokemonExternalId { get; set; }

}
