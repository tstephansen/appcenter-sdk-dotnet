using System;

namespace NewMind.Training.Api.Models.AppCenter;

public class LogBase
{
    public DateTime Timestamp { get; set; }
    public string Sid { get; set; }
    public DeviceModel Device { get; set; }
    public string Type { get; set; }
}