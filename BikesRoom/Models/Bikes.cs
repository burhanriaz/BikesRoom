using BikeRoom.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BikesRoom.Models
{
    public class Bikes
    {
        
        public int Id { get; set; }
        public BikesModel BikesModel { get; set; }
        public int BikesModelId { get; set; }
        public MakedByCompany MakedByCompany { get; set; }
        public int MakedByCompanyId { get; set; }


        [Required]
        public DateTime Year { get; set; }


        [Required]
        public int Mileage { get; set; }


        [Required]
        public int Price { get; set; }


     
        public string Feature { get; set; }

        [Required]
        public string SellerName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public int Currency { get; set; }

        public string ImagePath { get; set; }
         
    }
}
