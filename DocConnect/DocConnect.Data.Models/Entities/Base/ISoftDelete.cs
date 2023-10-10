namespace DocConnect.Data.Models.Entities.Base
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
