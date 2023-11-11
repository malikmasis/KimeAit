namespace KimeAit.Api.Models;

public sealed record ProductRequestModel
{
    public string Name { get; init; }

    public bool? IsHaram { get; init; }

    public string Origin { get; init; }

    public string Desc { get; init; }
}