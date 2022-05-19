using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BikeRoom.Models.BikeModelViewModel
{
    public class BikeModelVM
    {
        public BikesModel BikesModel { get; set; }
        
        public IEnumerable<MakedByCompany> MakedByCompany { get; set; }

    //    public IEnumerable<SelectListItem> Getlist(IEnumerable<MakedByCompany> item)
    //{
    //        List<SelectListItem>  listitems= new List<SelectListItem>();
    //        SelectListItem sli = new SelectListItem();
    //        foreach (var i in item)
    //        {
    //            sli = new SelectListItem
    //            {
    //                //Text = i.Name,
    //                //Value = i.Id.ToString()
    //                Text = i.GetType().GetProperty("Name").GetValue(i, null).ToString(),
    //                Value = i.GetType().GetProperty("Id").GetValue(i, null).ToString()

    //            };
    //            listitems.Add(sli);
    //        }
    //        return listitems;
    //}


    }
}
