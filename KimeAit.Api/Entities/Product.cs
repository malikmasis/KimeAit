namespace KimeAit.Api.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public bool? IsHaram { get; set; }

    public string Origin { get; set; }

    public string Desc { get; set; }

    public bool IsApproved { get; private set; }

    public IReadOnlyCollection<AlternativeProduct> AlternativeProducts { get; set; }
}