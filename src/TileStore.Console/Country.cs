namespace TileStore;

public class Country
{
    public string Name { get; set; }
    public string Code { get; set; }
    public float Coefficient { get; set; }

    public Country(string name, string code, float coefficient)
    {
        Name = name;
        Code = code;
        Coefficient = coefficient;
    }
}
