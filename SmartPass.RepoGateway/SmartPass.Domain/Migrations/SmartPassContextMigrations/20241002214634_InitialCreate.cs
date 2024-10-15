using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPass.Repository.Migrations.SmartPassContextMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardReaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AccessLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    InOutFlag = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LastUsingUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardReaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardReaders_AccessLevels_AccessLevelId",
                        column: x => x.AccessLevelId,
                        principalTable: "AccessLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardType = table.Column<int>(type: "integer", nullable: false),
                    AccessLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessKey = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    LastUsingUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessCards_AccessLevels_AccessLevelId",
                        column: x => x.AccessLevelId,
                        principalTable: "AccessLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessCards_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserUserRole",
                columns: table => new
                {
                    UserRolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUserRole", x => new { x.UserRolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserUserRole_UserRoles_UserRolesId",
                        column: x => x.UserRolesId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUserRole_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessCardId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionStatus = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedUtcDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CardReaderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_AccessCards_AccessCardId",
                        column: x => x.AccessCardId,
                        principalTable: "AccessCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sessions_CardReaders_CardReaderId",
                        column: x => x.CardReaderId,
                        principalTable: "CardReaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessCards_AccessLevelId",
                table: "AccessCards",
                column: "AccessLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessCards_UserId",
                table: "AccessCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardReaders_AccessLevelId",
                table: "CardReaders",
                column: "AccessLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AccessCardId",
                table: "Sessions",
                column: "AccessCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CardReaderId",
                table: "Sessions",
                column: "CardReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUserRole_UsersId",
                table: "UserUserRole",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "UserUserRole");

            migrationBuilder.DropTable(
                name: "AccessCards");

            migrationBuilder.DropTable(
                name: "CardReaders");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "AccessLevels");
        }
    }
}
