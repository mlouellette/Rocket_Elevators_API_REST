using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;


namespace Rocket_Elevators_Rest_API.Models
{
    public class User
    {
        
        [Key]
        public int? id { get; set; }
        public string? email { get; set; }
        public string? encrypted_password { get; set; }

    }
}