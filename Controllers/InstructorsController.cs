using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FinalProject.Controllers
{
    public class InstructorsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public InstructorsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {

                return View("Index", _context.instructors.ToList());

            }
            else
            {
                return View("Index", _context.instructors.Where(d => d.FullName.Contains(search) ||
                d.CourseName.Contains(search)).ToList());
            }

        }
        [HttpGet]
        public IActionResult GetDetailsView(int id)
        {
            Instructor inst = _context.instructors.Include(e => e.student).FirstOrDefault(e=> e.Id == id);

            if (inst == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", inst);
            }
        }
        [HttpGet]
        public IActionResult GetCreateView()
        {
           
            ViewBag.AllStudents = _context.students.ToList();
          
            return View("Create");
        }
        [HttpPost]
        public IActionResult AddNew(Instructor inst, IFormFile? imageFormFile)
        {
            if (imageFormFile != null)
            {
                string imageExtension = Path.GetExtension(imageFormFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imageExtension;
                string imgPath = "\\images\\" + imgName;
                inst.ImagePath = imgPath;
                string imgFullPath = _webHostEnvironment.WebRootPath + imgName;

                FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                imageFormFile.CopyTo(imgFileStream);
                imgFileStream.Dispose();
            }
            else
            {
                inst.ImagePath = "\\images\\No_Image.png";
            }


            if (inst.JoinDate < new DateTime(2010,8,23))
            {
                ModelState.AddModelError(string.Empty, "Join Date must be after 23 August 2010 ");
            }

            if (ModelState .IsValid == true)
            {
                _context.instructors.Add(inst);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllStudents = _context.students.ToList();

                return View("Create",inst);
            }
            

           
        }

        [HttpGet]
        public IActionResult GetEditView(int id)
        {
            Instructor instructor = _context.instructors.FirstOrDefault(d => d.Id ==id);
            if (instructor == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllStudents = _context.students.ToList();
                return View("Edit", instructor);
            }

        }

        [HttpPost]
        public IActionResult EditCurrent(Instructor inst, IFormFile? imageFormFile)
        {
            if (imageFormFile != null)
            {
                if (inst.ImagePath != "\\images\\No_Image.png") 
                {
                    string oldImgFullPath = _webHostEnvironment.WebRootPath + inst.ImagePath;
                    System.IO.File.Delete(oldImgFullPath);
                }
                string imageExtension = Path.GetExtension(imageFormFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imageExtension;
                string imgPath = "\\images\\" + imgName;
                 inst.ImagePath = imgPath;
                string imgFullPath = _webHostEnvironment.WebRootPath + imgName;

                FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                imageFormFile.CopyTo(imgFileStream);
                imgFileStream.Dispose();
            }
         

            if (inst.JoinDate < new DateTime(2010, 8, 23))
            {
                ModelState.AddModelError(string.Empty, "Join Date must be after 23 August 2010 ");
            }

            if (ModelState.IsValid == true)
            {
                _context.instructors.Update(inst);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllStudents = _context.students.ToList();
                return View("Edit", inst);
            }
        }

        [HttpGet]
        public IActionResult GetDeleteView(int id) 
        {
            Instructor inst = _context.instructors.Include(e => e.student).FirstOrDefault(e => e.Id == id);
            if (inst == null) 
            {
                return NotFound();
            }
            else
            {
                return View("Delete", inst);
            }
        }
        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Instructor instructor = _context.instructors.FirstOrDefault(d => d.Id == id);
            if (instructor != null && instructor.ImagePath != "\\images\\No_Image.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + instructor.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.instructors.Remove(instructor);
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
