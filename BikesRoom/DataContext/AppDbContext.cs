using BikeRoom.Models;
using BikesRoom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeRoom.DataContext
{
    public class AppDbContext :IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {

        }

        public DbSet<BikesModel> BikesModels { get; set; }
        public DbSet<MakedByCompany> MakedByCompanys { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers  { get; set; }

        public DbSet<Bikes> Bikes { get; set; }



    }
}
