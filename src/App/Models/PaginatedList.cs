using System.Text.Json.Serialization;

namespace App.Models;

public class PaginatedList<T>
{
    [JsonPropertyName("items")]
    public IReadOnlyCollection<T> Items { get; set; }
    [JsonPropertyName("page_number")]
    public int PageNumber { get; set; }
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
    [JsonPropertyName("previous_page")]
    public string? PreviousPage { get; set; }
    [JsonPropertyName("next_page")]
    public string? NextPage { get; set; }
}
