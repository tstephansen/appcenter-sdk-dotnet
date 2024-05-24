using System.Collections.Generic;

namespace NewMind.Training.Api.Models.AppCenter;

public class ExceptionDetailModel
{
    public string Type { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string? StackTrace { get; set; }
    public List<ExceptionDetailModel> InnerExceptions { get; set; } = new();
}