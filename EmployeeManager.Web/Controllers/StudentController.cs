using System.Collections.Generic;
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

            return View(student);
        }
        
        public IActionResult Create()
        {
            this.FillDropdownValues();
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
                this.FillDropdownValues();
                return View();
            }
        }
        
        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = this._dbContext.Students.FirstOrDefault(c => c.Id == id);
            this.FillDropdownValues();
            return View(model);
        }
        
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var client = this._dbContext.Students.FirstOrDefault(c => c.Id == id);
            var ok = await this.TryUpdateModelAsync(client);

            if (ok && this.ModelState.IsValid)
            {
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            this.FillDropdownValues();
            return View();
        }
        
        private void FillDropdownValues()
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