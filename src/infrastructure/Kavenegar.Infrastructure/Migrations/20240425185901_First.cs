using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kavenegar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Entity");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Entity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BLog",
                schema: "Entity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity_BLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BLog_User_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "Entity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "Entity",
                table: "User",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_BLog_AuthorId",
                schema: "Entity",
                table: "BLog",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BLog",
                schema: "Entity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Entity");
        }
    }
}
