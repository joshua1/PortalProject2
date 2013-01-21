using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PortalProject2.Models.Mapping
{
    public class UserAuthMap : EntityTypeConfiguration<UserAuth>
    {
        public UserAuthMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserName)
                .HasMaxLength(8000);

            this.Property(t => t.Email)
                .HasMaxLength(8000);

            this.Property(t => t.PrimaryEmail)
                .HasMaxLength(8000);

            this.Property(t => t.FirstName)
                .HasMaxLength(8000);

            this.Property(t => t.LastName)
                .HasMaxLength(8000);

            this.Property(t => t.DisplayName)
                .HasMaxLength(8000);

            this.Property(t => t.BirthDateRaw)
                .HasMaxLength(8000);

            this.Property(t => t.Country)
                .HasMaxLength(8000);

            this.Property(t => t.Culture)
                .HasMaxLength(8000);

            this.Property(t => t.FullName)
                .HasMaxLength(8000);

            this.Property(t => t.Gender)
                .HasMaxLength(8000);

            this.Property(t => t.Language)
                .HasMaxLength(8000);

            this.Property(t => t.MailAddress)
                .HasMaxLength(8000);

            this.Property(t => t.Nickname)
                .HasMaxLength(8000);

            this.Property(t => t.PostalCode)
                .HasMaxLength(8000);

            this.Property(t => t.TimeZone)
                .HasMaxLength(8000);

            this.Property(t => t.Salt)
                .HasMaxLength(8000);

            this.Property(t => t.PasswordHash)
                .HasMaxLength(8000);

            this.Property(t => t.DigestHA1Hash)
                .HasMaxLength(8000);

            this.Property(t => t.Roles)
                .HasMaxLength(8000);

            this.Property(t => t.Permissions)
                .HasMaxLength(8000);

            this.Property(t => t.RefIdStr)
                .HasMaxLength(8000);

            this.Property(t => t.Meta)
                .HasMaxLength(8000);

            // Table & Column Mappings
            this.ToTable("UserAuth");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.PrimaryEmail).HasColumnName("PrimaryEmail");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.BirthDateRaw).HasColumnName("BirthDateRaw");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.Culture).HasColumnName("Culture");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Language).HasColumnName("Language");
            this.Property(t => t.MailAddress).HasColumnName("MailAddress");
            this.Property(t => t.Nickname).HasColumnName("Nickname");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.TimeZone).HasColumnName("TimeZone");
            this.Property(t => t.Salt).HasColumnName("Salt");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.DigestHA1Hash).HasColumnName("DigestHA1Hash");
            this.Property(t => t.Roles).HasColumnName("Roles");
            this.Property(t => t.Permissions).HasColumnName("Permissions");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.RefId).HasColumnName("RefId");
            this.Property(t => t.RefIdStr).HasColumnName("RefIdStr");
            this.Property(t => t.Meta).HasColumnName("Meta");
        }
    }
}
