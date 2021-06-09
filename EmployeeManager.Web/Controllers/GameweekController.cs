using System.Collections.Generic;
using System.Linq;
using EmployeeManager.DAL;
using EmployeeManager.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Controllers
{
    public class GameweekController : Controller
    {
        
        private EmployeeManagerDbContext _dbContext;

        public GameweekController(EmployeeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [Authorize]
        public IActionResult Index()
        {
            var gameweeks = this._dbContext.Gameweeks
                .Include(p => p.League)
                .Include(p => p.Student)
                .ToList();

            return View(gameweeks);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            this.FillDropdownValuesLeagues();
            this.FillDropdownValuesStudents();
            return View();
        }
        
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult Create(Gameweek model)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.Gameweeks.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                this.FillDropdownValuesLeagues();
                this.FillDropdownValuesStudents();
                return View();
            }
        }
        
        private void FillDropdownValuesLeagues()
        {
            var selectItems = new List<SelectListItem>();
            
            var listItem = new SelectListItem();
            listItem.Text = "Liga";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Leagues)
            {
                listItem = new SelectListItem(category.Name, category.Id.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleLeagues = selectItems;
        }
        
        private void FillDropdownValuesStudents()
        {
            var selectItems = new List<SelectListItem>();
            
            var listItem = new SelectListItem();
            listItem.Text = "Student";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in this._dbContext.Students)
            {
                listItem = new SelectListItem(category.FullName, category.Id.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleStudents = selectItems;
        }
    }
}