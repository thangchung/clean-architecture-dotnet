using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductService.Infrastructure.Data.Migrations
{
    public partial class InitialProductionDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "prod");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "product_codes",
                schema: "prod",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_codes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                schema: "prod",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: false),
                    product_code_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_product_codes_product_code_id",
                        column: x => x.product_code_id,
                        principalSchema: "prod",
                        principalTable: "product_codes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "returns",
                schema: "prod",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reason = table.Column<int>(type: "integer", nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_returns", x => x.id);
                    table.ForeignKey(
                        name: "fk_returns_products_product_id",
                        column: x => x.product_id,
                        principalSchema: "prod",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_codes_id",
                schema: "prod",
                table: "product_codes",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_id",
                schema: "prod",
                table: "products",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_products_product_code_id",
                schema: "prod",
                table: "products",
                column: "product_code_id");

            migrationBuilder.CreateIndex(
                name: "ix_returns_id",
                schema: "prod",
                table: "returns",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_returns_product_id",
                schema: "prod",
                table: "returns",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "returns",
                schema: "prod");

            migrationBuilder.DropTable(
                name: "products",
                schema: "prod");

            migrationBuilder.DropTable(
                name: "product_codes",
                schema: "prod");
        }
    }
}
