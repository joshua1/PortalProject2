using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PortalProject2.Models.Mapping
{
    public class DeviceMap : EntityTypeConfiguration<Device>
    {
        public DeviceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DeviceToken)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.DevicePhoneNumber)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.DeviceModel)
                .HasMaxLength(50);

            this.Property(t => t.DeviceVersion)
                .HasMaxLength(50);

            this.Property(t => t.DevicePlatform)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Device");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DeviceToken).HasColumnName("DeviceToken");
            this.Property(t => t.DevicePhoneNumber).HasColumnName("DevicePhoneNumber");
            this.Property(t => t.DeviceModel).HasColumnName("DeviceModel");
            this.Property(t => t.DeviceVersion).HasColumnName("DeviceVersion");
            this.Property(t => t.DevicePlatform).HasColumnName("DevicePlatform");
        }
    }
}
