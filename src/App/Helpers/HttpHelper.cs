using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace App.Helpers;

public static class HttpHelper
{
    public static async Task<T?> GetAsync<T>(string url)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                };

                var content = await response.Content.ReadAsStringAsync() ?? string.Empty;

                if (string.IsNullOrEmpty(content))
                    return default;

                return JsonSerializer.Deserialize<T>(content, jsonOptions) ?? default;
            }

            return default;
        }
        catch
        {
            throw;
        }
    }

    public static async Task<T?> PostAsync<T>(string url, object? data)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.PostAsync(url, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var content = await response.Content.ReadAsStringAsync() ?? string.Empty;

                if (string.IsNullOrEmpty(content))
                    return default;

                return JsonSerializer.Deserialize<T>(content, jsonOptions) ?? default;
            }

            return default;
        }
        catch
        {
            throw;
        }

    }

    public static async Task<T?> PutAsync<T>(string url, object? data)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.PutAsync(url, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var content = await response.Content.ReadAsStringAsync() ?? string.Empty;

                if (string.IsNullOrEmpty(content))
                    return default;

                return JsonSerializer.Deserialize<T>(content, jsonOptions) ?? default;
            }

            return default;
        }
        catch
        {
            throw;
        }
    }

    public static async Task<T?> DeleteAsync<T>(string url)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var content = await response.Content.ReadAsStringAsync() ?? string.Empty;

                if (string.IsNullOrEmpty(content))
                    return default;

                return JsonSerializer.Deserialize<T>(content, jsonOptions) ?? default;
            }

            return default;
        }
        catch
        {
            throw;
        }
    }
}