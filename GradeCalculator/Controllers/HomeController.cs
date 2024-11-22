using GradeCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GradeCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GpaCalculatorContext _context;

        public HomeController(ILogger<HomeController> logger, GpaCalculatorContext Context)
        {
            _context = Context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GpaCalculator()
        {
            return View();
        }
        public IActionResult GetGpaCalculator()
        {
            return View();
        }
        public IActionResult Getentry(Course Request)
        {
            TempData["success"] = null;
            var data = Request;
            _context.Courses.Add(data);
            int save = _context.SaveChanges();
            if (save > 0)
            {
                TempData["success"] = "data added successfully";
            }
            return RedirectToAction("Details");
        }
        public IActionResult Details()
        {
            TempData["success"] = null;
            var data = _context.Courses.ToList();
            if (data.Any() == true)
            {
                TempData["success"] = "Items retrieved Successfully";
            }
            return View(data);

        }
        [HttpPost]
        public IActionResult Details(List<Course> courses)
        {
            if (courses == null || !courses.Any())
            {
                ViewBag.Message = "No courses were submitted!";
                return View("Index", courses);
            }

            int totalCredits = courses.Sum(c => c.CreditUnit);
            int totalGradePoints = courses.Sum(c =>
            {
                int grade;
                if (int.TryParse(c.Grade, out grade))
                {
                    return grade * c.CreditUnit;
                }
                else
                {

                    return 0;
                }
            });

            double gpa = (double)totalGradePoints / totalCredits;
            ViewBag.GPA = Math.Round(gpa, 2);

            return View("Details", courses);
        }
        public IActionResult Delete(List<Course> courses)
        {
            TempData["delete"] = null;
            var data = _context.Courses.ToList();
            if (data == null)
            {
                TempData["delete"] = "false";
                return RedirectToAction("Details");
            }
            _context.Courses.RemoveRange(data);
            var save = _context.SaveChanges();
            if (save <= 0)
            {
                TempData["delete"] = "false";
                return RedirectToAction("Details");
            }

            TempData["delete"] = "true";
            return View("Details");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
