using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(3, ErrorMessage = "Unesite barem 3 znaka")]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(3, ErrorMessage = "Unesite barem 3 znaka")]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public City City { get; set; }
        
        public string FullName => $"{FirstName} {LastName}";
        
        public virtual ICollection<StudentContract> Contracts { get; set; }
        
        public virtual ICollection<Gameweek> Gameweeks { get; set; }
    }
}