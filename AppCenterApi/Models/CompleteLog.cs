using System;
using System.Collections.Generic;

namespace NewMind.Training.Api.Models.AppCenter;

public class CompleteLog
{
    // Base Data
    public DateTime Timestamp { get; set; }
    public string Sid { get; set; }
    public DeviceModel Device { get; set; }
    public string Type { get; set; }
    // Managed Error Log
    public ExceptionDetailModel Exception { get; set; }
    public string Id { get; set; }
    public int ProcessId { get; set; }
    public string ProcessName { get; set; }
    public bool Fatal { get; set; }
    public DateTime AppLaunchTimestamp { get; set; }
    public string Architecture { get; set; }
    // Handled Error Log
    public Dictionary<string, string> Properties { get; set; } = new();
    // Event Log Model
    public string Name { get; set; }

    public ManagedErrorLogModel ToManagedErrorLog()
    {
        return new ManagedErrorLogModel
        {
            Timestamp = Timestamp,
            Sid = Sid,
            Device = Device,
            Type = Type,
            Id = Id,
            Exception = Exception,
            ProcessId = ProcessId,
            ProcessName = ProcessName,
            Fatal = Fatal,
            AppLaunchTimestamp = AppLaunchTimestamp,
            Architecture = Architecture
        };
    }
        
    public HandledErrorLogModel ToHandledErrorLog()
    {
        return new HandledErrorLogModel
        {
            Timestamp = Timestamp,
            Sid = Sid,
            Device = Device,
            Type = Type,
            Id = Guid.Parse(Id),
            Exception = Exception,
            Properties = Properties
        };
    }

    public EventLogModel ToEventLog()
    {
        return new EventLogModel
        {
            Timestamp = Timestamp,
            Sid = Sid,
            Device = Device,
            Type = Type,
            Id = Id,
            Name = Name,
            Properties = Properties
        };
    }
}