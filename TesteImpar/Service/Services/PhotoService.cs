using Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteImpar.Domain.Model;
using TesteImpar.Infra.Repositories;
using TesteImpar.Service.Interfaces;

namespace TesteImpar.Service.Services
{
    public class PhotoService:IPhotoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Photo> _repository;

        public PhotoService(IUnitOfWork uow, IRepository<Photo> repo)
        {
            this._repository = repo;
            this._uow = uow;
        }


        public async Task<Photo> InsertPhoto(Photo model)
        {
            var p=await this._repository.Insert(model);
            await this._uow.Save();
            return p;
        }

        public async Task DeletePhoto(Photo model)
        {
            await this._repository.Delete(model);
            await this._uow.Save();
        }
        public async Task<Photo> GetPhotoById(long id)
        {
            return await this._repository.Find(id);
        }

        public async Task UpdatePhoto(Photo model)
        {
            var p = await this.GetPhotoById(model.Id);
            p.Base64 = model.Base64;

            this._repository.Update(model);
            await this._uow.Save();
                
        }




    }
}
