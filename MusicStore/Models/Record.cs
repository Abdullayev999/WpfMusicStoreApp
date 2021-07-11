using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Models
{
    [Table("Records")]
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int MusicQuantity { get; set; }
        public int PriceSale { get; set; }

        public int YearPublishing { get; set; }
       
        public int CollectiveId { get; set; }
        public int CostPrice { get; set; }
    
        public int PublisherId { get; set; }
    
        public int GenreId { get; set; }
        [Required]
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
        [Required]
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        [Required]
        [ForeignKey("CollectiveId")]
        public Collective Collective { get; set; }



    }
}