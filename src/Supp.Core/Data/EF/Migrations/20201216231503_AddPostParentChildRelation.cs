using Microsoft.EntityFrameworkCore.Migrations;

namespace Supp.Core.Migrations
{
    public partial class AddPostParentChildRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PostRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostRelations_Posts_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostRelations_Posts_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostRelations_ChildId",
                table: "PostRelations",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_PostRelations_ParentId",
                table: "PostRelations",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostRelations");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Posts");
        }
    }
}
