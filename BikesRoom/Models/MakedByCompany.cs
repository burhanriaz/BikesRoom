using System.ComponentModel.DataAnnotations;

namespace BikeRoom.Models
{
    public class MakedByCompany
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "provide Company Name")]
        [StringLength(100, ErrorMessage = "Can't be greather then 100 letters"), MinLength(3, ErrorMessage = "Company Name Shoud be 3 letter or more")]

        public string Name { get; set; }
    }
}

