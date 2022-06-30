using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteImpar.Domain.Model;

namespace TesteImpar.Service.Interfaces
{
    public interface ICarService
    {
        public Task<List<Car>> ListCarPerPage(int Page = 0, int step = 10,string title="");
        public Task InsertCar(Car model);
        public Task UpdateCar(Car model);
        public Task DeleteCar(Car model);
        public Task<Car> GetCarById(long id);
        public Task<int> GetQtdPages();
    }
}
