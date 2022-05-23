using AutoMapper;
using BikeRoom.Models;
using BikesRoom.Controllers.Resources;

namespace BikesRoom.MappingProfiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BikesModel, BikesModelResources>();
        }
    }
}
