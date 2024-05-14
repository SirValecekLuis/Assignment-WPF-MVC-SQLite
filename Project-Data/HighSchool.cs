namespace Project_Data;

public class HighSchool
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public HighSchool()
    {
    }

    public HighSchool(long id, string name, string address)
    {
        Id = id;
        Name = name;
        Address = address;
    }
}