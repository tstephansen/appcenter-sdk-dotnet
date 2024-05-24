using AppCenterApi.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AppCenterApi.Data.Configurations;

public partial class EventPropertyConfiguration : IEntityTypeConfiguration<EventProperty>
{
    public void Configure(EntityTypeBuilder<EventProperty> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK__EventPro__3214EC07CA1148E6");

        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.PropertyName)
            .IsRequired()
            .HasMaxLength(255)
            .IsUnicode(false);
        entity.Property(e => e.PropertyValue)
            .IsRequired()
            .HasMaxLength(255);

        entity.HasOne(d => d.EventLog).WithMany(p => p.EventProperties)
            .HasForeignKey(d => d.EventLogId)
            .HasConstraintName("FK_EventProperties_EventLogs");

        entity.HasOne(d => d.HandledError).WithMany(p => p.EventProperties)
            .HasForeignKey(d => d.HandledErrorId)
            .HasConstraintName("FK_EventProperties_HandledErrorLogs");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<EventProperty> entity);
}