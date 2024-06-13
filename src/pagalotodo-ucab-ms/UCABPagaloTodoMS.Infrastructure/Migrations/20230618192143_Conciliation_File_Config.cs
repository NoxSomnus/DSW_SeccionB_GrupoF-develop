using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    public partial class Conciliation_File_Config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConciliationFileConfigureEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IncludeDni = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeName = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeLastname = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeUsername = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeEmail = table.Column<bool>(type: "boolean", nullable: false),
                    IncludePhoneNumber = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeAmount = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeBillDate = table.Column<bool>(type: "boolean", nullable: false),
                    IncludeContractnumber = table.Column<bool>(type: "boolean", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConciliationFileConfigureEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConciliationFileConfigureEntities_ServiceEntities_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConciliationFileConfigureEntities_UserEntities_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConciliationFileConfigureEntities_ProviderId",
                table: "ConciliationFileConfigureEntities",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ConciliationFileConfigureEntities_ServiceId",
                table: "ConciliationFileConfigureEntities",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConciliationFileConfigureEntities");
        }
    }
}
