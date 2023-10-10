using DocConnect.Data.Models.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace DocConnect.Data.Models.Entities
{
    public class RoleClaim : IdentityRoleClaim<uint>, ISoftDelete, IAuditableInfo
    {
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
