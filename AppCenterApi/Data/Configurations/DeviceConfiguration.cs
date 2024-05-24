using AppCenterApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppCenterApi.Data.Configurations;

public partial class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK__Devices__3214EC07B8F1C2B7");

        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.AppBuild).HasMaxLength(255);
        entity.Property(e => e.AppNamespace).HasMaxLength(255);
        entity.Property(e => e.AppVersion).HasMaxLength(255);
        entity.Property(e => e.Locale).HasMaxLength(255);
        entity.Property(e => e.Model).HasMaxLength(255);
        entity.Property(e => e.OemName).HasMaxLength(255);
        entity.Property(e => e.OsBuild).HasMaxLength(255);
        entity.Property(e => e.OsName).HasMaxLength(255);
        entity.Property(e => e.OsVersion).HasMaxLength(255);
        entity.Property(e => e.ScreenSize).HasMaxLength(255);
        entity.Property(e => e.SdkName).HasMaxLength(255);
        entity.Property(e => e.SdkVersion).HasMaxLength(50);

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Device> entity);
}