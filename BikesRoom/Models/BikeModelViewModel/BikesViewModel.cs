using BikeRoom.Models;
using System.Collections.Generic;

namespace BikesRoom.Models.BikeModelViewModel
{
    public class BikesViewModel
    {
        public Bikes Bikes { get; set; }     
        public IEnumerable<BikesModel> BikesModel { get; set; }
        public IEnumerable<MakedByCompany> MakedByCompany { get; set; }
    }
}
