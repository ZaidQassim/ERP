using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Appapi.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccordAttachments_CustomerAccords_CustomerAccordId",
                table: "CustomerAccordAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccordAttachments_Customers_CustomerId",
                table: "CustomerAccordAttachments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAccordAttachments_CustomerId",
                table: "CustomerAccordAttachments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "CustomerAccordAttachments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Employes",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 14, 1, 25, 57, 568, DateTimeKind.Local).AddTicks(9048),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2021, 2, 14, 0, 53, 17, 341, DateTimeKind.Local).AddTicks(5739));

            migrationBuilder.AlterColumn<int>(
                name: "CustomerAccordId",
                table: "CustomerAccordAttachments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccordAttachments_CustomerAccords_CustomerAccordId",
                table: "CustomerAccordAttachments",
                column: "CustomerAccordId",
                principalTable: "CustomerAccords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerAccordAttachments_CustomerAccords_CustomerAccordId",
                table: "CustomerAccordAttachments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Employes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2021, 2, 14, 0, 53, 17, 341, DateTimeKind.Local).AddTicks(5739),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2021, 2, 14, 1, 25, 57, 568, DateTimeKind.Local).AddTicks(9048));

            migrationBuilder.AlterColumn<int>(
                name: "CustomerAccordId",
                table: "CustomerAccordAttachments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "CustomerAccordAttachments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccordAttachments_CustomerId",
                table: "CustomerAccordAttachments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccordAttachments_CustomerAccords_CustomerAccordId",
                table: "CustomerAccordAttachments",
                column: "CustomerAccordId",
                principalTable: "CustomerAccords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerAccordAttachments_Customers_CustomerId",
                table: "CustomerAccordAttachments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
