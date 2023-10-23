﻿// <auto-generated />
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.AbilityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);

                    b.Property<int>("ExternalId")
                        .HasColumnType("integer")
                        .HasColumnName("external_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Abilities", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.MoveEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);

                    b.Property<int>("ExternalId")
                        .HasColumnType("integer")
                        .HasColumnName("external_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Moves", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PokemonDetailEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);

                    b.Property<int>("EvolvesFromPokemonExternalId")
                        .HasColumnType("integer")
                        .HasColumnName("evolves_from_pokemon_external_id");

                    b.Property<int>("ExternalId")
                        .HasColumnType("integer")
                        .HasColumnName("external_id");

                    b.Property<int>("Height")
                        .HasColumnType("integer")
                        .HasColumnName("height");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uuid")
                        .HasColumnName("pokemon_id");

                    b.Property<int>("Weight")
                        .HasColumnType("integer")
                        .HasColumnName("weight");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PokemonId")
                        .IsUnique();

                    b.ToTable("PokemonDetails", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PokemonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);

                    b.Property<int>("ExternalId")
                        .HasColumnType("integer")
                        .HasColumnName("external_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Pokemons", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.SpriteEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);

                    b.Property<string>("BackDefault")
                        .HasColumnType("text")
                        .HasColumnName("back_default");

                    b.Property<string>("BackFemale")
                        .HasColumnType("text")
                        .HasColumnName("back_female");

                    b.Property<string>("BackShiny")
                        .HasColumnType("text")
                        .HasColumnName("back_shiny");

                    b.Property<string>("BackShinyFemale")
                        .HasColumnType("text")
                        .HasColumnName("back_shiny_female");

                    b.Property<string>("DreamWorldFrontDefault")
                        .HasColumnType("text")
                        .HasColumnName("dream_world_front_default");

                    b.Property<string>("DreamWorldFrontFemale")
                        .HasColumnType("text")
                        .HasColumnName("dream_world_front_female");

                    b.Property<int>("ExternalId")
                        .HasColumnType("integer")
                        .HasColumnName("external_id");

                    b.Property<string>("FrontDefault")
                        .HasColumnType("text")
                        .HasColumnName("front_default");

                    b.Property<string>("FrontFemale")
                        .HasColumnType("text")
                        .HasColumnName("front_female");

                    b.Property<string>("FrontShiny")
                        .HasColumnType("text")
                        .HasColumnName("front_shiny");

                    b.Property<string>("FrontShinyFemale")
                        .HasColumnType("text")
                        .HasColumnName("front_shiny_female");

                    b.Property<string>("HomeFrontDefault")
                        .HasColumnType("text")
                        .HasColumnName("home_front_default");

                    b.Property<string>("HomeFrontFemale")
                        .HasColumnType("text")
                        .HasColumnName("home_front_female");

                    b.Property<string>("HomeFrontShiny")
                        .HasColumnType("text")
                        .HasColumnName("home_front_shiny");

                    b.Property<string>("HomeFrontShinyFemale")
                        .HasColumnType("text")
                        .HasColumnName("home_front_shiny_female");

                    b.Property<string>("OfficialArtworkFrontDefault")
                        .HasColumnType("text")
                        .HasColumnName("official_artwork_front_default");

                    b.Property<string>("OfficialArtworkFrontShiny")
                        .HasColumnType("text")
                        .HasColumnName("official_artwork_front_shiny");

                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uuid")
                        .HasColumnName("pokemon_id");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("PokemonId")
                        .IsUnique();

                    b.ToTable("Sprites", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TypeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ExternalId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("PokemonAbilities", b =>
                {
                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AbilityId")
                        .HasColumnType("uuid");

                    b.HasKey("PokemonId", "AbilityId");

                    b.HasIndex("AbilityId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonAbilities", (string)null);
                });

            modelBuilder.Entity("PokemonMoves", b =>
                {
                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MoveId")
                        .HasColumnType("uuid");

                    b.HasKey("PokemonId", "MoveId");

                    b.HasIndex("MoveId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonMoves", (string)null);
                });

            modelBuilder.Entity("PokemonTypes", b =>
                {
                    b.Property<Guid>("PokemonId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.HasKey("PokemonId", "TypeId");

                    b.HasIndex("PokemonId");

                    b.HasIndex("TypeId");

                    b.ToTable("PokemonTypes", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.PokemonDetailEntity", b =>
                {
                    b.HasOne("Domain.Entities.PokemonEntity", "Pokemon")
                        .WithOne("PokemonDetail")
                        .HasForeignKey("Domain.Entities.PokemonDetailEntity", "PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PokemonDetails_Pokemons_PokemonId");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("Domain.Entities.SpriteEntity", b =>
                {
                    b.HasOne("Domain.Entities.PokemonEntity", "Pokemon")
                        .WithOne("Sprites")
                        .HasForeignKey("Domain.Entities.SpriteEntity", "PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Sprites_Pokemons_PokemonId");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("PokemonAbilities", b =>
                {
                    b.HasOne("Domain.Entities.AbilityEntity", null)
                        .WithMany()
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PokemonAbilities_Abilities_AbilityId");

                    b.HasOne("Domain.Entities.PokemonEntity", null)
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PokemonAbilities_Pokemons_PokemonId");
                });

            modelBuilder.Entity("PokemonMoves", b =>
                {
                    b.HasOne("Domain.Entities.MoveEntity", null)
                        .WithMany()
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PokemonMoves_Moves_MoveId");

                    b.HasOne("Domain.Entities.PokemonEntity", null)
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PokemonMoves_Pokemons_PokemonId");
                });

            modelBuilder.Entity("PokemonTypes", b =>
                {
                    b.HasOne("Domain.Entities.PokemonEntity", null)
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PokemonTypes_Pokemons_PokemonId");

                    b.HasOne("Domain.Entities.TypeEntity", null)
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PokemonTypes_Types_TypeId");
                });

            modelBuilder.Entity("Domain.Entities.PokemonEntity", b =>
                {
                    b.Navigation("PokemonDetail");

                    b.Navigation("Sprites");
                });
#pragma warning restore 612, 618
        }
    }
}
