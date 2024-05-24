using System.Collections.Generic;

namespace NewMind.Training.Api.Models.AppCenter;

public class EventLogModel : LogBase
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, string> Properties { get; set; }
}