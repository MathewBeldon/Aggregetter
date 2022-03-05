using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aggregetter.Aggre.Persistance.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "tinytext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "tinytext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "tinytext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BaseAddress = table.Column<string>(type: "tinytext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TranslatedTitle = table.Column<string>(type: "tinytext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginalTitle = table.Column<string>(type: "tinytext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TranslatedBody = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OriginalBody = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TranslatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TranslatedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Endpoint = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArticleSlug = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDateUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleSlug",
                table: "Articles",
                column: "ArticleSlug");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Endpoint",
                table: "Articles",
                column: "Endpoint");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_Id",
                table: "Articles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ProviderId",
                table: "Articles",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ProviderId_CategoryId",
                table: "Articles",
                columns: new[] { "ProviderId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Id",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Id",
                table: "Languages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_Id",
                table: "Providers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_LanguageId",
                table: "Providers",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
