using System.Linq;
using EmployeeManager.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Controllers
{
    [Route("api/student-gameweeks")]
    [ApiController]
    public class GameweekApiController : Controller
    {
        private EmployeeManagerDbContext _dbContext;

        public GameweekApiController(EmployeeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(p => p.Id == id);

            var gameweeks = student.Gameweeks?.ToList();

            if (gameweeks == null)
            {
                return NotFound();
            }

            return Ok(gameweeks);
        }        
    }
}