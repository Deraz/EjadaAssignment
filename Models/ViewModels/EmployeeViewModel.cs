using System;

namespace EjadaAssignment.Models.ViewModels
{
    public class EmployeeViewModel
    {
        //View Model for the employee to include the output of the join in RepoImplement
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string DepartmentName { get; set; }
        public string Manager { get; set; }

    }
}