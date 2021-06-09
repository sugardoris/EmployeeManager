using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.DAL;
using EmployeeManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Controllers
{
    [Route("api/student-contract")]
    [ApiController]
    public class StudentContractApiController : Controller
    {
        private EmployeeManagerDbContext _dbContext;

        public StudentContractApiController(EmployeeManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult Get()
        {
            var contracts = this._dbContext.StudentContracts
                .Include(p => p.Student)
                .Include(p => p.Student.City)
                .Select(p => new ContractDTO()
                {
                    Id = p.Id,
                    Date = p.ContractDate,
                    StudentDto = new StudentDTO()
                    {
                        Id = p.Student.Id,
                        FullName = p.Student.FullName,
                        Email = p.Student.Email,
                        CityDto = new CityDTO()
                        {
                            Id = p.Student.CityId,
                            Name = p.Student.City.Name
                        }
                    }
                })
                .ToList();

            return Ok(contracts);
        }
        
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var contract = this._dbContext.StudentContracts
                .Include(p => p.Student)
                .Include(p => p.Student.City)
                .Where(p => p.Id == id)
                .Select(p => new ContractDTO()
                {
                    Id = p.Id,
                    Date = p.ContractDate,
                    StudentDto = new StudentDTO()
                    {
                        Id = p.Student.Id,
                        FullName = p.Student.FullName,
                        Email = p.Student.Email,
                        CityDto = new CityDTO()
                        {
                            Id = p.Student.CityId,
                            Name = p.Student.City.Name
                        }
                    }
                })
                .FirstOrDefault();

            if (contract == null)
            {
                return NotFound();
            }

            return Ok(contract);
        }
        
        [HttpPost]
        public async Task<ActionResult<ContractDTO>> Post([FromBody] StudentContract contractModel)
        {
            if (contractModel == null)
            {
                return BadRequest();
            }

            this._dbContext.Add(new StudentContract()
            {
                ContractDate = contractModel.ContractDate,
                StudentId = contractModel.StudentId
            });

            this._dbContext.SaveChanges();

            return Ok();

        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ContractDTO>> Put(int id, [FromBody] StudentContract contractModel)
        {
            var contract = this._dbContext.StudentContracts
                .Include(p => p.Student)
                .FirstOrDefault(p => p.Id == id);
            
            if (contractModel.ContractDate != null)
            {
                contract.ContractDate = contractModel.ContractDate;
            }

            if (contractModel.StudentId != null)
            {
                contract.StudentId = contractModel.StudentId;
            }

            this._dbContext.SaveChanges();

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contract = this._dbContext.StudentContracts
                .Include(p => p.Student)
                .FirstOrDefault(p => p.Id == id);

            if (contract == null)
            {
                return BadRequest();
            }
            this._dbContext.Remove(contract);
            this._dbContext.SaveChanges();
            return NoContent();
        }
        
        public class ContractDTO
        {
            public int Id { get; set; }
            
            public DateTime Date { get; set; }
            
            public StudentDTO StudentDto { get; set; }
        }
        
        public class StudentDTO
        {
            public int Id { get; set; }
            
            public string FullName { get; set; }
            
            public string Email { get; set; }

            public CityDTO CityDto { get; set; }
        }
        
        public class CityDTO
        {
            public int Id { get; set; }
            
            public string Name { get; set; }
        }
    }
}