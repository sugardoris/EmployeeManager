using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.DAL;
using EmployeeManager.Model;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Controllers
{
    public class StudentController : Controller
    {
        private EmployeeManagerDbContext _dbContext;

        public StudentController(EmployeeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult Index(StudentFilterModel filter)
        {
            var studentQuery = this._dbContext.Students
                .Include(p => p.City)
                .AsQueryable();
            
            filter = filter ?? new StudentFilterModel();

            if (!string.IsNullOrWhiteSpace(filter.FullName))
                studentQuery = studentQuery
                    .Where(p => (p.FirstName + " " + p.LastName).ToLower()
                        .Contains(filter.FullName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                studentQuery = studentQuery
                    .Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.City))
                studentQuery = studentQuery
                    .Where(p => p.City.Name.ToLower().Contains(filter.City.ToLower()));

            var model = studentQuery.ToList();
            
            return View(model);
        }
        
        public IActionResult Details(int? id = null)
        {
            var student = this._dbContext.Students
                .Include(p => p.City)
                .FirstOrDefault(p => p.Id == id);

            StudentContract? contract = this._dbContext.StudentContracts
                .FirstOrDefault(p => p.StudentId == id && p.ContractDate.Month.Equals(DateTime.Today.Month));
            
            ViewBag.Contract = contract != null ? "DA" : "NE";

            var gameweeks = this._dbContext.Gameweeks
                .Where(p => p.StudentId == id)
                .Where(p => (p.StartDate.Month.Equals(DateTime.Today.Month) &&
                             p.StartDate.Day <= 27) || (p.StartDate.Month.Equals(DateTime.Today.Month - 1) &&
                                                        p.StartDate.Day >= 28))
                .ToList();

            if (gameweeks.Count > 0)
            {
                int numOfGameweeks = gameweeks.Count;
                int numOfGames = 0;
                foreach (var gameweek in gameweeks)
                {
                    numOfGames += gameweek.NumberOfGames;
                }

                int numHours = numOfGames * 2;
                ViewBag.Gameweeks = numOfGameweeks;
                ViewBag.Games = numOfGames;
                ViewBag.Hours = numHours;
            }
            else
            {
                ViewBag.Gameweeks = 0;
                ViewBag.Games = 0;
                ViewBag.Hours = 0;
            }

            return View(student);
        }

        public IActionResult Create()
        {
            this.FillDropdownValuesCities();
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Student model)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.Students.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                this.FillDropdownValuesCities();
                return View();
            }
        }
        
        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = this._dbContext.Students.FirstOrDefault(c => c.Id == id);
            this.FillDropdownValuesCities();
            return View(model);
        }
        
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var student = this._dbContext.Students.FirstOrDefault(c => c.Id == id);
            var ok = await this.TryUpdateModelAsync(student);

            if (ok && this.ModelState.IsValid)
            {
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            this.FillDropdownValuesCities();
            return View();
        }
        
        public IActionResult Delete(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(p => p.Id == id);

            this._dbContext.Students.Remove(student);
            this._dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        
        private void FillDropdownValuesCities()
        {
            var selectItems = new List<SelectListItem>();
            
            var listItem = new SelectListItem();
            listItem.Text = "Grad";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Cities)
            {
                listItem = new SelectListItem(category.Name, category.Id.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleCities = selectItems;
        }
    }
}