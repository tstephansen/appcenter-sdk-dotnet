using AppCenterApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppCenterApi.Data.Configurations;

public partial class ExceptionDetailConfiguration : IEntityTypeConfiguration<ExceptionDetail>
{
    public void Configure(EntityTypeBuilder<ExceptionDetail> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK__Exceptio__3214EC07CB45A765");

        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.Message).HasMaxLength(255);
        entity.Property(e => e.StackTrace);
        entity.Property(e => e.Type).HasMaxLength(255);

        entity.HasOne(d => d.ParentException).WithMany(p => p.InverseParentException)
            .HasForeignKey(d => d.ParentExceptionId)
            .HasConstraintName("FK__Exception__Paren__30F848ED");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<ExceptionDetail> entity);
}