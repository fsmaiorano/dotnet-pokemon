using Domain.Common;

namespace Domain.Entities;

public class PokemonDetailEntity : BaseEntity
{
    public Guid PokemonId { get; set; }
    public virtual PokemonEntity? Pokemon { get; set; }
    public int ExternalId { get; private set; }
    public int Height { get; private set; }
    public int Weight { get; private set; }
    public int EvolvesFromPokemonExternalId { get; private set; }

}
