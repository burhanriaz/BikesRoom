using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRoom.Models
{
    public class BikesModel
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [ForeignKey("MakedByCompany")]
        public int MakedByFk { get; set; }
        public MakedByCompany MakedByCompany { get; set; }

       
    }
}
