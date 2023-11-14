namespace KimeAit.Api.Entities;

public class AlternativeProduct : BaseEntity
{
    public Product Product { get; set; }

    public int ProductId { get; set; }

    public string Name { get; set; }

    public string Origin { get; set; }

    public string Desc { get; set; }

    public bool HasHalalCertificate { get; private set; }
}