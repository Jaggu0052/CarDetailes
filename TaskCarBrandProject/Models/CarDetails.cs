using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskCarBrandProject.Models
{
    public class CarDetails
    {
        [Key]
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public bool New { get; set; }
        public string ImageUrl { get; set; }
    }
}
