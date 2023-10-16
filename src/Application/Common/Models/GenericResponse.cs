namespace Application.Common.Models;
public class GenericResponse
{
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
    public List<GenericContent> Results { get; set; }

    public GenericResponse()
    {
        Results = new List<GenericContent>();
    }
}