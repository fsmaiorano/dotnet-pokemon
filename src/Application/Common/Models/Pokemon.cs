namespace Application.Common.Models;

public class Pokemon
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public int? EvolvesFrom { get; set; }
    public PokemonSprite? Sprites { get; set; }
    public List<PokemonType>? Types { get; set; }
}
