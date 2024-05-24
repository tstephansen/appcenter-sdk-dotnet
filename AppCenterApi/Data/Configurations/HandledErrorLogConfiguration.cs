using AppCenterApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppCenterApi.Data.Configurations;

public partial class HandledErrorLogConfiguration : IEntityTypeConfiguration<HandledErrorLog>
{
    public void Configure(EntityTypeBuilder<HandledErrorLog> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK__HandledE__3214EC07753C19D9");

        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.Sid)
            .HasMaxLength(255);
        entity.Property(e => e.Timestamp).HasColumnType("datetime");
        entity.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(255);

        entity.HasOne(d => d.Device).WithMany(p => p.HandledErrorLogs)
            .HasForeignKey(d => d.DeviceId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__HandledEr__Devic__31EC6D26");

        entity.HasOne(d => d.Exception).WithMany(p => p.HandledErrorLogs)
            .HasForeignKey(d => d.ExceptionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__HandledEr__Excep__32E0915F");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<HandledErrorLog> entity);
}