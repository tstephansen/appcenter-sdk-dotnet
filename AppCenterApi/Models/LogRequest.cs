using System.Collections.Generic;

namespace NewMind.Training.Api.Models.AppCenter;

public class LogRequest
{
    public List<CompleteLog> Logs { get; set; } = new();
}