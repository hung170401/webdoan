using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace webdoan.Areas.Admin.Models
{
    [Table("Roles")]
    public partial class Role
    {
        
        public int RoleId { get; set; }

        public string? RoleName { get; set; }

        public string? Description { get; set; }

        public virtual ICollection<Account> Accounts { get; } = new List<Account>();
    }

}
