namespace NewMind.Training.Api.Models.AppCenter;

public class DeviceModel
{
    public string SdkName { get; set; } = null!;
    public string SdkVersion { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string OemName { get; set; } = null!;
    public string OsName { get; set; } = null!;
    public string OsVersion { get; set; } = null!;
    public string OsBuild { get; set; } = null!;
    public string Locale { get; set; } = null!;
    public int TimeZoneOffset { get; set; }
    public string ScreenSize { get; set; } = null!;
    public string AppVersion { get; set; } = null!;
    public string AppBuild { get; set; } = null!;
    public string AppNamespace { get; set; } = null!;
}