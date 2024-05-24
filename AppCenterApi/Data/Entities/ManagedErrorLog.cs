#nullable disable

namespace AppCenterApi.Data.Entities;

public partial class ManagedErrorLog
{
    public Guid Id { get; set; }

    public Guid ExceptionId { get; set; }

    public int ProcessId { get; set; }

    public string ProcessName { get; set; }

    public bool Fatal { get; set; }

    public DateTime AppLaunchTimestamp { get; set; }

    public string Architecture { get; set; }

    public DateTime Timestamp { get; set; }

    public string Sid { get; set; }

    public Guid DeviceId { get; set; }

    public string Type { get; set; }

    public virtual Device Device { get; set; }

    public virtual ExceptionDetail Exception { get; set; }
}