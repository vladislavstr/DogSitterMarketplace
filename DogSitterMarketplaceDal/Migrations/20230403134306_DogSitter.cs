using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogSitterMarketplaceDal.Migrations
{
    /// <inheritdoc />
    public partial class DogSitter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "DaysOfWeek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "WorkTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "time(0)", nullable: false),
                    Stop = table.Column<DateTime>(type: "time(0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);

                });

            migrationBuilder.CreateTable(
                name: "SitterWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitterWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SitterWorks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SitterWorks_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    SitterWorkId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    IsNotActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationWorks_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationWorks_SitterWorks_SitterWorkId",
                        column: x => x.SitterWorkId,
                        principalTable: "SitterWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimingLocationWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeekId = table.Column<int>(type: "int", nullable: false),
                    WorkTimeId = table.Column<int>(type: "int", nullable: false),
                    SitterWorkEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimingLocationWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimingLocationWorks_DaysOfWeek_DayOfWeekId",
                        column: x => x.DayOfWeekId,
                        principalTable: "DaysOfWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimingLocationWorks_SitterWorks_SitterWorkEntityId",
                        column: x => x.SitterWorkEntityId,
                        principalTable: "SitterWorks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TimingLocationWorks_WorkTimes_WorkTimeId",
                        column: x => x.WorkTimeId,
                        principalTable: "WorkTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DaysOfWeek_Name",
                table: "DaysOfWeek",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Name",
                table: "Locations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationWorks_LocationId",
                table: "LocationWorks",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationWorks_SitterWorkId",
                table: "LocationWorks",
                column: "SitterWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_SitterWorks_UserId",
                table: "SitterWorks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SitterWorks_WorkTypeId",
                table: "SitterWorks",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimingLocationWorks_DayOfWeekId",
                table: "TimingLocationWorks",
                column: "DayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_TimingLocationWorks_SitterWorkEntityId",
                table: "TimingLocationWorks",
                column: "SitterWorkEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TimingLocationWorks_WorkTimeId",
                table: "TimingLocationWorks",
                column: "WorkTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypes_Name",
                table: "WorkTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationWorks");

            //migrationBuilder.DropTable(
            //    name: "PetEntity");

            migrationBuilder.DropTable(
                name: "TimingLocationWorks");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "DaysOfWeek");

            migrationBuilder.DropTable(
                name: "SitterWorks");

            migrationBuilder.DropTable(
                name: "WorkTimes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkTypes");

        }
    }
}