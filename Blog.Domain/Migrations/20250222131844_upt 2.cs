using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Domain.Migrations
{
    /// <inheritdoc />
    public partial class upt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatId",
                table: "Blogs");
        }
    }
}
