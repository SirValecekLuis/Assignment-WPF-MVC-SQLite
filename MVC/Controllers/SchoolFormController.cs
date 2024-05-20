using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using Project_Data;

namespace MVC.Controllers
{
    public class SchoolFormController : Controller
    {
        private readonly DatabaseService _databaseService;

        private static List<HighSchool>? HighSchools { get; set; }
        private static List<StudyProgram>? StudyPrograms { get; set; }

        public SchoolFormController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            HighSchools = _databaseService.MyDatabase.GetObjectsFromDb<HighSchool>();
            StudyPrograms = _databaseService.MyDatabase.GetObjectsFromDb<StudyProgram>();
        }

        public IActionResult FormSent()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SchoolForm schoolForm)
        {
            if (schoolForm.SelectedStudyProgramIds != null && (schoolForm.SelectedHighSchoolId == null ||
                                                               schoolForm.SelectedStudyProgramIds.Count == 0) ||
                schoolForm.SelectedStudyProgramIds.GroupBy(x => x).Any(g => g.Count() > 1))
            {
                Console.WriteLine("Neplatné");
                return View();
            }

            if (TempData["StudentForm"] == null) return RedirectToAction("Index");

            var serializedForm = TempData["StudentForm"]?.ToString();
            if (serializedForm == null) return RedirectToAction("Index");

            var form = JsonConvert.DeserializeObject<StudentForm>(serializedForm);

            if (form == null)
            {
                return View();
            }

            Application app = new();
            var appId = _databaseService.MyDatabase.GetNextIdFromDb<Application>();
            app.Id = appId;
            app.Date = DateTime.Now;
            // Vložení žádosti do DB
            _databaseService.MyDatabase.InsertObjectToDb(app);

            Student student = new();
            student.Id = _databaseService.MyDatabase.GetNextIdFromDb<Student>();
            student.Name = form.Name;
            student.Address = form.Address;
            student.BirthNumber = form.BirthNumber;
            student.PhoneNumber = form.PhoneNumber;
            student.ApplicationId = appId;

            // Vložení studenta do databáze
            _databaseService.MyDatabase.InsertObjectToDb(student);

            foreach (var studyId in schoolForm.SelectedStudyProgramIds)
            {
                var newForm = new Form(studyId, appId);
                _databaseService.MyDatabase.InsertObjectToDb(newForm);
            }

            return RedirectToAction("FormSent");
        }

        [HttpGet]
        public JsonResult GetHighSchools()
        {
            return Json(HighSchools);
        }

        [HttpGet]
        public JsonResult GetStudyPrograms(int highSchoolId)
        {
            var studyPrograms = _databaseService.MyDatabase.GetObjectsFromDb<StudyProgram>(joinAfter:$"WHERE HighSchoolId = {highSchoolId}");
            return Json(studyPrograms);
        }
    }
}