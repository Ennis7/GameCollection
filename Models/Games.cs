using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GameCollection.Models
{
    public enum Genre
    {
        Adventure,
        Action,
        OpenWorld,
        Fighting,
        Sports,
        Horror,
        FPS,
        Puzzle
    }
    public class Games
    {
        public int ID { get; set; }

        [StringLength(60)]
        public string Title { get; set; } = String.Empty;

        [Required]
        //[StringLength(60)]
        public Genre GenreType { get; set; }

        [StringLength(60)]
        public string Developer { get; set; } = String.Empty;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReleaseDate { get; set; }

        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } = 0;

        //Foreign Key
        public int OwnerID { get; set; }

        //Navigation Property
        public Owner Owner { get; set; } = null!;
    }
}
