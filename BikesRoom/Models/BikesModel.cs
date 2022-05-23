
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRoom.Models
{
    public class BikesModel
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage ="provide Bike Model")]
        [StringLength(100,ErrorMessage ="Can't be greather then 100 letters"),MinLength(2,ErrorMessage ="Model Name Shoud be 2 letter or more") ]
        public string Name { get; set; }

        [ForeignKey("MakedByCompany")]
        [Required(ErrorMessage = "Select Company Name")]
        public int MakedByFk { get; set; }
        public MakedByCompany MakedByCompany { get; set; }

       
    }
}
