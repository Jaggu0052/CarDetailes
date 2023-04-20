using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCarBrandProject.IBusinessLogic;
using TaskCarBrandProject.IRepository;
using TaskCarBrandProject.Models;

namespace TaskCarBrandProject.BusinessLogic
{
    public class CarDetailsBusinessLogic : ICarDetailsBusinessLogic
    {

        private readonly IRepositoryCarDetails _repositoryCarDetails;

        public CarDetailsBusinessLogic(IRepositoryCarDetails repositoryCarDetails)
        {
            _repositoryCarDetails = repositoryCarDetails;
        }


        public bool DeleteCarDetails(int id)
        {
            return _repositoryCarDetails.DeleteCarDetails(id);
        }

        public List<CarDetails> GetAllCarDetails()
        {
            return _repositoryCarDetails.GetAllCarDetails();
        }

        public CarDetails GetByIdCarDetails(int id)
        {
            return _repositoryCarDetails.GetByIdCarDetails(id);
        }

        public bool InsertCarDetils(CarDetails carDetail)
        {
            return _repositoryCarDetails.InsertCarDetils(carDetail);
        }

        public bool UpdateCarDetails(CarDetails carDetail)
        {
            return _repositoryCarDetails.UpdateCarDetails(carDetail);
        }
    }
}
