using DocConnect.Data.Models.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace DocConnect.Data.Models.Entities
{
    public class User : IdentityUser<uint>, ISoftDelete, IAuditableInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Verified { get; set; }

        public uint DoctorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public Doctor? Doctor { get; set; }

        public Patient? Patient { get; set; }

        public ICollection<Token> Tokens { get; set; } = new List<Token>();
    }
}


