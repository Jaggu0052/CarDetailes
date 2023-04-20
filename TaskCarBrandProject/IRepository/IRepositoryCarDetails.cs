using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCarBrandProject.Models;

namespace TaskCarBrandProject.IRepository
{
    public interface IRepositoryCarDetails
    {
        public List<CarDetails> GetAllCarDetails();

        public CarDetails GetByIdCarDetails(int id);

        public bool UpdateCarDetails(CarDetails carDetail);

        public bool InsertCarDetils(CarDetails carDetail);

        public bool DeleteCarDetails(int id);
    }
}
