namespace consumer_complaints.Models
{
    public enum UserRole
    {
        Admin,
        Manager,
        Staff
    }

    public enum ComplaintStatus
    {
        New,
        Assigned,
        InProgress,
        NeedsCustomerInfo,
        Escalated,
        Closed,
        Cancelled
    }
    public enum AssignmentStatus
    {
        Pending,
        Accepted,
        Completed,
        Reassigned
    }
    public enum SenderType
    {
        Customer,
        Manager,
        Staff,
        System
    }
    public enum ClosureType
    {
        Resolved,
        Rejected,
        Duplicated,
        NoAction
    }
    public enum NotificationType
    {
        Assigned,
        Escalated,
        InfoRequested,
        Closed,
        System
    }
    public enum EmailStatus
    {
        Pending,
        Sent,
        Failed
    }

    public enum EmailTemplate
    {
        ComplaintReceived,
        InfoRequested,
        ComplaintClosed
    }
}
