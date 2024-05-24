#nullable disable

namespace AppCenterApi.Data.Entities;

public class Device
{
    public Guid Id { get; set; }

    public string SdkName { get; set; }

    public string SdkVersion { get; set; }

    public string Model { get; set; }

    public string OemName { get; set; }

    public string OsName { get; set; }

    public string OsVersion { get; set; }

    public string OsBuild { get; set; }

    public string Locale { get; set; }

    public int TimeZoneOffset { get; set; }

    public string ScreenSize { get; set; }

    public string AppVersion { get; set; }

    public string AppBuild { get; set; }

    public string AppNamespace { get; set; }

    public virtual ICollection<EventLog> EventLogs { get; set; } = new List<EventLog>();

    public virtual ICollection<HandledErrorLog> HandledErrorLogs { get; set; } = new List<HandledErrorLog>();

    public virtual ICollection<ManagedErrorLog> ManagedErrorLogs { get; set; } = new List<ManagedErrorLog>();
}