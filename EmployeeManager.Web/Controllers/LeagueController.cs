using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.DAL;
using EmployeeManager.Model;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.Controllers
{
    public class LeagueController : Controller
    {
        
        private EmployeeManagerDbContext _dbContext;

        public LeagueController(EmployeeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [Authorize]
        public IActionResult Index(LeagueFilterModel filterModel)
        {
            var leagueQuery = this._dbContext.Leagues.AsQueryable();
            
            filterModel = filterModel ?? new LeagueFilterModel();

            if (!string.IsNullOrWhiteSpace(filterModel.Name))
                leagueQuery = leagueQuery.Where(p => p.Name.ToLower().Contains(filterModel.Name.ToLower()));

            var model = leagueQuery.ToList();
            
            return View(model);
        }
        
        [Authorize]
        public IActionResult Details(int? id = null)
        {
            var league = this._dbContext.Leagues
                .FirstOrDefault(p => p.Id == id);

            return View(league);
        }
        
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }
        
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public IActionResult Create(League model)
        {
            if (ModelState.IsValid)
            {
                this._dbContext.Leagues.Add(model);
                this._dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }
        
        [Authorize(Roles = "Manager")]
        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = this._dbContext.Leagues.FirstOrDefault(c => c.Id == id);
            return View(model);
        }
        
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            var client = this._dbContext.Leagues.FirstOrDefault(c => c.Id == id);
            var ok = await this.TryUpdateModelAsync(client);

            if (ok && this.ModelState.IsValid)
            {
                this._dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }
    }
}