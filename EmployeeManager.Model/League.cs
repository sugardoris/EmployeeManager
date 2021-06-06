using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Model
{
    public class League
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(5, ErrorMessage = "Unesite barem 5 znakova")]
        public string Name { get; set; }
        
        [Required]
        [Range(30, 50, ErrorMessage = "Liga može imati minimalno 30, a maksimalno 50 kola")]
        public int NumberOfGameweeks { get; set; }
        
        public virtual ICollection<Gameweek> Gameweeks { get; set; }
    }
}