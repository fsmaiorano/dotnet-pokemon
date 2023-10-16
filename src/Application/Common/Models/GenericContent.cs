using Domain.Common;

namespace Application;

public class GenericContent : BaseEntity
{
    public int ExternalId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public GenericContent(int externalId, string name, string url)
    {
        ExternalId = externalId;
        Name = name;
        Url = url;
    }
}
