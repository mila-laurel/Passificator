using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Passificator.Model
{
    [Table("Staff")]
    internal class Staff
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("position")]
        public string Position { get; set; }
    }
}
