﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using CRUD_dojo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_dojo.Controllers
{
    public class StudentController : Controller
    {
        private static InMemory _inMemory = new InMemory();


        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Students List";
            

            if (HttpContext.Session.GetString("activeUser") == null)
            {
                return RedirectToAction("Login", "Student");
            }

            var userAsJson = HttpContext.Session.GetString("activeUser");
            var activeUser = JsonSerializer.Deserialize<User>(userAsJson);
            ViewData["Role"] = activeUser.Role;

            Response.Cookies.Append("MyCookie", "MyCookieValue");

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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (_inMemory.IsValid(user))
            {
                var userAsJson = JsonSerializer.Serialize(user);
                HttpContext.Session.SetString("activeUser", userAsJson);

                return RedirectToAction("Index", "Student");
            }

            return View();
        }

        public IActionResult Logout()
        {
            // HttpContext.Session.Remove("activeUser");
            Response.Cookies.Delete("MyCookie");
            HttpContext.Session.Clear(); // remove all keys from session

            return RedirectToAction("Login", "Student");
        }
    }
}
