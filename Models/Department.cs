using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjadaAssignment.Models
{
    public class Department
    {
        //Department model properties (columns)
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        [ForeignKey("Employee")]
        public int ManagerId { get; set; }
    }
}