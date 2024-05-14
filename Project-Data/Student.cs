namespace Project_Data;

public class Student
{
    public long Id { get; set; }
    public string Name { get; set; }

    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string BirthNumber { get; set; }
    public long ApplicationId { get; set; }

    public Student()
    {
    }

    public Student(long id, string name, string address, string phoneNumber, string birthNumber, long applicationId)
    {
        Id = id;
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        BirthNumber = birthNumber;
        ApplicationId = applicationId;
    }
}