using DocConnect.Data.Models.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace DocConnect.Data.Models.Entities
{
    public class Role : IdentityRole<uint>, ISoftDelete, IAuditableInfo
    {
        public Role() : base() { }

        public Role(string role) : base(role) { }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
