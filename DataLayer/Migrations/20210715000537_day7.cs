using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class day7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdTags_Ads_Ad_Id",
                table: "AdTags");

            migrationBuilder.DropForeignKey(
                name: "FK_AdTags_Tags_Ad_Id",
                table: "AdTags");

            migrationBuilder.AlterColumn<int>(
                name: "Ad_Id",
                table: "AdTags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "CurrentAd",
                table: "Ads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_AdTags_Ads_Ad_Id",
                table: "AdTags",
                column: "Ad_Id",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AdTags_Tags_Ad_Id",
                table: "AdTags",
                column: "Ad_Id",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdTags_Ads_Ad_Id",
                table: "AdTags");

            migrationBuilder.DropForeignKey(
                name: "FK_AdTags_Tags_Ad_Id",
                table: "AdTags");

            migrationBuilder.DropColumn(
                name: "CurrentAd",
                table: "Ads");

            migrationBuilder.AlterColumn<int>(
                name: "Ad_Id",
                table: "AdTags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdTags_Ads_Ad_Id",
                table: "AdTags",
                column: "Ad_Id",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdTags_Tags_Ad_Id",
                table: "AdTags",
                column: "Ad_Id",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
