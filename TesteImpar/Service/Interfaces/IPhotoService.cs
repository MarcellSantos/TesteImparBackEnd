using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteImpar.Domain.Model;

namespace TesteImpar.Service.Interfaces
{
    public interface IPhotoService
    {
        public Task<Photo> InsertPhoto(Photo model);
        public Task DeletePhoto(Photo model);
        public Task UpdatePhoto(Photo model);
        public Task<Photo> GetPhotoById(long id);
    }
}
