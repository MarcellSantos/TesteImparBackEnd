using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteImpar.Domain.Model;
using TesteImpar.Infra.Repositories;
using TesteImpar.Service.Interfaces;

namespace TesteImpar.Service.Services
{
    public class CarService:ICarService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Car> _repository;

        public CarService(IUnitOfWork uow,IRepository<Car> repo)
        {
            this._repository = repo;
            this._uow = uow;
        }


        public async Task ListCars()
        {
            await this._repository.FindAll();
        }

        public async Task<List<Car>> ListCarPerPage(int Page=0,int step=10, string title = "")
        {
            return this._repository.Queryable().Where(x => ((title == "") ? true : x.Name.Contains(title)))//caso tenha termo de procura filtra por ele
                .Skip(step * Page)
                .Take(step)
                .Include("Photo")
                .ToList();
        }
        public async Task<Car> GetCarById(long id)
        {
            return this._repository.Queryable().Where(x=>x.Id==id).FirstOrDefault();
        }


        public async Task InsertCar(Car model)
        {
            await this._repository.Insert(model);
            await this._uow.Save();
        }

        public async Task UpdateCar(Car model)
        {
            var car = await this.GetCarById(model.Id);
            //updating car
            car.Name = model.Name;
            car.Status = model.Status;
            
            this._repository.Update(car);
            await this._uow.Save();
        }
        public async Task DeleteCar(Car model)
        {
            
            await this._repository.Delete(model);
            await this._uow.Save();
            
        }

        public async Task<int> GetQtdPages()
        {
            return this._repository.Queryable().ToListAsync().Result.Count();
        }
    }
}
