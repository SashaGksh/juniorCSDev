using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class day71 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdTags_Tags_Ad_Id",
                table: "AdTags");

            migrationBuilder.CreateIndex(
                name: "IX_AdTags_Tag_Id",
                table: "AdTags",
                column: "Tag_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdTags_Tags_Tag_Id",
                table: "AdTags",
                column: "Tag_Id",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdTags_Tags_Tag_Id",
                table: "AdTags");

            migrationBuilder.DropIndex(
                name: "IX_AdTags_Tag_Id",
                table: "AdTags");

            migrationBuilder.AddForeignKey(
                name: "FK_AdTags_Tags_Ad_Id",
                table: "AdTags",
                column: "Ad_Id",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
