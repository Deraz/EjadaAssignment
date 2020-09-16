using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjadaAssignment.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        [ForeignKey("Employee")]
        public int ManagerId { get; set; }
        //public virtual ICollection<Employee> Employees { get; set; }
    }
}