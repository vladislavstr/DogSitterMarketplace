using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogSitterMarketplaceDal.Migrations
{
    /// <inheritdoc />
    public partial class finishUpdateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationWorks_LocationEntity_LocationId",
                table: "LocationWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationWorks_SitterWorkEntity_SitterWorkId",
                table: "LocationWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_LocationEntity_LocationId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SitterWorkEntity_SitterWorkId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_SitterWorkEntity_Users_UserId",
                table: "SitterWorkEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_SitterWorkEntity_WorkTypes_WorkTypeId",
                table: "SitterWorkEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SitterWorkEntity",
                table: "SitterWorkEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationEntity",
                table: "LocationEntity");

            migrationBuilder.RenameTable(
                name: "SitterWorkEntity",
                newName: "SitterWorks");

            migrationBuilder.RenameTable(
                name: "LocationEntity",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_SitterWorkEntity_WorkTypeId",
                table: "SitterWorks",
                newName: "IX_SitterWorks_WorkTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SitterWorkEntity_UserId",
                table: "SitterWorks",
                newName: "IX_SitterWorks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LocationEntity_Name",
                table: "Locations",
                newName: "IX_Locations_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SitterWorks",
                table: "SitterWorks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationWorks_Locations_LocationId",
                table: "LocationWorks",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationWorks_SitterWorks_SitterWorkId",
                table: "LocationWorks",
                column: "SitterWorkId",
                principalTable: "SitterWorks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Locations_LocationId",
                table: "Orders",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SitterWorks_SitterWorkId",
                table: "Orders",
                column: "SitterWorkId",
                principalTable: "SitterWorks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SitterWorks_Users_UserId",
                table: "SitterWorks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SitterWorks_WorkTypes_WorkTypeId",
                table: "SitterWorks",
                column: "WorkTypeId",
                principalTable: "WorkTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationWorks_Locations_LocationId",
                table: "LocationWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_LocationWorks_SitterWorks_SitterWorkId",
                table: "LocationWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Locations_LocationId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SitterWorks_SitterWorkId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_SitterWorks_Users_UserId",
                table: "SitterWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_SitterWorks_WorkTypes_WorkTypeId",
                table: "SitterWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SitterWorks",
                table: "SitterWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "SitterWorks",
                newName: "SitterWorkEntity");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "LocationEntity");

            migrationBuilder.RenameIndex(
                name: "IX_SitterWorks_WorkTypeId",
                table: "SitterWorkEntity",
                newName: "IX_SitterWorkEntity_WorkTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SitterWorks_UserId",
                table: "SitterWorkEntity",
                newName: "IX_SitterWorkEntity_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_Name",
                table: "LocationEntity",
                newName: "IX_LocationEntity_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SitterWorkEntity",
                table: "SitterWorkEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationEntity",
                table: "LocationEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationWorks_LocationEntity_LocationId",
                table: "LocationWorks",
                column: "LocationId",
                principalTable: "LocationEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationWorks_SitterWorkEntity_SitterWorkId",
                table: "LocationWorks",
                column: "SitterWorkId",
                principalTable: "SitterWorkEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_LocationEntity_LocationId",
                table: "Orders",
                column: "LocationId",
                principalTable: "LocationEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SitterWorkEntity_SitterWorkId",
                table: "Orders",
                column: "SitterWorkId",
                principalTable: "SitterWorkEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SitterWorkEntity_Users_UserId",
                table: "SitterWorkEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SitterWorkEntity_WorkTypes_WorkTypeId",
                table: "SitterWorkEntity",
                column: "WorkTypeId",
                principalTable: "WorkTypes",
                principalColumn: "Id");
        }
    }
}
