using DocConnect.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocConnect.Data.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.AccessFailedCount).HasColumnName("access_failed_count");
            builder.Property(e => e.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            builder.Property(e => e.CreatedAt).HasColumnName("created_at");
            builder.Property(e => e.DoctorId).HasColumnName("doctor_id");
            builder.Property(e => e.Email).HasColumnName("email");
            builder.Property(e => e.EmailConfirmed).HasColumnName("email_is_confirmed");
            builder.Property(e => e.FirstName).HasColumnName("first_name");
            builder.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            builder.Property(e => e.Verified).HasColumnName("is_verified");
            builder.Property(e => e.LastName).HasColumnName("last_name");
            builder.Property(e => e.LockoutEnd).HasColumnName("lockout_end");
            builder.Property(e => e.LockoutEnabled).HasColumnName("lockout_is_enabled");
            builder.Property(e => e.NormalizedEmail).HasColumnName("normalized_email");
            builder.Property(e => e.NormalizedUserName).HasColumnName("normalized_username");
            builder.Property(e => e.PasswordHash).HasColumnName("password_hash");
            builder.Property(e => e.PhoneNumber).HasColumnName("phone_number");
            builder.Property(e => e.PhoneNumberConfirmed).HasColumnName("phone_number_is_confirmed");
            builder.Property(e => e.SecurityStamp).HasColumnName("security_stamp");
            builder.Property(e => e.TwoFactorEnabled).HasColumnName("two_factor_is_enabled");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.UserName).HasColumnName("username");
        }
    }
}
