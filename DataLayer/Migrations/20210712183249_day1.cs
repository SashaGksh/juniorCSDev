using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class day1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads_Tags");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Contents");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Contents",
                newName: "Link");

            migrationBuilder.CreateTable(
                name: "AdTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad_Id = table.Column<int>(type: "int", nullable: false),
                    Tag_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdTags_Ads_Ad_Id",
                        column: x => x.Ad_Id,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdTags_Tags_Ad_Id",
                        column: x => x.Ad_Id,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdTags_Ad_Id",
                table: "AdTags",
                column: "Ad_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdTags");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Contents",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ads_Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad_Id = table.Column<int>(type: "int", nullable: false),
                    Tag_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ads_Tags_Ads_Ad_Id",
                        column: x => x.Ad_Id,
                        principalTable: "Ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ads_Tags_Tags_Ad_Id",
                        column: x => x.Ad_Id,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ads_Tags_Ad_Id",
                table: "Ads_Tags",
                column: "Ad_Id");
        }
    }
}
