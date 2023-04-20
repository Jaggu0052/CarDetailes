using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskCarBrandProject.Context;
using TaskCarBrandProject.IRepository;
using TaskCarBrandProject.Models;

namespace TaskCarBrandProject.Repository
{
    public class CarDetailsRepository : IRepositoryCarDetails
    {

        private readonly CarDetailsContext _carDetailsContext;

        public CarDetailsRepository(CarDetailsContext carDetailsContext)
        {
            _carDetailsContext = carDetailsContext;
        }

        public bool DeleteCarDetails(int id)
        {
            var data = _carDetailsContext.CarDetail.Where(findData => findData.Id == id).FirstOrDefault();

            _carDetailsContext.CarDetail.Remove(data);
            _carDetailsContext.SaveChanges();

            return true;
        }

        public List<CarDetails> GetAllCarDetails()
        {
            Logger.Debug("Start");
            var data = _carDetailsContext.CarDetail.ToList();
            Logger.Debug("End");
            return data;
        }

        public CarDetails GetByIdCarDetails(int id)
        {
            var data = _carDetailsContext.CarDetail.Where(findData => findData.Id == id).FirstOrDefault();
            return data;
        }

        public bool InsertCarDetils(CarDetails carDetail)
        {
            _carDetailsContext.CarDetail.Add(carDetail);
            _carDetailsContext.SaveChanges();
            return true;

        }

        public bool UpdateCarDetails(CarDetails carDetail)
        {

         
            var data = _carDetailsContext.CarDetail.Where(findData => findData.Id == carDetail.Id).FirstOrDefault();

            data.BrandName = carDetail.BrandName;
            data.Id = carDetail.Id;
            data.Model = carDetail.Model;
            data.New = carDetail.New;
            data.Price = carDetail.Price;
            data.ImageUrl = carDetail.ImageUrl;
            data.Year = carDetail.Year;

            _carDetailsContext.CarDetail.Update(data);
            _carDetailsContext.SaveChanges();
            return true;
        }
    }
}
