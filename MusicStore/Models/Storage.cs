using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Models
{
    [Table("Storages")]
    public class Storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Record Record { get; set; }
        [Required]
        [ForeignKey("RecordId")]
        public int RecordId { get; set; }
        public int Quantity { get; set; }
    }
}