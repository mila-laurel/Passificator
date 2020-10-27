using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passificator.Model
{
    [Table("Guests")]
    internal class Guest
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("company")]
        public string Company { get; set; }
        [Column("document")]
        public string Document { get; set; }
        [Column("car")]
        public string Car { get; set; }
    }
}
