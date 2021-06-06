using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.Model
{
    public class StudentContract
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime ContractDate { get; set; }
        
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}