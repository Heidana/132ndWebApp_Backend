namespace _132ndWebsite.Core.Models;

public class Squadron
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Callsign { get; set; }

    // Parameterless constructor for EF
    public Squadron() { }

    // Constructor with parameters for easier instantiation
    public Squadron(int id, string name, string callsign)
    {
        Id = id;
        Name = name;
        Callsign = callsign;
    }
}
