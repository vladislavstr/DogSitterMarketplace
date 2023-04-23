using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogSitterMarketplaceDal.Migrations
{
    /// <inheritdoc />
    public partial class changetableswithWorks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkTypeEntity",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "SitterWork",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Location",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "DayOfWeekEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfWeekEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationWorkEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    IsNotActive = table.Column<bool>(type: "bit", nullable: false),
                    SitterWorkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationWorkEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationWorkEntity_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocationWorkEntity_SitterWork_SitterWorkId",
                        column: x => x.SitterWorkId,
                        principalTable: "SitterWork",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TimingLocationWorkEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    Stop = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    DayOfWeekId = table.Column<int>(type: "int", nullable: false),
                    LocationWorkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimingLocationWorkEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimingLocationWorkEntity_DayOfWeekEntity_DayOfWeekId",
                        column: x => x.DayOfWeekId,
                        principalTable: "DayOfWeekEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimingLocationWorkEntity_LocationWorkEntity_LocationWorkId",
                        column: x => x.LocationWorkId,
                        principalTable: "LocationWorkEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypeEntity_Name",
                table: "WorkTypeEntity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Name",
                table: "Location",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeekEntity_Name",
                table: "DayOfWeekEntity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationWorkEntity_LocationId",
                table: "LocationWorkEntity",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationWorkEntity_SitterWorkId",
                table: "LocationWorkEntity",
                column: "SitterWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_TimingLocationWorkEntity_DayOfWeekId",
                table: "TimingLocationWorkEntity",
                column: "DayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_TimingLocationWorkEntity_LocationWorkId",
                table: "TimingLocationWorkEntity",
                column: "LocationWorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimingLocationWorkEntity");

            migrationBuilder.DropTable(
                name: "DayOfWeekEntity");

            migrationBuilder.DropTable(
                name: "LocationWorkEntity");

            migrationBuilder.DropIndex(
                name: "IX_WorkTypeEntity_Name",
                table: "WorkTypeEntity");

            migrationBuilder.DropIndex(
                name: "IX_Location_Name",
                table: "Location");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "WorkTypeEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "SitterWork",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Location",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");
        }
    }
}
