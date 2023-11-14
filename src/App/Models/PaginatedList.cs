namespace App.Models;

public class PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public string? PreviousPage { get; set; }
    public string? NextPage { get; set; }
}
