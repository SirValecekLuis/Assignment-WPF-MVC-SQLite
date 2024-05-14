namespace Project_Data;

public class Application
{
    public long Id { get; set; }
    public DateTime Date { get; set; }

    public Application()
    {
    }

    public Application(long id, DateTime date)
    {
        Id = id;
        Date = date;
    }
}