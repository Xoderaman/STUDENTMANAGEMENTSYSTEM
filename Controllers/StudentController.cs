using DemoCrud.Data;
using DemoCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoCrud.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor to initialize the DbContext
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public IActionResult Index()
        {
            var students = _context.Students.ToList(); // Fetch students from the database
            return View(students); // Return to the View with students data
        }

        // GET: Student/Details/5
        public IActionResult Details(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound(); // If student is not found
            }
            return View(student); // Return details of the student
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View(); // Return empty form to create a student
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstName, LastName, DateOfBirth, Email, EnrollmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student); // Add new student to DbContext
                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to Index view
            }
            return View(student); // Return to Create view if model is invalid
        }

        // GET: Student/Edit/5
        public IActionResult Edit(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound(); // If student is not found
            }
            return View(student); // Return the student object for editing
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, FirstName, LastName, DateOfBirth, Email, EnrollmentId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound(); // If the ID doesn't match, return NotFound
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student); // Update the student in the database
                    _context.SaveChanges(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Students.Any(s => s.Id == student.Id))
                    {
                        return NotFound(); // If the student doesn't exist anymore, return NotFound
                    }
                    else
                    {
                        throw; // Rethrow if any other concurrency issue happens
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to Index view
            }
            return View(student); // Return to Edit view if the model is invalid
        }

        // GET: Student/Delete/5
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound(); // If student not found, return NotFound
            }
            return View(student); // Return the student to be deleted
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student); // Remove the student from DbContext
                _context.SaveChanges(); // Save changes to the database
            }
            return RedirectToAction(nameof(Index)); // Redirect to Index after deleting
        }
    }
}
