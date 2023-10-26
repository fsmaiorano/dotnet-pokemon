using System.Text.Json;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Google.Cloud.Firestore;

namespace Infrastructure.Firestore;

public class FirestoreContext : IFirestoreContext
{
    private readonly FirestoreDb _db;

    public FirestoreContext()
    {
        string filename = "pokemon-5d230-firebase-adminsdk-mfdyn-7ed3f9c0e2.json";
        var path = $"{GetSolutionPath().FullName}/{filename}";

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

        var jsonString = File.ReadAllText(path);

        _db = new FirestoreDbBuilder
        {
            ProjectId = "pokemon-5d230",
            JsonCredentials = jsonString
        }.Build();
    }

    public async Task<Pokemon> GetPokemon(string name)
    {
        try
        {
            var doc = await _db.Collection("pokemons").Document(name).GetSnapshotAsync();

            if (!doc.Exists)
                return null!;

            var pokemon = doc.ConvertTo<Pokemon>();
            return pokemon;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task SavePokemon(Pokemon pokemon)
    {
        try
        {
            var doc = await _db.Collection("pokemons").Document(pokemon.Name).GetSnapshotAsync();

            if (doc.Exists)
                return;

            await _db.Collection("pokemons").Document(pokemon.Name).SetAsync(pokemon.ToFirestore(pokemon));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task SavePokemons(List<Pokemon> pokemons)
    {
        try
        {
            foreach (var pokemon in pokemons)
            {
                var doc = await _db.Collection("pokemons").Document(pokemon.Name).GetSnapshotAsync();

                if (doc.Exists)
                    continue;

                await _db.Collection("pokemons").Document(pokemon.Name).SetAsync(pokemon.ToFirestore(pokemon));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    internal static DirectoryInfo GetSolutionPath(string currentPath = null!)
    {
        var directory = new DirectoryInfo(
            currentPath ?? Directory.GetCurrentDirectory());
        while (directory != null && !directory.GetFiles("*.sln").Any())
        {
            directory = directory.Parent;
        }
        return directory!;
    }
}