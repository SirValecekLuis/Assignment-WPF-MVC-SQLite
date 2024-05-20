using System.ComponentModel.DataAnnotations;

namespace MVC.Models;

public class SchoolForm
{
    public long? SelectedHighSchoolId { get; set; }
    public List<long>? SelectedStudyProgramIds { get; set; }
}