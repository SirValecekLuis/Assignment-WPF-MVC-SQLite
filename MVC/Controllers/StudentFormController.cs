using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class BirthNumberAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var birthNumber = value?.ToString();
        if (birthNumber == null) return default;

        var r = new Regex("[0-9]{6}\\/[0-9]{4}");
        var match = r.Match(birthNumber);

        return match.Success ? ValidationResult.Success : new ValidationResult(ErrorMessage);
    }
}

public class StudentFormController : Controller
{
    private readonly DatabaseService _databaseService;

    public StudentFormController(DatabaseService databaseService)
    {
        _databaseService = databaseService;
    }


    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(StudentForm studentForm)
    {
        if (!ModelState.IsValid) return View();

        _databaseService.Test();

        Console.WriteLine("Formulář vyplněn správně!");
        return RedirectToAction("Index");
    }
}