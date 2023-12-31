﻿using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

public class MoveEntity : BaseEntity
{
    public int ExternalId { get; private set; }
    public string Name { get; private set; }
    public string Url { get; private set; }

    [JsonIgnore]
    public virtual IList<PokemonEntity>? Pokemons { get; set; }

    public MoveEntity(int externalId, string name, string url)
    {
        ExternalId = externalId;
        Name = name;
        Url = url;
    }
}
