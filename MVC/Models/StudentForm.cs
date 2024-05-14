using System.ComponentModel.DataAnnotations;
using MVC.Controllers;

namespace MVC.Models;

public class StudentForm
{
    [Required(ErrorMessage = "Jméno a příjmení je vyžadováno.")]
    [Display(Name = "Jméno a příjmení uchazeče: ")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Toto není platná e-mailová adresa.")]
    [EmailAddress]
    [Display(Name = "E-mailová adresa: ")]
    public string Address { get; set; }
    
    [Required(ErrorMessage = "Toto není platné telefonní číslo.")]
    [Phone]
    [MinLength(9, ErrorMessage = "Telefonní číslo musí být délky 9")]
    [MaxLength(9, ErrorMessage = "Telefonní číslo musí být délky 9")]
    [Display(Name = "Telefonní číslo: ")]
    public string PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Rodné číslo je vyžadováno.")]
    [Display(Name = "Rodné číslo: ")]
    [BirthNumber(ErrorMessage = "Rodné číslo musí být ve formátu (xxxxxx/xxxx)")]
    public string BirthNumber { get; set; }
    
}