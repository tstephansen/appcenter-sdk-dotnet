#nullable disable
using AppCenterApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppCenterApi.Data;

public partial class ErrorsContext : DbContext
{
    private readonly ILogger<ErrorsContext> _logger;

    public ErrorsContext(DbContextOptions<ErrorsContext> options, ILogger<ErrorsContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    #region DbSets
    public virtual DbSet<Device> Devices { get; set; }
    public virtual DbSet<EventLog> EventLogs { get; set; }
    public virtual DbSet<EventProperty> EventProperties { get; set; }
    public virtual DbSet<ExceptionDetail> ExceptionDetails { get; set; }
    public virtual DbSet<HandledErrorLog> HandledErrorLogs { get; set; }
    public virtual DbSet<ManagedErrorLog> ManagedErrorLogs { get; set; }
    #endregion

    #region OnConfiguring
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ErrorLogs;Integrated Security=True;Encrypt=True");
    }
    #endregion

    #region OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.DeviceConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.EventLogConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.EventPropertyConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ExceptionDetailConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.HandledErrorLogConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.ManagedErrorLogConfiguration());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    #endregion

    #region Overrides and Customizations
    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "DbUpdateConcurrencyException occurred while trying to save changes");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "DbUpdateException occurred while trying to save changes");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving changes to the local db");
        }
        return 0;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "DbUpdateConcurrencyException occurred while trying to save changes");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "DbUpdateException occurred while trying to save changes");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving changes to the local db");
        }
        return 0;
    }
    #endregion
}