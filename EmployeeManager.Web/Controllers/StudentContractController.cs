using System.Collections.Generic;
using EmployeeManager.DAL;
using EmployeeManager.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeManager.Controllers
{
    public class StudentContractController : Controller
    {
        private EmployeeManagerDbContext _dbContext;

        public StudentContractController(EmployeeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            this.FillDropdownValuesStudents();
            return View();
        }
        
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult Create(StudentContract model)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.StudentContracts.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Create));
            }
            else
            {
                this.FillDropdownValuesStudents();
                return View();
            }
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