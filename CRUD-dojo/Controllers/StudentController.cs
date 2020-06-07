using System.Linq;
using CRUD_dojo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_dojo.Controllers
{
    public class StudentController : Controller
    {
        private static InMemory _inMemory = new InMemory();


        [HttpGet]
        public IActionResult Index()
        {
            return View(_inMemory.Students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Student newStudent)
        {
            _inMemory.Students.Add(newStudent); //TODO: Ensure IDs are unique
            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var studentToEdit = _inMemory.Students.FirstOrDefault(s => s.Id == id);
            return View(studentToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Student editedStudent)
        {
            var student = _inMemory.Students.FirstOrDefault(s => s.Id == editedStudent.Id);
            var index = _inMemory.Students.IndexOf(student);

            if (student != null)
            {
                _inMemory.Students[index] = editedStudent;
            }

            return RedirectToAction("Index", "Student");
        }


        [HttpGet("*url", Order = int.MaxValue)]
        public IActionResult Default(int id)
        {
            return RedirectToAction("Index", "Student");
        }
    }
}
