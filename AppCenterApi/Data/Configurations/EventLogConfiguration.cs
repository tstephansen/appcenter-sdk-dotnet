using AppCenterApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppCenterApi.Data.Configurations;

public partial class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
{
    public void Configure(EntityTypeBuilder<EventLog> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK__EventLog__3214EC074FAE5E2B");

        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.Name).HasMaxLength(255);
        entity.Property(e => e.Sid)
            .HasMaxLength(255);
        entity.Property(e => e.Timestamp).HasColumnType("datetime");
        entity.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(255);

        entity.HasOne(d => d.Device).WithMany(p => p.EventLogs)
            .HasForeignKey(d => d.DeviceId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__EventLogs__Devic__2E1BDC42");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<EventLog> entity);
}