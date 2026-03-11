using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDataColumnToCityTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CityTrips_FromCityId_ToCityId_TripId",
                table: "CityTrips");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDeparture",
                table: "CityTrips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_CityTrips_FromCityId_ToCityId_DateOfDeparture",
                table: "CityTrips",
                columns: new[] { "FromCityId", "ToCityId", "DateOfDeparture" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CityTrips_FromCityId_ToCityId_DateOfDeparture",
                table: "CityTrips");

            migrationBuilder.DropColumn(
                name: "DateOfDeparture",
                table: "CityTrips");

            migrationBuilder.CreateIndex(
                name: "IX_CityTrips_FromCityId_ToCityId_TripId",
                table: "CityTrips",
                columns: new[] { "FromCityId", "ToCityId", "TripId" },
                unique: true);
        }
    }
}
