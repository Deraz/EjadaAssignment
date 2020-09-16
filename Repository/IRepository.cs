using System.Collections.Generic;
using EjadaAssignment.Models;
using EjadaAssignment.Models.ViewModels;

namespace EjadaAssignment.Repository
{
    public interface IRepository<T>
    {
        //General interface for an entity of type T
        //(Will be typed on injecting the dependency on its controller)
        List<T> GetAllEntities();
        T GetEntity(int id);
        int Add(T entity);
        int Update(T entity);
        int Remove(int id);
        List<EmployeeViewModel> GetAllInViewModel(char condition);
    }
}