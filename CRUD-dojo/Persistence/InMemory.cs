using System.Collections.Generic;
using CRUD_dojo.Models;

namespace CRUD_dojo.Controllers
{
    internal class InMemory
    {
        public List<Student> Students;

        public InMemory()
        {
            Students = new List<Student>();
            SeedStudents();
        }

        private void SeedStudents()
        {
            Students.Add(new Student(1, "Krzysztof", "Jaroska", 28));
            Students.Add(new Student(2, "Dominik", "Starzyk", 21));
            Students.Add(new Student(3, "Agnieszka", "Koszany", 18));
        }

        public bool IsValid(User user)
        {
            if (user.Name == "1") // Check in Database if user exists- simplified AF
            {
                user.Role = Roles.User;
                return true;
            }

            if (user.Name == "2")
            {
                user.Role = Roles.Admin;
                return true;
            }

            return false;
        }
    }
}