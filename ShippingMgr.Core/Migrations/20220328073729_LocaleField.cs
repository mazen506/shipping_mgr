using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingMgr.Core.Migrations
{
    public partial class LocaleField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Phone",
            //    table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated_At",
                table: "Users",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "Title_En",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Title_Ar",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Remember_Token",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Description_En",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Description_Ar",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_At",
                table: "Users",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "Address_En",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Ar",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "Locale",
                table: "Users",
                type: "longtext",
                nullable: true
                );

            migrationBuilder.AlterColumn<string>(
                name: "Name_En",
                table: "Currencies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Name_Ar",
                table: "Currencies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Code_En",
                table: "Currencies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Code_Ar",
                table: "Currencies",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Locale",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated_At",
                table: "Users",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Title_En",
                keyValue: null,
                column: "Title_En",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title_En",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Title_Ar",
                keyValue: null,
                column: "Title_Ar",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title_Ar",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Remember_Token",
                keyValue: null,
                column: "Remember_Token",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Remember_Token",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Logo",
                keyValue: null,
                column: "Logo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Description_En",
                keyValue: null,
                column: "Description_En",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description_En",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Description_Ar",
                keyValue: null,
                column: "Description_Ar",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description_Ar",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_At",
                table: "Users",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Address_En",
                keyValue: null,
                column: "Address_En",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Address_En",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Address_Ar",
                keyValue: null,
                column: "Address_Ar",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Ar",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "longtext",
                nullable: false);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Name_En",
                keyValue: null,
                column: "Name_En",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name_En",
                table: "Currencies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Name_Ar",
                keyValue: null,
                column: "Name_Ar",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name_Ar",
                table: "Currencies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code_En",
                keyValue: null,
                column: "Code_En",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Code_En",
                table: "Currencies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Code_Ar",
                keyValue: null,
                column: "Code_Ar",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Code_Ar",
                table: "Currencies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
