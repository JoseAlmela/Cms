using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CmsEuroval.Migrations
{
    public partial class DemoData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pistas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Padding" },
                    { 2, "Football" },
                    { 3, "Soccer" }
                });

            migrationBuilder.InsertData(
                table: "Socios",
                columns: new[] { "Id", "Email", "Nombre" },
                values: new object[,]
                {
                    { 1, "micorreo@euroval.com", "Jose" },
                    { 2, "micorre2o@euroval.com", "Juan" },
                    { 3, "micorre3o@euroval.com", "Miguel" }
                });

            migrationBuilder.InsertData(
                table: "Reservas",
                columns: new[] { "Id", "Duracion", "FechaReserva", "PistaId", "SocioId" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 2, 54, 0, 0), new DateTime(2019, 1, 30, 21, 31, 13, 951, DateTimeKind.Local), 1, 1 },
                    { 4, new TimeSpan(0, 2, 1, 0, 0), new DateTime(2019, 2, 6, 21, 31, 13, 954, DateTimeKind.Local), 2, 1 },
                    { 7, new TimeSpan(0, 0, 10, 0, 0), new DateTime(2019, 6, 26, 21, 31, 13, 954, DateTimeKind.Local), 3, 1 },
                    { 2, new TimeSpan(0, 3, 4, 0, 0), new DateTime(2019, 7, 4, 21, 31, 13, 954, DateTimeKind.Local), 1, 2 },
                    { 5, new TimeSpan(0, 1, 6, 0, 0), new DateTime(2019, 8, 27, 21, 31, 13, 954, DateTimeKind.Local), 2, 2 },
                    { 8, new TimeSpan(0, 3, 6, 0, 0), new DateTime(2019, 3, 11, 21, 31, 13, 954, DateTimeKind.Local), 3, 2 },
                    { 3, new TimeSpan(0, 2, 19, 0, 0), new DateTime(2019, 4, 24, 21, 31, 13, 954, DateTimeKind.Local), 1, 3 },
                    { 6, new TimeSpan(0, 2, 8, 0, 0), new DateTime(2019, 2, 8, 21, 31, 13, 954, DateTimeKind.Local), 2, 3 },
                    { 9, new TimeSpan(0, 1, 40, 0, 0), new DateTime(2019, 2, 10, 21, 31, 13, 954, DateTimeKind.Local), 3, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reservas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Pistas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pistas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pistas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
