#nullable disable

namespace AppCenterApi.Data.Entities;

public partial class HandledErrorLog
{
    public Guid Id { get; set; }

    public Guid ExceptionId { get; set; }

    public DateTime Timestamp { get; set; }

    public string Sid { get; set; }

    public Guid DeviceId { get; set; }

    public string Type { get; set; }

    public virtual Device Device { get; set; }

    public virtual ICollection<EventProperty> EventProperties { get; set; } = new List<EventProperty>();

    public virtual ExceptionDetail Exception { get; set; }
}