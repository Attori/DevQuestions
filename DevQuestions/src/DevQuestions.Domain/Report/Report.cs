namespace DevQuestions.Domain.Report;

public class Report
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ReportedUserId { get; set; }
    public required string Reason { get; set; }
    public ReportStatus ReportStatus { get; set; } = ReportStatus.Open;
    public DateTime Created { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? ResolvedByUserId { get; set; }
}