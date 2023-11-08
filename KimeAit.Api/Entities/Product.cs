namespace KimeAit.Api.Entities;

public class Product
{
    public int Id { get; set; }

    public string Name { get; private set; }

    public bool? IsHaram { get; private set; }

    public string Origin { get; private set; }

    public string Desc { get; private set; }
}