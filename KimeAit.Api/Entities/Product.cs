namespace KimeAit.Api.Entities;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public bool? IsHaram { get; set; }

    public string Origin { get; set; }

    public string Desc { get; set; }

    public bool IsApproved { get; private set; }
}