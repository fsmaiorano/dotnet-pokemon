CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Abilities" (
    id uuid NOT NULL,
    external_id integer NOT NULL,
    name text NOT NULL,
    url text NOT NULL,
    CONSTRAINT "PK_Abilities" PRIMARY KEY (id)
);

CREATE TABLE "Moves" (
    id uuid NOT NULL,
    external_id integer NOT NULL,
    name text NOT NULL,
    url text NOT NULL,
    CONSTRAINT "PK_Moves" PRIMARY KEY (id)
);

CREATE TABLE "Pokemons" (
    id uuid NOT NULL,
    external_id integer NOT NULL,
    name text NOT NULL,
    url text NOT NULL,
    CONSTRAINT "PK_Pokemons" PRIMARY KEY (id)
);

CREATE TABLE "Types" (
    "Id" uuid NOT NULL,
    "ExternalId" integer NOT NULL,
    "Name" text NOT NULL,
    "Url" text NOT NULL,
    CONSTRAINT "PK_Types" PRIMARY KEY ("Id")
);

CREATE TABLE "PokemonAbilities" (
    "PokemonId" uuid NOT NULL,
    "AbilityId" uuid NOT NULL,
    CONSTRAINT "PK_PokemonAbilities" PRIMARY KEY ("PokemonId", "AbilityId"),
    CONSTRAINT "FK_PokemonAbilities_Abilities_AbilityId" FOREIGN KEY ("AbilityId") REFERENCES "Abilities" (id) ON DELETE CASCADE,
    CONSTRAINT "FK_PokemonAbilities_Pokemons_PokemonId" FOREIGN KEY ("PokemonId") REFERENCES "Pokemons" (id) ON DELETE CASCADE
);

CREATE TABLE "PokemonDetails" (
    id uuid NOT NULL,
    pokemon_id uuid NOT NULL,
    external_id integer NOT NULL,
    height integer NOT NULL,
    weight integer NOT NULL,
    evolves_from_pokemon_external_id integer NOT NULL,
    CONSTRAINT "PK_PokemonDetails" PRIMARY KEY (id),
    CONSTRAINT "FK_PokemonDetails_Pokemons_PokemonId" FOREIGN KEY (pokemon_id) REFERENCES "Pokemons" (id) ON DELETE CASCADE
);

CREATE TABLE "PokemonMoves" (
    "PokemonId" uuid NOT NULL,
    "MoveId" uuid NOT NULL,
    CONSTRAINT "PK_PokemonMoves" PRIMARY KEY ("PokemonId", "MoveId"),
    CONSTRAINT "FK_PokemonMoves_Moves_MoveId" FOREIGN KEY ("MoveId") REFERENCES "Moves" (id) ON DELETE CASCADE,
    CONSTRAINT "FK_PokemonMoves_Pokemons_PokemonId" FOREIGN KEY ("PokemonId") REFERENCES "Pokemons" (id) ON DELETE CASCADE
);

CREATE TABLE "Sprites" (
    id uuid NOT NULL,
    pokemon_id uuid NOT NULL,
    external_id integer NOT NULL,
    back_default text,
    back_female text,
    front_default text,
    front_female text,
    back_shiny text,
    back_shiny_female text,
    front_shiny text,
    front_shiny_female text,
    dream_world_front_default text,
    dream_world_front_female text,
    home_front_default text,
    home_front_female text,
    home_front_shiny text,
    home_front_shiny_female text,
    official_artwork_front_default text,
    official_artwork_front_shiny text,
    CONSTRAINT "PK_Sprites" PRIMARY KEY (id),
    CONSTRAINT "FK_Sprites_Pokemons_PokemonId" FOREIGN KEY (pokemon_id) REFERENCES "Pokemons" (id) ON DELETE CASCADE
);

CREATE TABLE "PokemonTypes" (
    "PokemonId" uuid NOT NULL,
    "TypeId" uuid NOT NULL,
    CONSTRAINT "PK_PokemonTypes" PRIMARY KEY ("PokemonId", "TypeId"),
    CONSTRAINT "FK_PokemonTypes_Pokemons_PokemonId" FOREIGN KEY ("PokemonId") REFERENCES "Pokemons" (id) ON DELETE CASCADE,
    CONSTRAINT "FK_PokemonTypes_Types_TypeId" FOREIGN KEY ("TypeId") REFERENCES "Types" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Abilities_id" ON "Abilities" (id);

CREATE INDEX "IX_Moves_id" ON "Moves" (id);

CREATE INDEX "IX_PokemonAbilities_AbilityId" ON "PokemonAbilities" ("AbilityId");

CREATE INDEX "IX_PokemonAbilities_PokemonId" ON "PokemonAbilities" ("PokemonId");

CREATE INDEX "IX_PokemonDetails_id" ON "PokemonDetails" (id);

CREATE UNIQUE INDEX "IX_PokemonDetails_pokemon_id" ON "PokemonDetails" (pokemon_id);

CREATE INDEX "IX_PokemonMoves_MoveId" ON "PokemonMoves" ("MoveId");

CREATE INDEX "IX_PokemonMoves_PokemonId" ON "PokemonMoves" ("PokemonId");

CREATE INDEX "IX_Pokemons_id" ON "Pokemons" (id);

CREATE INDEX "IX_PokemonTypes_PokemonId" ON "PokemonTypes" ("PokemonId");

CREATE INDEX "IX_PokemonTypes_TypeId" ON "PokemonTypes" ("TypeId");

CREATE INDEX "IX_Sprites_id" ON "Sprites" (id);

CREATE UNIQUE INDEX "IX_Sprites_pokemon_id" ON "Sprites" (pokemon_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20231023161204_Init', '8.0.0');

COMMIT;