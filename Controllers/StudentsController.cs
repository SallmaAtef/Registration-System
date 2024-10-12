using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Controllers
{
    public class StudentsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public StudentsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;
            if(string.IsNullOrEmpty(search) == true)
            {

                return View("Index", _context.students.ToList());

            }
            else
            {
                return View("Index", _context.students.Where(d=>d.FullName.Contains(search)||
                d.Faculty.Contains(search)).ToList());
            }

            
        }

        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Student stud = _context.students.Include(d=> d.Instructors).FirstOrDefault(d => d.Id == id);

            ViewBag.CurrentStud = stud;
            
            if (stud == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", stud);
            }
        }
        [HttpGet]
        public IActionResult GetCreateView()
        { 

            return View("Create");
        }
        [HttpPost]
        public IActionResult AddNew(Student stud, IFormFile? imageFormFile)
        {
            if (imageFormFile != null) 
            {
                string imageExtension = Path.GetExtension(imageFormFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imageExtension;
                string imgPath = "\\images\\" + imgName;
                stud.ImagePath = imgPath;
                string imgFullPath = _webHostEnvironment.WebRootPath + imgName;

                FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                imageFormFile.CopyTo(imgFileStream);
                imgFileStream.Dispose();
            }
            else
            {
                stud.ImagePath = "\\images\\No_Image.png";
            }
            
            
            
            
            if (stud.JoinDate < new DateTime(2010, 8, 23))
            {
                ModelState.AddModelError(string.Empty, "Join Date must be after 23 August 2010 ");
            }

            if (ModelState.IsValid == true)
            {
                _context.students.Add(stud);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }

            else
            {
                return View("Create",stud);
            }
        }

        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Student student = _context.students.FirstOrDefault(d => d.Id ==id);
                if (student == null)
                {
                    return NotFound() ;
                }
                else
                {
                return View("Edit", student);
                }
            
        }

        [HttpPost]
        public IActionResult EditCurrent(Student stud, IFormFile? imageFormFile)
        {
            if (imageFormFile != null)
            {
                if (stud.ImagePath != "\\images\\No_Image.png")
                {
                    string oldImgFullPath = _webHostEnvironment.WebRootPath + stud.ImagePath;
                    System.IO.File.Delete(oldImgFullPath);
                }
                string imageExtension = Path.GetExtension(imageFormFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imageExtension;
                string imgPath = "\\images\\" + imgName;
                stud.ImagePath = imgPath;
                string imgFullPath = _webHostEnvironment.WebRootPath + imgName;

                FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                imageFormFile.CopyTo(imgFileStream);
                imgFileStream.Dispose();
            }
          

            if (stud.JoinDate < new DateTime(2010, 8, 23))
            {
                ModelState.AddModelError(string.Empty, "Join Date must be after 23 August 2010 ");
            }

            if (ModelState.IsValid == true)
            {
                _context.students.Update(stud);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit", stud);
            }
        }
        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Student stud = _context.students.Include(d => d.Instructors).FirstOrDefault(d => d.Id == id);

            
            ViewBag.CurrentStud = stud;

            if (stud == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", stud);
            }
        }
        [HttpPost]
        public IActionResult DeleteCurrent (int id) 
        {
            Student student = _context.students.FirstOrDefault(d => d.Id == id);
            
            if (student != null && student.ImagePath != "\\images\\No_Image.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + student.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }
            _context.students.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");

        }

        //https://localhost:7245/Departments/Greetvisitor
        public string GreetVisitor()
        {
            return "Welcome to Our Universty";
        }

        //https://localhost:7245/Departments/GreetUser?firstName=Folan&lastName=AlFolane
        public string GreetUser(string firstName, string lastName)
        {
            return $"Hi {firstName} {lastName}!";
        }
    }
}
