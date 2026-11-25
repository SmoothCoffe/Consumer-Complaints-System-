using consumer_complaints.Models;
using Microsoft.EntityFrameworkCore;
namespace consumer_complaints.Data;

public class ComplaintsDbContext :DbContext

{
    public ComplaintsDbContext(DbContextOptions<ComplaintsDbContext> options)        : base(options)
    {

    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Complaint> Complaints => Set<Complaint>();
    public DbSet<ComplaintAssignment> ComplaintAssignments => Set<ComplaintAssignment>();
    public DbSet<ComplaintMessage> ComplaintMessages => Set<ComplaintMessage>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<SlaConfig> SlaConfigs => Set<SlaConfig>();
    public DbSet<EscalationLog> EscalationLogs => Set<EscalationLog>();
    public DbSet<EmailOutbox> EmailOutbox => Set<EmailOutbox>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureEnums(modelBuilder);
        ConfigureCustomer(modelBuilder);
        ConfigureDepartment(modelBuilder);
        ConfigureUser(modelBuilder);
        ConfigureSlaConfig(modelBuilder);
        ConfigureComplaint(modelBuilder);
        ConfigureAssignment(modelBuilder);
        ConfigureMessage(modelBuilder);
        ConfigureEscalationLog(modelBuilder);
        ConfigureEmailOutbox(modelBuilder);
        ConfigureNotification(modelBuilder);
    }

    private static void ConfigureEnums(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>().Property(u => u.Role)
                    .HasConversion<string>().HasMaxLength(20);

        modelBuilder.Entity<Complaint>().Property(c => c.Status)
            .HasConversion<string>().HasMaxLength(20);

        modelBuilder.Entity<Complaint>().Property(c => c.ClosureType)
            .HasConversion<string>().HasMaxLength(20);

        modelBuilder.Entity<ComplaintAssignment>().Property(a => a.Status)
            .HasConversion<string>().HasMaxLength(20);

        modelBuilder.Entity<ComplaintMessage>().Property(m => m.SenderType)
            .HasConversion<string>().HasMaxLength(20);

        modelBuilder.Entity<EmailOutbox>().Property(e => e.Template)
            .HasConversion<string>().HasMaxLength(50);

        modelBuilder.Entity<EmailOutbox>().Property(e => e.Status)
            .HasConversion<string>().HasMaxLength(20);

        modelBuilder.Entity<Notification>().Property(n => n.Type)
            .HasConversion<string>().HasMaxLength(30);
    }
    private static void ConfigureCustomer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(c => c.PhoneNormalized);
            entity.HasIndex(c => c.Email);
        });
    }
    private static void ConfigureDepartment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasIndex(d => d.Code).IsUnique().HasFilter("[code] IS NOT NULL");
            entity.HasIndex(d => d.ManagerId);

            entity.HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(d => d.EscalationManager)
                .WithMany()
                .HasForeignKey(d => d.EscalationManagerId)
                .OnDelete(DeleteBehavior.NoAction);
        });
            }
    private static void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasIndex(u => u.Username).IsUnique();
            entity.HasIndex(u => u.Email).IsUnique().HasFilter("[email] IS NOT NULL");
            entity.HasIndex(u => new { u.DepartmentId, u.Role, u.IsAssignable });

            entity.HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    private static void ConfigureSlaConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SlaConfig>(entity =>
        {
            entity.HasIndex(s => s.DepartmentId).IsUnique();

            entity.HasOne(s => s.Department)
                .WithMany(d => d.SlaConfigs)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
    private static void ConfigureComplaint(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasIndex(c => c.ReferenceNumber).IsUnique();
            entity.HasIndex(c => new { c.Status, c.SlaDeadline });
            entity.HasIndex(c => new { c.DepartmentId, c.Status });
            entity.HasIndex(c => new { c.CustomerId, c.ReportedAt });
            entity.HasIndex(c => c.CurrentEscalatedToUserId);

            entity.Property(c => c.RowVersion).IsRowVersion();

            entity.HasOne(c => c.Customer)
                .WithMany(cu => cu.Complaints)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Department)
                .WithMany(d => d.Complaints)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.SlaConfig)
                .WithMany()
                .HasForeignKey(c => c.SlaConfigId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(c => c.ClosedByUser)
                .WithMany()
                .HasForeignKey(c => c.ClosedByUserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(c => c.CurrentEscalatedToUser)
                .WithMany()
                .HasForeignKey(c => c.CurrentEscalatedToUserId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    private static void ConfigureAssignment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComplaintAssignment>(entity =>
        {
            entity.HasIndex(a => a.ComplaintId)
                .IsUnique()
                .HasFilter("[ended_at] IS NULL");

            entity.HasIndex(a => new { a.AssignedToUserId, a.EndedAt });

            entity.HasOne(a => a.Complaint)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.AssignedToUser)
                .WithMany(u => u.AssignmentsReceived)
                .HasForeignKey(a => a.AssignedToUserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(a => a.AssignedByUser)
                .WithMany(u => u.AssignmentsMade)
                .HasForeignKey(a => a.AssignedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    private static void ConfigureMessage(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComplaintMessage>(entity =>
        {
            entity.HasIndex(m => new { m.ComplaintId, m.CreatedAt });

            entity.ToTable(t => t.HasCheckConstraint(
                "CK_complaint_messages_sender",
                "([sender_type] = 'Customer' AND [sender_user_id] IS NULL) OR " +
                "([sender_type] <> 'Customer' AND [sender_user_id] IS NOT NULL)"));

            entity.HasOne(m => m.Complaint)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(m => m.SenderUser)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderUserId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    private static void ConfigureEscalationLog(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EscalationLog>(entity =>
        {
            entity.HasIndex(e => new { e.ComplaintId, e.EscalatedAt });
            entity.HasIndex(e => new { e.ToUserId, e.AcknowledgedAt });

            entity.HasOne(e => e.Complaint)
                .WithMany(c => c.EscalationLogs)
                .HasForeignKey(e => e.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.FromUser)
                .WithMany()
                .HasForeignKey(e => e.FromUserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(e => e.ToUser)
                .WithMany(u => u.EscalationsReceived)
                .HasForeignKey(e => e.ToUserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(e => e.AcknowledgedByUser)
                .WithMany()
                .HasForeignKey(e => e.AcknowledgedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    private static void ConfigureEmailOutbox(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailOutbox>(entity =>
        {
            entity.HasIndex(e => new { e.Status, e.NextAttemptAt });

            entity.HasOne(e => e.RelatedComplaint)
                .WithMany()
                .HasForeignKey(e => e.RelatedComplaintId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }

    private static void ConfigureNotification(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasIndex(n => new { n.UserId, n.IsRead, n.CreatedAt });

            entity.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(n => n.RelatedComplaint)
                .WithMany()
                .HasForeignKey(n => n.RelatedComplaintId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }


}
