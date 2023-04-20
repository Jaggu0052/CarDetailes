using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCarBrandProject.IBusinessLogic;
using TaskCarBrandProject.Models;

namespace TaskCarBrandProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDetailsController : ControllerBase
    {
        private readonly ICarDetailsBusinessLogic _carDetailsBusinessLogic;

        public CarDetailsController(ICarDetailsBusinessLogic carDetailsBusinessLogic)
        {
            _carDetailsBusinessLogic = carDetailsBusinessLogic;
        }

        [HttpDelete("DeleteCarDetails/{id}")]
        public bool DeleteCarDetails(int id)
        {
            return _carDetailsBusinessLogic.DeleteCarDetails(id);
        }


        [HttpGet("GetAllCarDetails")]
        public List<CarDetails> GetAllCarDetails()
        {
            return _carDetailsBusinessLogic.GetAllCarDetails();
        }


        [HttpGet("GetByIdCarDetails/{id}")]
        public CarDetails GetByIdCarDetails(int id)
        {
            return _carDetailsBusinessLogic.GetByIdCarDetails(id);
        }

        [Route("InsertCarDetils")]
        [HttpPost]
        public bool InsertCarDetils([FromBody] CarDetails carDetail)
        {
            return _carDetailsBusinessLogic.InsertCarDetils(carDetail);
        }


        [HttpPut("UpdateCarDetails")]
        public bool UpdateCarDetails(CarDetails carDetail)
        {
            return _carDetailsBusinessLogic.UpdateCarDetails(carDetail);
        }
    }
}
