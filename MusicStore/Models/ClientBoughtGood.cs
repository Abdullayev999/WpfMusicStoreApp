using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicStore.Models
{
    [Table("ClientBoughtGoods")] 
    public class ClientBoughtGood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RecordId { get; set; }
        [Required]
        [ForeignKey("RecordId")]
        public Record Record { get; set; }
        
        public int UserId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}