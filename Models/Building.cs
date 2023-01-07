using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;


namespace Rocket_Elevators_Rest_API.Models
{
    public class Building
    {
        [Key]
        public int? Id { get; set; }
        public string? addressBuilding { get; set; }
        public string? FullNameBuildingAdmin { get; set; }
        public string? EmailAdminBuilding { get; set; }
        public int? customer_id { get; set; }


    }
}
