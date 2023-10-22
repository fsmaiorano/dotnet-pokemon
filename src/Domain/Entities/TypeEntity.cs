namespace Domain.Entities;

public class TypeEntity
{
    public Guid Id { get; set; }
    public int ExternalId { get; set; }
    public required string Name { get; set; }
    public required string Url { get; set; }

    public virtual IList<PokemonEntity>? Pokemons { get; set; }
}
