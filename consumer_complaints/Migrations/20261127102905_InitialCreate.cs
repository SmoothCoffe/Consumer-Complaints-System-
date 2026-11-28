using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace consumer_complaints.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers_tb",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    phone_normalized = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers_tb", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "complaint_assignments_tb",
                columns: table => new
                {
                    assignment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    complaint_id = table.Column<int>(type: "int", nullable: false),
                    assigned_to_user_id = table.Column<int>(type: "int", nullable: false),
                    assigned_by_user_id = table.Column<int>(type: "int", nullable: true),
                    assigned_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    accepted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ended_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    delay_reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complaint_assignments_tb", x => x.assignment_id);
                });

            migrationBuilder.CreateTable(
                name: "complaint_messages_tb",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    complaint_id = table.Column<int>(type: "int", nullable: false),
                    sender_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    sender_user_id = table.Column<int>(type: "int", nullable: true),
                    body = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    is_internal = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    read_by_customer_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    read_by_staff_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complaint_messages_tb", x => x.message_id);
                    table.CheckConstraint("CK_complaint_messages_sender", "([sender_type] = 'Customer' AND [sender_user_id] IS NULL) OR ([sender_type] <> 'Customer' AND [sender_user_id] IS NOT NULL)");
                });

            migrationBuilder.CreateTable(
                name: "complaints_tb",
                columns: table => new
                {
                    complaint_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reference_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    department_id = table.Column<int>(type: "int", nullable: false),
                    created_by_user_id = table.Column<int>(type: "int", nullable: true),
                    subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    related_item = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    reported_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sla_config_id = table.Column<int>(type: "int", nullable: true),
                    sla_deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sla_paused_minutes = table.Column<int>(type: "int", nullable: false),
                    info_requested_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    customer_responded_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    escalation_level = table.Column<byte>(type: "tinyint", nullable: false),
                    current_escalated_to_user_id = table.Column<int>(type: "int", nullable: true),
                    escalated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    closed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    closed_by_user_id = table.Column<int>(type: "int", nullable: true),
                    closure_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    resolution_notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    time_taken_minutes = table.Column<int>(type: "int", nullable: true),
                    row_version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_complaints_tb", x => x.complaint_id);
                    table.ForeignKey(
                        name: "FK_complaints_tb_customers_tb_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers_tb",
                        principalColumn: "customer_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "email_outbox_tb",
                columns: table => new
                {
                    email_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    to_email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    body_html = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    template = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    related_complaint_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    attempts = table.Column<int>(type: "int", nullable: false),
                    last_error = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    next_attempt_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sent_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_outbox_tb", x => x.email_id);
                    table.ForeignKey(
                        name: "FK_email_outbox_tb_complaints_tb_related_complaint_id",
                        column: x => x.related_complaint_id,
                        principalTable: "complaints_tb",
                        principalColumn: "complaint_id");
                });

            migrationBuilder.CreateTable(
                name: "departments_tb",
                columns: table => new
                {
                    department_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    manager_id = table.Column<int>(type: "int", nullable: true),
                    escalation_manager_id = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments_tb", x => x.department_id);
                });

            migrationBuilder.CreateTable(
                name: "sla_configs_tb",
                columns: table => new
                {
                    sla_config_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    department_id = table.Column<int>(type: "int", nullable: false),
                    target_resolution_minutes = table.Column<int>(type: "int", nullable: false),
                    escalation_grace_minutes = table.Column<int>(type: "int", nullable: false),
                    max_escalation_level = table.Column<byte>(type: "tinyint", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sla_configs_tb", x => x.sla_config_id);
                    table.ForeignKey(
                        name: "FK_sla_configs_tb_departments_tb_department_id",
                        column: x => x.department_id,
                        principalTable: "departments_tb",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users_tb",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    department_id = table.Column<int>(type: "int", nullable: true),
                    role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    is_assignable = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_login_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    failed_login_count = table.Column<int>(type: "int", nullable: false),
                    lockout_end = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_tb", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_users_tb_departments_tb_department_id",
                        column: x => x.department_id,
                        principalTable: "departments_tb",
                        principalColumn: "department_id");
                });

            migrationBuilder.CreateTable(
                name: "escalation_logs_tb",
                columns: table => new
                {
                    escalation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplaintId = table.Column<int>(type: "int", nullable: false),
                    from_user_id = table.Column<int>(type: "int", nullable: true),
                    to_user_id = table.Column<int>(type: "int", nullable: false),
                    level = table.Column<byte>(type: "tinyint", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    escalated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    acknowledged_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    acknowledged_by_user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_escalation_logs_tb", x => x.escalation_id);
                    table.ForeignKey(
                        name: "FK_escalation_logs_tb_complaints_tb_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "complaints_tb",
                        principalColumn: "complaint_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_escalation_logs_tb_users_tb_acknowledged_by_user_id",
                        column: x => x.acknowledged_by_user_id,
                        principalTable: "users_tb",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_escalation_logs_tb_users_tb_from_user_id",
                        column: x => x.from_user_id,
                        principalTable: "users_tb",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_escalation_logs_tb_users_tb_to_user_id",
                        column: x => x.to_user_id,
                        principalTable: "users_tb",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "notifications_tb",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    related_complaint_id = table.Column<int>(type: "int", nullable: true),
                    is_read = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    read_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications_tb", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_notifications_tb_complaints_tb_related_complaint_id",
                        column: x => x.related_complaint_id,
                        principalTable: "complaints_tb",
                        principalColumn: "complaint_id");
                    table.ForeignKey(
                        name: "FK_notifications_tb_users_tb_user_id",
                        column: x => x.user_id,
                        principalTable: "users_tb",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_complaint_assignments_tb_assigned_by_user_id",
                table: "complaint_assignments_tb",
                column: "assigned_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_complaint_assignments_tb_assigned_to_user_id_ended_at",
                table: "complaint_assignments_tb",
                columns: new[] { "assigned_to_user_id", "ended_at" });

            migrationBuilder.CreateIndex(
                name: "IX_complaint_assignments_tb_complaint_id",
                table: "complaint_assignments_tb",
                column: "complaint_id",
                unique: true,
                filter: "[ended_at] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_complaint_messages_tb_complaint_id_created_at",
                table: "complaint_messages_tb",
                columns: new[] { "complaint_id", "created_at" });

            migrationBuilder.CreateIndex(
                name: "IX_complaint_messages_tb_sender_user_id",
                table: "complaint_messages_tb",
                column: "sender_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_tb_closed_by_user_id",
                table: "complaints_tb",
                column: "closed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_tb_created_by_user_id",
                table: "complaints_tb",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_tb_current_escalated_to_user_id",
                table: "complaints_tb",
                column: "current_escalated_to_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_tb_customer_id_reported_at",
                table: "complaints_tb",
                columns: new[] { "customer_id", "reported_at" });

            migrationBuilder.CreateIndex(
                name: "IX_complaints_tb_department_id_status",
                table: "complaints_tb",
                columns: new[] { "department_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_complaints_tb_reference_number",
                table: "complaints_tb",
                column: "reference_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_complaints_tb_sla_config_id",
                table: "complaints_tb",
                column: "sla_config_id");

            migrationBuilder.CreateIndex(
                name: "IX_complaints_tb_status_sla_deadline",
                table: "complaints_tb",
                columns: new[] { "status", "sla_deadline" });

            migrationBuilder.CreateIndex(
                name: "IX_customers_tb_email",
                table: "customers_tb",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_customers_tb_phone_normalized",
                table: "customers_tb",
                column: "phone_normalized");

            migrationBuilder.CreateIndex(
                name: "IX_departments_tb_code",
                table: "departments_tb",
                column: "code",
                unique: true,
                filter: "[code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_departments_tb_escalation_manager_id",
                table: "departments_tb",
                column: "escalation_manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_departments_tb_manager_id",
                table: "departments_tb",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_email_outbox_tb_related_complaint_id",
                table: "email_outbox_tb",
                column: "related_complaint_id");

            migrationBuilder.CreateIndex(
                name: "IX_email_outbox_tb_status_next_attempt_at",
                table: "email_outbox_tb",
                columns: new[] { "status", "next_attempt_at" });

            migrationBuilder.CreateIndex(
                name: "IX_escalation_logs_tb_acknowledged_by_user_id",
                table: "escalation_logs_tb",
                column: "acknowledged_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_escalation_logs_tb_ComplaintId_escalated_at",
                table: "escalation_logs_tb",
                columns: new[] { "ComplaintId", "escalated_at" });

            migrationBuilder.CreateIndex(
                name: "IX_escalation_logs_tb_from_user_id",
                table: "escalation_logs_tb",
                column: "from_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_escalation_logs_tb_to_user_id_acknowledged_at",
                table: "escalation_logs_tb",
                columns: new[] { "to_user_id", "acknowledged_at" });

            migrationBuilder.CreateIndex(
                name: "IX_notifications_tb_related_complaint_id",
                table: "notifications_tb",
                column: "related_complaint_id");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_tb_user_id_is_read_created_at",
                table: "notifications_tb",
                columns: new[] { "user_id", "is_read", "created_at" });

            migrationBuilder.CreateIndex(
                name: "IX_sla_configs_tb_department_id",
                table: "sla_configs_tb",
                column: "department_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_tb_department_id_role_is_assignable",
                table: "users_tb",
                columns: new[] { "department_id", "role", "is_assignable" });

            migrationBuilder.CreateIndex(
                name: "IX_users_tb_email",
                table: "users_tb",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_tb_username",
                table: "users_tb",
                column: "username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_complaint_assignments_tb_complaints_tb_complaint_id",
                table: "complaint_assignments_tb",
                column: "complaint_id",
                principalTable: "complaints_tb",
                principalColumn: "complaint_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_complaint_assignments_tb_users_tb_assigned_by_user_id",
                table: "complaint_assignments_tb",
                column: "assigned_by_user_id",
                principalTable: "users_tb",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_complaint_assignments_tb_users_tb_assigned_to_user_id",
                table: "complaint_assignments_tb",
                column: "assigned_to_user_id",
                principalTable: "users_tb",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_complaint_messages_tb_complaints_tb_complaint_id",
                table: "complaint_messages_tb",
                column: "complaint_id",
                principalTable: "complaints_tb",
                principalColumn: "complaint_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_complaint_messages_tb_users_tb_sender_user_id",
                table: "complaint_messages_tb",
                column: "sender_user_id",
                principalTable: "users_tb",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_complaints_tb_departments_tb_department_id",
                table: "complaints_tb",
                column: "department_id",
                principalTable: "departments_tb",
                principalColumn: "department_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_complaints_tb_sla_configs_tb_sla_config_id",
                table: "complaints_tb",
                column: "sla_config_id",
                principalTable: "sla_configs_tb",
                principalColumn: "sla_config_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_complaints_tb_users_tb_closed_by_user_id",
                table: "complaints_tb",
                column: "closed_by_user_id",
                principalTable: "users_tb",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_complaints_tb_users_tb_created_by_user_id",
                table: "complaints_tb",
                column: "created_by_user_id",
                principalTable: "users_tb",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_complaints_tb_users_tb_current_escalated_to_user_id",
                table: "complaints_tb",
                column: "current_escalated_to_user_id",
                principalTable: "users_tb",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_tb_users_tb_escalation_manager_id",
                table: "departments_tb",
                column: "escalation_manager_id",
                principalTable: "users_tb",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_tb_users_tb_manager_id",
                table: "departments_tb",
                column: "manager_id",
                principalTable: "users_tb",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_tb_users_tb_escalation_manager_id",
                table: "departments_tb");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_tb_users_tb_manager_id",
                table: "departments_tb");

            migrationBuilder.DropTable(
                name: "complaint_assignments_tb");

            migrationBuilder.DropTable(
                name: "complaint_messages_tb");

            migrationBuilder.DropTable(
                name: "email_outbox_tb");

            migrationBuilder.DropTable(
                name: "escalation_logs_tb");

            migrationBuilder.DropTable(
                name: "notifications_tb");

            migrationBuilder.DropTable(
                name: "complaints_tb");

            migrationBuilder.DropTable(
                name: "customers_tb");

            migrationBuilder.DropTable(
                name: "sla_configs_tb");

            migrationBuilder.DropTable(
                name: "users_tb");

            migrationBuilder.DropTable(
                name: "departments_tb");
        }
    }
}
