using System;

namespace NewMind.Training.Api.Models.AppCenter;

public class ManagedErrorLogModel : LogBase
{
    public ExceptionDetailModel Exception { get; set; }
    public string Id { get; set; }
    public int ProcessId { get; set; }
    public string ProcessName { get; set; }
    public bool Fatal { get; set; }
    public DateTime AppLaunchTimestamp { get; set; }
    public string Architecture { get; set; }
}