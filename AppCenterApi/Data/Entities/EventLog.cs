#nullable disable

namespace AppCenterApi.Data.Entities;

public class EventLog
{
    public Guid Id { get; set; }

    public DateTime Timestamp { get; set; }

    public string Sid { get; set; }

    public Guid DeviceId { get; set; }

    public string Type { get; set; }

    public string Name { get; set; }

    public virtual Device Device { get; set; }

    public virtual ICollection<EventProperty> EventProperties { get; set; } = new List<EventProperty>();
}