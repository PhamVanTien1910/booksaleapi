using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookSale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_methods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_name = table.Column<string>(type: "text", nullable: false),
                    customer_phone = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    payment_method_id = table.Column<int>(type: "integer", nullable: false),
                    payment_order_id = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_payment_methods_payment_method_id",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_superuser = table.Column<bool>(type: "boolean", nullable: false),
                    is_staff = table.Column<bool>(type: "boolean", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    date_joined = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_user_roles_role_role_id",
                        column: x => x.role_id,
                        principalTable: "roles_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "payment_methods",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[] { 1, new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8021), "VNPay", new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8023) });

            migrationBuilder.InsertData(
                table: "roles_role",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 16, 3, 34, 22, 537, DateTimeKind.Utc).AddTicks(7309), "admin", new DateTime(2025, 1, 16, 3, 34, 22, 537, DateTimeKind.Utc).AddTicks(7310) },
                    { 2, new DateTime(2025, 1, 16, 3, 34, 22, 537, DateTimeKind.Utc).AddTicks(7312), "member", new DateTime(2025, 1, 16, 3, 34, 22, 537, DateTimeKind.Utc).AddTicks(7312) }
                });

            migrationBuilder.InsertData(
                table: "users_user",
                columns: new[] { "id", "created_at", "date_joined", "email", "name", "is_active", "is_staff", "is_superuser", "last_login", "password", "role_id", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8009), new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8010), "admin@email.com", "Admin", true, false, true, new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8010), "$2a$11$RNr.qMwKfGGvdEbUd.PvSuut6YqMPvOk.Wkxd7dmByfUcuAc3zpCq", 1, new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8013) },
                    { 2, new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8016), new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8016), "tien@email.com", "Tien", true, false, false, new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8017), "$2a$11$RNr.qMwKfGGvdEbUd.PvSuut6YqMPvOk.Wkxd7dmByfUcuAc3zpCq", 2, new DateTime(2025, 1, 16, 3, 34, 22, 690, DateTimeKind.Utc).AddTicks(8018) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_payment_method_id",
                table: "orders",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_user_email",
                table: "users_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_user_role_id",
                table: "users_user",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "users_user");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "roles_role");
        }
    }
}
