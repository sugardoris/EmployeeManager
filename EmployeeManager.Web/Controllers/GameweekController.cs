using System.Collections.Generic;
using EmployeeManager.DAL;
using EmployeeManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManager.Controllers
{
    public class GameweekController : Controller
    {
        
        private EmployeeManagerDbContext _dbContext;

        public GameweekController(EmployeeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Create()
        {
            this.FillDropdownValuesLeagues();
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Gameweek model)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.Gameweeks.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction();
            }
            else
            {
                this.FillDropdownValuesLeagues();
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
    }
}