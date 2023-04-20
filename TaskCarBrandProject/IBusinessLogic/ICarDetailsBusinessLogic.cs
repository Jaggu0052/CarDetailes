using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCarBrandProject.Models;

namespace TaskCarBrandProject.IBusinessLogic
{
    public interface ICarDetailsBusinessLogic
    {
        public List<CarDetails> GetAllCarDetails();

        public CarDetails GetByIdCarDetails(int id);

        public bool UpdateCarDetails(CarDetails carDetail);

        public bool InsertCarDetils(CarDetails carDetail);

        public bool DeleteCarDetails(int id);
    }
}
