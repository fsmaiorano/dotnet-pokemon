using System.Text.Json.Serialization;

namespace App.Models;
public class GenericResponse<T>
{
    [JsonPropertyName("items")]
    public List<T> Items { get; set; }

    [JsonPropertyName("pageNumber")]
    public int? PageNumber { get; set; }

    [JsonPropertyName("totalPages")]
    public int? TotalPages { get; set; }

    [JsonPropertyName("totalCount")]
    public int? TotalCount { get; set; }

    [JsonPropertyName("previousPage")]
    public object PreviousPage { get; set; }

    [JsonPropertyName("nextPage")]
    public string NextPage { get; set; }

    [JsonPropertyName("hasPreviousPage")]
    public bool? HasPreviousPage { get; set; }

    [JsonPropertyName("hasNextPage")]
    public bool? HasNextPage { get; set; }
}