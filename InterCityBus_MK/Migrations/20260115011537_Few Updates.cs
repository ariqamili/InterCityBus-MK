using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterCityBus_MK.Migrations
{
    /// <inheritdoc />
    public partial class FewUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Companies_CompanyId",
                table: "Trips");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "DepartureTime",
                table: "Trips",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ArrivalTime",
                table: "Trips",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "FromStationId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Trips",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ToStationId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ArrivalTime",
                table: "Stops",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Stations",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "Companies",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_FromStationId",
                table: "Trips",
                column: "FromStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ToStationId",
                table: "Trips",
                column: "ToStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_StationId",
                table: "Stops",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_TripId",
                table: "Stops",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Stations_StationId",
                table: "Stops",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stops_Trips_TripId",
                table: "Stops",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Companies_CompanyId",
                table: "Trips",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stations_FromStationId",
                table: "Trips",
                column: "FromStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stations_ToStationId",
                table: "Trips",
                column: "ToStationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Stations_StationId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_Stops_Trips_TripId",
                table: "Stops");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Companies_CompanyId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stations_FromStationId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stations_ToStationId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_FromStationId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ToStationId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Stops_StationId",
                table: "Stops");

            migrationBuilder.DropIndex(
                name: "IX_Stops_TripId",
                table: "Stops");

            migrationBuilder.DropColumn(
                name: "FromStationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ToStationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Stops");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepartureTime",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArrivalTime",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Stations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Companies_CompanyId",
                table: "Trips",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
