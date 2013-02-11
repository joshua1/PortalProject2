using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PortalProject2.Models.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.MessageId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MessageStatus)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MessageText)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Message");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MessageId).HasColumnName("MessageId");
            this.Property(t => t.StatusCode).HasColumnName("StatusCode");
            this.Property(t => t.MessageStatus).HasColumnName("MessageStatus");
            this.Property(t => t.DateSent).HasColumnName("DateSent");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.MessageText).HasColumnName("MessageText");
        }
    }
}
