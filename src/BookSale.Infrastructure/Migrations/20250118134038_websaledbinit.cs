using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookSale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class websaledbinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    bio = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    stock = table.Column<int>(type: "integer", nullable: false),
                    authors_id = table.Column<int>(type: "integer", nullable: false),
                    categorys_id = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.id);
                    table.ForeignKey(
                        name: "FK_books_authors_authors_id",
                        column: x => x.authors_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_books_categories_categorys_id",
                        column: x => x.categorys_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    book_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.id);
                    table.ForeignKey(
                        name: "FK_carts_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_carts_users_user_user_id",
                        column: x => x.user_id,
                        principalTable: "users_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    book_id = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.id);
                    table.ForeignKey(
                        name: "FK_reviews_books_book_id",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reviews_users_user_user_id",
                        column: x => x.user_id,
                        principalTable: "users_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "payment_methods",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3843), new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3844) });

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 13, 40, 37, 268, DateTimeKind.Utc).AddTicks(3398), new DateTime(2025, 1, 18, 13, 40, 37, 268, DateTimeKind.Utc).AddTicks(3398) });

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 13, 40, 37, 268, DateTimeKind.Utc).AddTicks(3400), new DateTime(2025, 1, 18, 13, 40, 37, 268, DateTimeKind.Utc).AddTicks(3401) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3789), new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3790), new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3791), "$2a$11$Yv3vX6vtu5Rw0s.B0z.9Ae14Us5FKRCkrXi17IuDCOh4TP2c5Bn8.", new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3794) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3799), new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3800), new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3801), "$2a$11$Yv3vX6vtu5Rw0s.B0z.9Ae14Us5FKRCkrXi17IuDCOh4TP2c5Bn8.", new DateTime(2025, 1, 18, 13, 40, 37, 437, DateTimeKind.Utc).AddTicks(3802) });

            migrationBuilder.CreateIndex(
                name: "IX_books_authors_id",
                table: "books",
                column: "authors_id");

            migrationBuilder.CreateIndex(
                name: "IX_books_categorys_id",
                table: "books",
                column: "categorys_id");

            migrationBuilder.CreateIndex(
                name: "IX_carts_book_id",
                table: "carts",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_carts_user_id",
                table: "carts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_book_id",
                table: "reviews",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_user_id",
                table: "reviews",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.UpdateData(
                table: "payment_methods",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(151), new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(151) });

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 3, 12, 29, 596, DateTimeKind.Utc).AddTicks(7977), new DateTime(2025, 1, 18, 3, 12, 29, 596, DateTimeKind.Utc).AddTicks(7977) });

            migrationBuilder.UpdateData(
                table: "roles_role",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 3, 12, 29, 596, DateTimeKind.Utc).AddTicks(7980), new DateTime(2025, 1, 18, 3, 12, 29, 596, DateTimeKind.Utc).AddTicks(7980) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(130), new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(131), new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(132), "$2a$11$HU1fFqUwWJPpEojdbBQW7O/xCNIVMQlJWLM9Eb9RlzZOV9/7zfoRa", new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(140) });

            migrationBuilder.UpdateData(
                table: "users_user",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "date_joined", "last_login", "password", "updated_at" },
                values: new object[] { new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(145), new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(146), new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(146), "$2a$11$HU1fFqUwWJPpEojdbBQW7O/xCNIVMQlJWLM9Eb9RlzZOV9/7zfoRa", new DateTime(2025, 1, 18, 3, 12, 29, 784, DateTimeKind.Utc).AddTicks(147) });
        }
    }
}
