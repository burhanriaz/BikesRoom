using System.ComponentModel.DataAnnotations;

namespace BikeRoom.Models
{
    public class MakedByCompany
    {
        public int Id { get; set; }

        [Required]
       [StringLength(255)]
        public string Name { get; set; }
    }
}

