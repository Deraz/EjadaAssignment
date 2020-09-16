using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EjadaAssignment.Models;
using EjadaAssignment.Repository;
using Newtonsoft.Json;

namespace EjadaAssignment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Employee> _repository;
        //Dependency Injection for the repository general entity interface strongly typed to "Employee"
        public EmployeeController(ILogger<HomeController> logger, IRepository<Employee> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public JsonResult List()
        {
            //Listing all employees with a certain join query explained in function implementation
            return Json(_repository.GetAllInViewModel('e'));
        }

        [HttpPost]
        public JsonResult Create(Employee employee)
        {
            //Creation of a new employee
            return Json(_repository.Add(employee));
        }

        [HttpPost]
        public JsonResult Edit(Employee employee)
        {
           //Updating an existing employee
           return Json(_repository.Update(employee));
        }

        [HttpPost]
        public JsonResult Delete(Employee employee)
        {
            //Deletion of an existing employee
            return Json(_repository.Remove(employee.Id));
        }

        public JsonResult GetOne(int id)
        {
            //Retrieval of a certain employee with the column ID
            return Json(_repository.GetEntity(id));
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}