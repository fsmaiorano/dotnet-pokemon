namespace Application;

public class PokemonType
{
    public IList<string> Types { get; private set; }

    public PokemonType(IList<string> types)
    {
        Types = types;
    }
}
