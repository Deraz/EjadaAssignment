using System.ComponentModel.DataAnnotations.Schema;

namespace EjadaAssignment.Models
{
    public class Employee
    {
        //Employee model properties (columns)
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
    }
}