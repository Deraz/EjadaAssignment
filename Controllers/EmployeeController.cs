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

        public EmployeeController(ILogger<HomeController> logger, IRepository<Employee> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public JsonResult List()
        {
            return Json(_repository.GetAllInViewModel('e'));
        }

        [HttpPost]
        public JsonResult Create(Employee employee)
        {
            return Json(_repository.Add(employee));
        }

        [HttpPost]
        public JsonResult Edit(Employee employee)
        {
           return Json(_repository.Update(employee));
        }

        [HttpPost]
        public JsonResult Delete(Employee employee)
        {
            return Json(_repository.Remove(employee.Id));
        }

        public JsonResult GetOne(int id)
        {
            return Json(_repository.GetEntity(id));
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}