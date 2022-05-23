using BikeRoom.Models;
using System;
using System.ComponentModel.DataAnnotations;
using BikesRoom.Extentions;
using BikesRoom.Extentions.DocumentVM;

namespace BikesRoom.Models
{
    public class Bikes
    {
        
        public int Id { get; set; }

        public BikesModel BikesModel { get; set; }


        //[RegularExpression("^0*[1-9]*$", ErrorMessage = "Select Bike Model")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Bike Model")]
        public int BikesModelId { get; set; }
        public MakedByCompany MakedByCompany { get; set; }

        [RegularExpression("^[1-9]*$", ErrorMessage = "Select Company Name")]
        public int MakedByCompanyId { get; set; }


        [Required (ErrorMessage ="Provide Year of Purching")]
        [YearRange(1990,ErrorMessage = "Invalid Year")]  //create custom data Annotations for year range
        public int Year { get; set; }


        [Required(ErrorMessage = "Provide Mileage" )]
         [Range(1,int.MaxValue ,ErrorMessage ="Mileage Should be grether then 0")]
        public int Mileage { get; set; }


        [Required(ErrorMessage = "Provide Price")]
        [Range(1, int.MaxValue,ErrorMessage = "Price Should be grether then 0")]
        public int Price { get; set; }


     
        public string Feature { get; set; }

        [Required(ErrorMessage = "Provide Seller Name")]
        public string SellerName { get; set; }

        [Required(ErrorMessage = "Provide Seller Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Provide Seller Phone")]
        public string Phone { get; set; }

        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Select Currency")]
        public string Currency { get; set; }

      //  [Required(ErrorMessage = "Please Select file")]
        //[AllowedExtensions(new string[] { ".png", ".jpg" })]
        public string ImagePath { get; set; }
         
    }
}
