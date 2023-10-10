namespace DocConnect.Data.Models.Entities.Base
{
    public interface IAuditableInfo
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
