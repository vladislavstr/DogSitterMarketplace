using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DogSitterMarketplaceDal.Migrations
{
    /// <inheritdoc />
    public partial class initialqqq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Parameters = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppealStatusEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppealStatusEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppealTypeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppealTypeEntity", x => x.Id);
                });

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
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPassportDataEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassportNumber = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPassportDataEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStatusEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatusEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypeEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserPassportDataId = table.Column<int>(type: "int", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    UserStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserPassportDataEntity_UserPassportDataId",
                        column: x => x.UserPassportDataId,
                        principalTable: "UserPassportDataEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_UserRoleEntity_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoleEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_UserStatusEntity_UserStatusId",
                        column: x => x.UserStatusId,
                        principalTable: "UserStatusEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Characteristics = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_AnimalsTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AnimalsTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SitterWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitterWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SitterWork_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SitterWork_WorkTypeEntity_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypeEntity",
                        principalColumn: "Id");
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    SitterWorkId = table.Column<int>(type: "int", nullable: false),
                    Summ = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_SitterWork_SitterWorkId",
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

            migrationBuilder.CreateTable(
                name: "Appeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    AppealFromUserId = table.Column<int>(type: "int", nullable: false),
                    AppealToUserId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appeals_AppealStatusEntity_StatusId",
                        column: x => x.StatusId,
                        principalTable: "AppealStatusEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appeals_AppealTypeEntity_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AppealTypeEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appeals_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appeals_Users_AppealFromUserId",
                        column: x => x.AppealFromUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appeals_Users_AppealToUserId",
                        column: x => x.AppealToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    CommentFromUserId = table.Column<int>(type: "int", nullable: false),
                    CommentToUserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommentFromUserId",
                        column: x => x.CommentFromUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_CommentToUserId",
                        column: x => x.CommentToUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderEntityPetEntity",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    PetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntityPetEntity", x => new { x.OrdersId, x.PetsId });
                    table.ForeignKey(
                        name: "FK_OrderEntityPetEntity_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderEntityPetEntity_Pets_PetsId",
                        column: x => x.PetsId,
                        principalTable: "Pets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalsTypes_Name",
                table: "AnimalsTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_AppealFromUserId",
                table: "Appeals",
                column: "AppealFromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_AppealToUserId",
                table: "Appeals",
                column: "AppealToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_OrderId",
                table: "Appeals",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_StatusId",
                table: "Appeals",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Appeals_TypeId",
                table: "Appeals",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentFromUserId",
                table: "Comments",
                column: "CommentFromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentToUserId",
                table: "Comments",
                column: "CommentToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OrderId",
                table: "Comments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeekEntity_Name",
                table: "DayOfWeekEntity",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Name",
                table: "Location",
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
                name: "IX_OrderEntityPetEntity_PetsId",
                table: "OrderEntityPetEntity",
                column: "PetsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LocationId",
                table: "Orders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SitterWorkId",
                table: "Orders",
                column: "SitterWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_TypeId",
                table: "Pets",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_UserId",
                table: "Pets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SitterWork_UserId",
                table: "SitterWork",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SitterWork_WorkTypeId",
                table: "SitterWork",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimingLocationWorkEntity_DayOfWeekId",
                table: "TimingLocationWorkEntity",
                column: "DayOfWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_TimingLocationWorkEntity_LocationWorkId",
                table: "TimingLocationWorkEntity",
                column: "LocationWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPassportDataId",
                table: "Users",
                column: "UserPassportDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserStatusId",
                table: "Users",
                column: "UserStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypeEntity_Name",
                table: "WorkTypeEntity",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appeals");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrderEntityPetEntity");

            migrationBuilder.DropTable(
                name: "TimingLocationWorkEntity");

            migrationBuilder.DropTable(
                name: "AppealStatusEntity");

            migrationBuilder.DropTable(
                name: "AppealTypeEntity");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "DayOfWeekEntity");

            migrationBuilder.DropTable(
                name: "LocationWorkEntity");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "AnimalsTypes");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "SitterWork");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkTypeEntity");

            migrationBuilder.DropTable(
                name: "UserPassportDataEntity");

            migrationBuilder.DropTable(
                name: "UserRoleEntity");

            migrationBuilder.DropTable(
                name: "UserStatusEntity");
        }
    }
}
