using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Model
{
    public class Gameweek
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Range(1, 50, ErrorMessage = "Redni broj ne može biti manji od 1 ni veći od 50")]
        public int OrdinalNumber { get; set; }
        
        [Required]
        [Range(1, 16, ErrorMessage = "Kolo ne može imati manje od 1 ni više od 16 utakmica")]
        public int NumberOfGames { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }
        
        [ForeignKey(nameof(League))]
        public int LeagueId { get; set; }
        public League League { get; set; }
        
        public virtual ICollection<Student> Students { get; set; }
    }
}