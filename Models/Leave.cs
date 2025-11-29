using HMS.Data;

namespace HMS.Models;

public class Leave
{
    public int Id { get; set; }
    public int StaffId { get; set; }

    // Leave Details
    public LeaveType LeaveType { get; set; } // Enum
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalDays { get; set; } // Support half-days
    public string Reason { get; set; }
    public string? AttachmentUrl { get; set; } // For sick notes

    // Status
    public LeaveStatus Status { get; set; } // Enum

    // Approval
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string? ApprovalNotes { get; set; }
    public string? RejectionReason { get; set; }

    // Timestamps
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }

    // Navigation
    public Staff Staff { get; set; }
    public ApplicationUser? ApproverUser { get; set; }
}

public enum LeaveType
{
    Vacation,      // Semester
    Sick,          // Sjukfrånvaro
    VAB,           // Vård av barn
    Parental,      // Föräldraledighet
    Unpaid,        // Obetald ledighet
    Other
}

public enum LeaveStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled
}