using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerService.Infrastructure.Data.Migrations
{
    public partial class InitialCustomerDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "customer");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    country_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "credit_cards",
                schema: "customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name_on_card = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    card_number = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    expiry = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    customer_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_credit_cards", x => x.id);
                    table.ForeignKey(
                        name: "fk_credit_cards_customers_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "customer",
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_credit_cards_customer_id",
                schema: "customer",
                table: "credit_cards",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_credit_cards_id",
                schema: "customer",
                table: "credit_cards",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_customers_id",
                schema: "customer",
                table: "customers",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credit_cards",
                schema: "customer");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "customer");
        }
    }
}
