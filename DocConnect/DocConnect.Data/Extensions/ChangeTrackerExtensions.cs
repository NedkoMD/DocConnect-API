using DocConnect.Data.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DocConnect.Data.Extensions
{
    public static class ChangeTrackerExtensions
    {
        public static void SetEntityProperties(this ChangeTracker changeTracker)
        {
            SoftDelete(changeTracker);
            CreatedAt(changeTracker);
            UpdatedAt(changeTracker);
        }

        private static void CreatedAt(ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            var entities = changeTracker
                    .Entries()
                    .Where(t => t.Entity is IAuditableInfo && t.State == EntityState.Added);

            foreach (var entry in entities)
            {
                var dateTime = DateTime.UtcNow;
                var entity = (IAuditableInfo)entry.Entity;
                entity.CreatedAt = dateTime;
                entity.UpdatedAt = dateTime;
            }
        }

        private static void UpdatedAt(ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            var entities = changeTracker
                    .Entries()
                    .Where(t => t.Entity is IAuditableInfo && t.State == EntityState.Modified);

            foreach (var entry in entities)
            {
                var entity = (IAuditableInfo)entry.Entity;
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        private static void SoftDelete(ChangeTracker changeTracker)
        {
            changeTracker.DetectChanges();
            var entities = changeTracker
                    .Entries()
                    .Where(t => t.Entity is ISoftDelete && t.State == EntityState.Deleted);

            foreach (var entry in entities)
            {
                var entity = (ISoftDelete)entry.Entity;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }

}
