using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infotrack.Sales.SearchEngine.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class InitSearchHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keyword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistories_Url_Keyword_SearchDate",
                table: "SearchHistories",
                columns: new[] { "Url", "Keyword", "SearchDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchHistories");
        }
    }
}
