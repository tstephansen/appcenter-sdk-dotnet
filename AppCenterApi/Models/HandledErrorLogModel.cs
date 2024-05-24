using System;
using System.Collections.Generic;

namespace NewMind.Training.Api.Models.AppCenter;

public class HandledErrorLogModel : LogBase
{
    public Guid Id { get; set; }
    public ExceptionDetailModel Exception { get; set; }
    public Dictionary<string, string> Properties { get; set; } = new();
}