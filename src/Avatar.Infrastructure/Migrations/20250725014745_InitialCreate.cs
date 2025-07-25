using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Avatar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Avatars",
                columns: new[] { "Id", "Category", "CreatedAt", "CreatedBy", "Description", "ImageUrl", "IsActive", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "Human", new DateTime(2025, 7, 25, 1, 47, 45, 321, DateTimeKind.Utc).AddTicks(5467), "System", "A default avatar for new users", "/images/default-avatar.png", true, "Default Avatar", null, null },
                    { 2, "Robot", new DateTime(2025, 7, 25, 1, 47, 45, 321, DateTimeKind.Utc).AddTicks(5652), "System", "A futuristic robot avatar", "/images/robot-avatar.png", true, "Robot Avatar", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_Category",
                table: "Avatars",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_IsActive",
                table: "Avatars",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Avatars_Name",
                table: "Avatars",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avatars");
        }
    }
}
