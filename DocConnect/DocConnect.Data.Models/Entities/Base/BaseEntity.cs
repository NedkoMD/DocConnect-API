namespace DocConnect.Data.Models.Entities.Base
{
    public class BaseEntity : IAuditableInfo, ISoftDelete
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
