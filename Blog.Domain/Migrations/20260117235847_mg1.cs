using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobHistorys",
                table: "JobHistorys");

            migrationBuilder.RenameTable(
                name: "JobHistorys",
                newName: "JobHistories");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobHistories",
                table: "JobHistories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "JobHistories",
                newName: "JobHistorys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobHistorys",
                table: "JobHistorys",
                column: "Id");
        }
    }
}
