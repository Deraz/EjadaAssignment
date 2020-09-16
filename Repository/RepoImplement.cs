using System;
using System.Collections.Generic;
using System.Linq;
using EjadaAssignment.Data;
using EjadaAssignment.Models;
using EjadaAssignment.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EjadaAssignment.Repository
{
    public class RepoImplement<T>:IRepository<T> where T:class
    {
        private ApplicationDbContext _db;
        //Dependency Injection for database instance.
        public RepoImplement(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<T> GetAllEntities()
        {
            //Read All
            return _db.Set<T>().ToList();
        }

        public T GetEntity(int id)
        {
            //Read One
            return _db.Set<T>().Find(id);
        }

        public int Add(T entity)
        {
            //Create One
            _db.Set<T>().Add(entity);
            return _db.SaveChanges();
        }

        public int Update(T entity)
        {
            //Update One
            _db.Set<T>().Attach(entity).State = EntityState.Modified;
            return _db.SaveChanges();
        }

        public int Remove(int id)
        {
            //Remove One
            var entity = GetEntity(id);
            _db.Set<T>().Remove(entity);
            return _db.SaveChanges();
        }

        public List<EmployeeViewModel> GetAllInViewModel(char condition)
        {
            //query is written as SQL Query below
           var result = 
                from emp in _db.Employees join tbl in
                    (from e in _db.Employees
                join d in _db.Departments on e.DepartmentId equals d.Id
                select new
                {
                    e.Id, e.FirstName, e.LastName, e.Email, e.Age, d.DepartmentName, d.ManagerId
                })
                on emp.Id equals tbl.ManagerId
                select new  EmployeeViewModel{ Id = tbl.Id, FirstName = tbl.FirstName,LastName = tbl.LastName, Email = tbl.Email, Age = tbl.Age, DepartmentName = tbl.DepartmentName, Manager = string.Concat(emp.FirstName," ",emp.LastName) };

                
            return result.ToList();


            //SQL Query (Sub-query type)
            //SELECT tbl.FirstName, tbl.LastName, tbl.Email, tbl.Age, tbl.DepartmentName, CONCAT(Employees.FirstName,' ',Employees.LastName) as Manager
            //FROM
            //(SELECT FirstName, LastName, Email, Age, DepartmentName, ManagerId FROM Employees as e
            //JOIN Departments as d
            //on e.DepartmentId = d.Id) AS tbl
            //JOIN Employees
            //on tbl.ManagerId = Employees.Id
        }
    }
}