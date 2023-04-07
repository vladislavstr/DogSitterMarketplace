using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogSitterMarketplaceDal.Migrations
{
    /// <inheritdoc />
    public partial class addAppealstoDBSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppealEntity_AppealStatusEntity_StatusId",
                table: "AppealEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AppealEntity_AppealTypeEntity_TypeId",
                table: "AppealEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AppealEntity_Orders_OrderId",
                table: "AppealEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AppealEntity_Users_AppealFromUserId",
                table: "AppealEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_AppealEntity_Users_AppealToUserId",
                table: "AppealEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppealEntity",
                table: "AppealEntity");

            migrationBuilder.RenameTable(
                name: "AppealEntity",
                newName: "Appeals");

            migrationBuilder.RenameIndex(
                name: "IX_AppealEntity_TypeId",
                table: "Appeals",
                newName: "IX_Appeals_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_AppealEntity_StatusId",
                table: "Appeals",
                newName: "IX_Appeals_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_AppealEntity_OrderId",
                table: "Appeals",
                newName: "IX_Appeals_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_AppealEntity_AppealToUserId",
                table: "Appeals",
                newName: "IX_Appeals_AppealToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AppealEntity_AppealFromUserId",
                table: "Appeals",
                newName: "IX_Appeals_AppealFromUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appeals",
                table: "Appeals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appeals_AppealStatusEntity_StatusId",
                table: "Appeals",
                column: "StatusId",
                principalTable: "AppealStatusEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appeals_AppealTypeEntity_TypeId",
                table: "Appeals",
                column: "AnimalTypeId",
                principalTable: "AppealTypeEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appeals_Orders_OrderId",
                table: "Appeals",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appeals_Users_AppealFromUserId",
                table: "Appeals",
                column: "AppealFromUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appeals_Users_AppealToUserId",
                table: "Appeals",
                column: "AppealToUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appeals_AppealStatusEntity_StatusId",
                table: "Appeals");

            migrationBuilder.DropForeignKey(
                name: "FK_Appeals_AppealTypeEntity_TypeId",
                table: "Appeals");

            migrationBuilder.DropForeignKey(
                name: "FK_Appeals_Orders_OrderId",
                table: "Appeals");

            migrationBuilder.DropForeignKey(
                name: "FK_Appeals_Users_AppealFromUserId",
                table: "Appeals");

            migrationBuilder.DropForeignKey(
                name: "FK_Appeals_Users_AppealToUserId",
                table: "Appeals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appeals",
                table: "Appeals");

            migrationBuilder.RenameTable(
                name: "Appeals",
                newName: "AppealEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Appeals_TypeId",
                table: "AppealEntity",
                newName: "IX_AppealEntity_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Appeals_StatusId",
                table: "AppealEntity",
                newName: "IX_AppealEntity_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Appeals_OrderId",
                table: "AppealEntity",
                newName: "IX_AppealEntity_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Appeals_AppealToUserId",
                table: "AppealEntity",
                newName: "IX_AppealEntity_AppealToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appeals_AppealFromUserId",
                table: "AppealEntity",
                newName: "IX_AppealEntity_AppealFromUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppealEntity",
                table: "AppealEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppealEntity_AppealStatusEntity_StatusId",
                table: "AppealEntity",
                column: "StatusId",
                principalTable: "AppealStatusEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppealEntity_AppealTypeEntity_TypeId",
                table: "AppealEntity",
                column: "AnimalTypeId",
                principalTable: "AppealTypeEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppealEntity_Orders_OrderId",
                table: "AppealEntity",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppealEntity_Users_AppealFromUserId",
                table: "AppealEntity",
                column: "AppealFromUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppealEntity_Users_AppealToUserId",
                table: "AppealEntity",
                column: "AppealToUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
