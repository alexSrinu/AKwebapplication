using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AKwebapplication.Models;

namespace AKwebapplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeRep _repository;

        // Constructor to inject the repository
        public HomeController()
        {
            _repository = new HomeRep(); // Or use Dependency Injection
        }

        // GET: Home
        public ActionResult Index()
        {
            var persons = _repository.GetAllActivePersons();
            return View(persons);
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            var person = _repository.GetPersonById(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Home person, HttpPostedFileBase File)
        {
          //  if (ModelState.IsValid)
          //  {
                if (File != null && File.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(File.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    File.SaveAs(path);
                    person.FileName = fileName; // Save the file name to the model
                }

                _repository.InsertPerson(person);
                return RedirectToAction("Index");
         //   }

            // Check for errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(person);
        }
        // GET: Home/Edit/5
        // GET: Home/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var person = _repository.GetPersonById(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            // Ensure dates are formatted correctly
            person.JoinDate = person.JoinDate.Date;
            person.FeePaidDate = person.FeePaidDate.HasValue ? (DateTime?)person.FeePaidDate.Value.Date : null;
            person.DueDate = person.DueDate.HasValue ? (DateTime?)person.DueDate.Value.Date : null;

            return View(person);
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Home person, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    // Save the new image file
                    var filePath = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);

                    person.FileName = Path.GetFileName(file.FileName); // Update the person's FileName property
                }
                else
                {
                    // If no new file is uploaded, keep the existing image name
                    var existingPerson = _repository.GetPersonById(person.Id);
                    person.FileName = existingPerson.FileName;
                }

                _repository.UpdatePerson(person);
                return RedirectToAction("Index");
            }

            return View(person);
        }
    



    /*  [HttpGet]
      public ActionResult Edit(int id)
      {
          var person = _repository.GetPersonById(id);
          if (person == null)
          {
              return HttpNotFound();
          }


          person.JoinDate = person.JoinDate.Date; 
          person.FeePaidDate = person.FeePaidDate.HasValue ? (DateTime?)person.FeePaidDate.Value.Date : null; 
          person.DueDate = person.DueDate.HasValue ? (DateTime?)person.DueDate.Value.Date : null; 

          return View(person);
      }

      // POST: Home/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(Home person)
      {
          if (ModelState.IsValid)
          {
              _repository.UpdatePerson(person);
              return RedirectToAction("Index");
          }
          return View(person);
      }*/

    // GET: Home/Delete/5
    public ActionResult Delete(int id)
        {
            var person = _repository.GetPersonById(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.DeletePerson(id);
            return RedirectToAction("Index");
        }
    }
}