using System.Diagnostics;
using EjadaAssignment.Models;
using EjadaAssignment.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EjadaAssignment.Controllers
{
    public class DepartmentController: Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Department> _repository;
        //Dependency Injection for the repository general entity interface strongly typed to "Department"
        public DepartmentController(ILogger<HomeController> logger, IRepository<Department> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public JsonResult List()
        {
            //Return JSON for all departments
            return Json(_repository.GetAllEntities());
        }

        [HttpPost]
        public JsonResult Create(Department department)
        {
            //Creation of a new department in database
            return Json(_repository.Add(department));
        }

        [HttpPost]
        public JsonResult Edit(Department department)
        {
            //Updating an existing department
            return Json(_repository.Update(department));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}