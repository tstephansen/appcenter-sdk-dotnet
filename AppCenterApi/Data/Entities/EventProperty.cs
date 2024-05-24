#nullable disable
namespace AppCenterApi.Data.Entities;

public class EventProperty
{
    public Guid Id { get; set; }

    public string PropertyName { get; set; }

    public string PropertyValue { get; set; }

    public Guid? HandledErrorId { get; set; }

    public Guid? EventLogId { get; set; }

    public virtual EventLog EventLog { get; set; }

    public virtual HandledErrorLog HandledError { get; set; }
}