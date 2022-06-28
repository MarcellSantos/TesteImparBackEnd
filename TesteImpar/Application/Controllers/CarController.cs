using Infra.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteImpar.Application.InputModel;
using TesteImpar.Application.ResponseModel;
using TesteImpar.Domain.Model;
using TesteImpar.Service.Interfaces;
using TesteImpar.Service.Services;

namespace TesteImpar.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _uow;
        public CarController(ICarService carService, IPhotoService photoService, IUnitOfWork uow)
        {
            this._carService = carService;
            this._photoService = photoService;
            this._uow = uow;
        }


        [HttpGet]
        public async Task<IActionResult> ListCars()
        {
            try
            {


                return StatusCode(200, new ApiResponse()
                {

                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new ApiResponse()
                {
                    Message = ex.Message
                });
            }

            return null;
        }

        



        [HttpGet]
        public async Task<IActionResult> ListCarsPerPage(int page = 0, int step = 10)
        {

            try
            {
                var response = await this._carService.ListCarPerPage(page, step);
                var numPages = await this._carService.GetQtdPages();
                return StatusCode(200, new ApiResponse()
                {
                    Content = new { response, numPages }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new ApiResponse()
                {
                    Message = ex.Message
                });
            }
        }



        [HttpPost]
        public async Task<IActionResult> InsertCar(CardInputModel model)
        {
            try
            {
                var photo = new Photo() { 
                    Base64 =model.Base64

                };
                var car = new Car() { 
                    Name=model.Titulo,
                    Status=true
                };


                var p = await this._photoService.InsertPhoto(photo);
                car.PhotoId = p.Id;
                await this._carService.InsertCar(car);

                return StatusCode(200, new ApiResponse()
                {
                    Message = "Sucsess"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new ApiResponse()
                {
                    Message = ex.Message
                });
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditCar(CardInputModel model)
        {
            try
            {

                this._uow.BeginTransaction();
                var carOld=await this._carService.GetCarById(model.IdCar);
                var photoOld = await this._photoService.GetPhotoById(carOld.PhotoId.Value);
                carOld.Name = model.Titulo;
                carOld.Status = model.Status;
                photoOld.Base64 = model.Base64;
                await this._carService.UpdateCar(carOld);
                await this._photoService.UpdatePhoto(photoOld);
                this._uow.Commit();
                return StatusCode(200, new ApiResponse()
                {
                    Message = "Sucsess"
                });
            }
            catch (Exception ex)
            {
                this._uow.Rollback();
                return StatusCode(400, new ApiResponse()
                {
                    Message = ex.Message
                }); 

            }
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCar(long id)
        {

            try
            {
                this._uow.BeginTransaction();
                var car = await this._carService.GetCarById(id);
                if (car.PhotoId != null)
                {
                    var photo = await this._photoService.GetPhotoById(car.PhotoId.Value);
                    await this._photoService.DeletePhoto(photo);
                }
                await this._carService.DeleteCar(car);

                this._uow.Commit();
                return StatusCode(200, new ApiResponse()
                {
                    Message = "Sucsess"
                });
            }
            catch (Exception ex)
            {
                this._uow.Rollback();
                return StatusCode(400, new ApiResponse()
                {
                    Message = ex.Message
                });
            }

        }
    }
}
