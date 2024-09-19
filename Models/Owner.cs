using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GameCollection.Models
{
    public class Owner
    {
        [Required]
        public int ID { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

        //navigation property with initialization
        public ICollection<Games> Games { get; set; } = new HashSet<Games>();
    }
}