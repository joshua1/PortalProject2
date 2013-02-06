using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PortalProject2.Models.Mapping
{
    public class MessageNumberMap : EntityTypeConfiguration<MessageNumber>
    {
        public MessageNumberMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("MessageNumber");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MessageId).HasColumnName("MessageId");
            this.Property(t => t.DeviceId).HasColumnName("DeviceId");

            // Relationships
            this.HasRequired(t => t.Device)
                .WithMany(t => t.MessageNumbers)
                .HasForeignKey(d => d.DeviceId);
            this.HasRequired(t => t.Message)
                .WithMany(t => t.MessageNumbers)
                .HasForeignKey(d => d.MessageId);

        }
    }
}
