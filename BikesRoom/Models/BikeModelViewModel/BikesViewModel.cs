using BikeRoom.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BikesRoom.Extentions.DocumentVM;

namespace BikesRoom.Models.BikeModelViewModel
{
    public class BikesViewModel
    {
        public Bikes Bikes { get; set; }
        public IEnumerable<BikesModel> BikesModel { get; set; }
        public IEnumerable<MakedByCompany> MakedByCompany { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }

        //[Required(ErrorMessage = "Please Select file")]
        //[AllowedExtensions(new string[] { ".png", ".jpg" })]
       // public IFormFile image { get; set; }
        
    
        private List<Currency> Clist = new List<Currency>();
        private List<Currency> CreateList()
        {
            Clist.Add(new Currency("PAK", "PAK"));
            Clist.Add(new Currency("USD", "USD"));
            Clist.Add(new Currency("IND", "IND"));

            return Clist;

        }
        public BikesViewModel()
        {
            Currencies = CreateList();
        }
    } 
    public class Currency  
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public Currency(String id, String name)
        {
            Id = id;
            Name = name;
        }
    }

}
