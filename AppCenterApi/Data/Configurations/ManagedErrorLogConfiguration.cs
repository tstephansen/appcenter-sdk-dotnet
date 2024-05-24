using AppCenterApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppCenterApi.Data.Configurations;

public partial class ManagedErrorLogConfiguration : IEntityTypeConfiguration<ManagedErrorLog>
{
    public void Configure(EntityTypeBuilder<ManagedErrorLog> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK__ManagedE__3214EC077437F4FD");

        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.AppLaunchTimestamp).HasColumnType("datetime");
        entity.Property(e => e.Architecture).HasMaxLength(255);
        entity.Property(e => e.ProcessName).HasMaxLength(255);
        entity.Property(e => e.Sid)
            .HasMaxLength(255);
        entity.Property(e => e.Timestamp).HasColumnType("datetime");
        entity.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(255);

        entity.HasOne(d => d.Device).WithMany(p => p.ManagedErrorLogs)
            .HasForeignKey(d => d.DeviceId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__ManagedEr__Devic__33D4B598");

        entity.HasOne(d => d.Exception).WithMany(p => p.ManagedErrorLogs)
            .HasForeignKey(d => d.ExceptionId)
            .HasConstraintName("FK__ManagedEr__Excep__34C8D9D1");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<ManagedErrorLog> entity);
}