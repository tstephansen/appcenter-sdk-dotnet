#nullable disable

namespace AppCenterApi.Data.Entities;

public partial class ExceptionDetail
{
    public Guid Id { get; set; }

    public string Type { get; set; }

    public string Message { get; set; }

    public string StackTrace { get; set; }

    public Guid? ParentExceptionId { get; set; }

    public virtual ICollection<HandledErrorLog> HandledErrorLogs { get; set; } = new List<HandledErrorLog>();

    public virtual ICollection<ExceptionDetail> InverseParentException { get; set; } = new List<ExceptionDetail>();

    public virtual ICollection<ManagedErrorLog> ManagedErrorLogs { get; set; } = new List<ManagedErrorLog>();

    public virtual ExceptionDetail ParentException { get; set; }
}