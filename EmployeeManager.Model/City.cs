using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Model
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public virtual ICollection<Student> Students { get; set; }
    }
}