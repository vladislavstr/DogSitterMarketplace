using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogSitterMarketplaceDal.Migrations
{
    /// <inheritdoc />
    public partial class upd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AnimalsTypes_TypeId",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Pets",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_TypeId",
                table: "Pets",
                newName: "IX_Pets_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AnimalsTypes_TypeId",
                table: "Pets",
                column: "TypeId",
                principalTable: "AnimalsTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AnimalsTypes_TypeId",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Pets",
                newName: "AnimalTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_TypeId",
                table: "Pets",
                newName: "IX_Pets_AnimalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AnimalsTypes_AnimalTypeId",
                table: "Pets",
                column: "AnimalTypeId",
                principalTable: "AnimalsTypes",
                principalColumn: "Id");
        }
    }
}
