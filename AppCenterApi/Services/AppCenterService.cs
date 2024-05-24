using AppCenterApi.Data;
using AppCenterApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NewMind.Training.Api.Models.AppCenter;

namespace AppCenterApi.Services;

/// <summary>
///     A service for logging AppCenter events and errors.
/// </summary>
public interface IAppCenterService
{
    /// <summary>
    ///     Creates a log from the software.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Returns <c>true</c> if the log was successfully created, <c>false</c> otherwise.</returns>
    Task<bool> LogAsync(LogRequest request);
}

/// <summary>
///     A service for logging AppCenter events and errors.
/// </summary>
/// <seealso cref="AppCenterApi.Services.IAppCenterService"/>
public class AppCenterService : IAppCenterService
{
    private readonly ILogger<AppCenterService> _logger;
    private readonly ErrorsContext _context;
    private const string StartSessionLog = "startSession";
    private const string EventLog = "event";
    private const string StartServiceLog = "startService";
    private const string HandledErrorLog = "handledError";
    private const string ManagedErrorLog = "managedError";

    /// <summary>
    ///     Initializes a new instance of the <see cref="AppCenterService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="context">The context.</param>
    public AppCenterService(ILogger<AppCenterService> logger, ErrorsContext context)
    {
        _logger = logger;
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> LogAsync(LogRequest request)
    {
        try
        {
            var results = new List<bool>();
            foreach (var log in request.Logs)
            {
                switch (log.Type)
                {
                    case StartSessionLog:
                    case StartServiceLog:
                    case EventLog:
                        results.Add(await LogEventAsync(log.ToEventLog()));
                        break;
                    case HandledErrorLog:
                        results.Add(await LogHandledErrorAsync(log.ToHandledErrorLog()));
                        break;
                    case ManagedErrorLog:
                        results.Add(await LogManagedErrorAsync(log.ToManagedErrorLog()));
                        break;
                    default:
                        _logger.LogWarning("Unable to process log, unknown log type {LogType}", log.Type);
                        results.Add(false);
                        break;
                }
            }
            return results.All(c => c);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating the AppCenter log");
            return false;
        }
    }

    #region Internal Methods
    internal async Task<bool> LogEventAsync(EventLogModel log)
    {
        try
        {
            var device = await CreateDeviceAsync(log.Device);
            var eventLog = new EventLog
            {
                Id = Guid.NewGuid(),
                Timestamp = log.Timestamp,
                Sid = log.Sid,
                DeviceId = device.Id,
                Type = log.Type,
                Name = log.Name
            };
            await _context.EventLogs.AddAsync(eventLog);
            foreach (var prop in log.Properties)
            {
                await _context.EventProperties.AddAsync(new EventProperty
                {
                    Id = Guid.NewGuid(),
                    PropertyName = prop.Key,
                    PropertyValue = prop.Value,
                    EventLogId = eventLog.Id
                });
            }
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating the event log");
            return false;
        }
    }

    internal async Task<bool> LogHandledErrorAsync(HandledErrorLogModel log)
    {
        try
        {
            var device = await CreateDeviceAsync(log.Device);
            var exceptionDetail = CreateExceptionDetails(log.Exception);
            var errorLog = new HandledErrorLog
            {
                Id = Guid.NewGuid(),
                Timestamp = log.Timestamp,
                Sid = log.Sid,
                DeviceId = device.Id,
                Type = log.Type,
                Exception = exceptionDetail
            };
            await _context.HandledErrorLogs.AddAsync(errorLog);
            foreach (var prop in log.Properties)
            {
                await _context.EventProperties.AddAsync(new EventProperty
                {
                    Id = Guid.NewGuid(),
                    PropertyName = prop.Key,
                    PropertyValue = prop.Value,
                    HandledErrorId = errorLog.Id
                });
            }
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while logging the handled error");
            return false;
        }
    }

    internal async Task<bool> LogManagedErrorAsync(ManagedErrorLogModel log)
    {
        try
        {
            var device = await CreateDeviceAsync(log.Device);
            var exceptionDetail = CreateExceptionDetails(log.Exception);
            var errorLog = new ManagedErrorLog
            {
                Id = Guid.NewGuid(),
                Exception = exceptionDetail,
                ProcessId = log.ProcessId,
                ProcessName = log.ProcessName,
                Fatal = log.Fatal,
                AppLaunchTimestamp = log.AppLaunchTimestamp,
                Architecture = log.Architecture,
                Timestamp = log.Timestamp,
                Sid = log.Sid,
                DeviceId = device.Id,
                Type = log.Type
            };
            await _context.ManagedErrorLogs.AddAsync(errorLog);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while logging the managed error");
            return false;
        }
    }

    internal async Task<Device> CreateDeviceAsync(DeviceModel deviceModel)
    {
        var deviceId = Guid.NewGuid();
        var device = await _context.Devices.FirstOrDefaultAsync(c =>
            c.SdkName == deviceModel.SdkName &&
            c.SdkVersion == deviceModel.SdkVersion &&
            c.Model == deviceModel.Model &&
            c.OemName == deviceModel.OemName &&
            c.OsName == deviceModel.OsName &&
            c.OsVersion == deviceModel.OsVersion &&
            c.OsBuild == deviceModel.OsBuild &&
            c.Locale == deviceModel.Locale &&
            c.TimeZoneOffset == deviceModel.TimeZoneOffset &&
            c.ScreenSize == deviceModel.ScreenSize &&
            c.AppVersion == deviceModel.AppVersion &&
            c.AppBuild == deviceModel.AppBuild &&
            c.AppNamespace == deviceModel.AppNamespace);
        if (device == null)
        {
            device = new Device
            {
                Id = deviceId,
                SdkName = deviceModel.SdkName,
                SdkVersion = deviceModel.SdkVersion,
                Model = deviceModel.Model,
                OemName = deviceModel.OemName,
                OsName = deviceModel.OsName,
                OsVersion = deviceModel.OsVersion,
                OsBuild = deviceModel.OsBuild,
                Locale = deviceModel.Locale,
                TimeZoneOffset = deviceModel.TimeZoneOffset,
                ScreenSize = deviceModel.ScreenSize,
                AppVersion = deviceModel.AppVersion,
                AppBuild = deviceModel.AppBuild,
                AppNamespace = deviceModel.AppNamespace
            };
            await _context.Devices.AddAsync(device);
        }
        return device;
    }

    internal ExceptionDetail CreateExceptionDetails(ExceptionDetailModel model, ExceptionDetail? ex = null, ManagedErrorLog? managedError = null)
    {
        if (model.Message.Length > 255)
            model.Message = model.Message[..254];
        var detail = new ExceptionDetail
        {
            Id = Guid.NewGuid(),
            Type = model.Type,
            Message = model.Message,
            StackTrace = model.StackTrace,
            ParentExceptionId = ex?.Id
        };
        if (managedError != null)
            detail.ManagedErrorLogs.Add(managedError);
        if (model.InnerExceptions is not { Count: > 0 })
            return detail;
        foreach (var inner in model.InnerExceptions)
        {
            detail.InverseParentException.Add(CreateExceptionDetails(inner, detail));
        }
        return detail;
    }
    #endregion
}