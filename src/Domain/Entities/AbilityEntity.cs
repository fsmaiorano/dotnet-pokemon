using Domain.Common;

namespace Domain.Entities;

public class AbilityEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int ExternalId { get; private set; }
    public string Name { get; private set; }
    public string Url { get; private set; }

    public virtual IList<PokemonEntity>? Pokemons { get; set; }

    public AbilityEntity(int externalId, string name, string url)
    {
        ExternalId = externalId;
        Name = name;
        Url = url;
    }
}
