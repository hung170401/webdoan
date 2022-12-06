using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webdoan.Areas.Admin.Models
{
    [Table("Categories")]
    public class Categories
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int CatId { get; set; }

        public string? CatName { get; set; }
        public bool Published { get; set; }
        public string? Description { get; set; }
    }
}
