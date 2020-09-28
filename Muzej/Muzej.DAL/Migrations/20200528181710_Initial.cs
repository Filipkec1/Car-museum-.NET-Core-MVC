using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Muzej.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Motors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Power = table.Column<int>(nullable: false),
                    Torque = table.Column<int>(nullable: false),
                    Configuration = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    OIB = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Makers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    CountryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Makers_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModelName = table.Column<string>(maxLength: 30, nullable: false),
                    Color = table.Column<string>(nullable: true),
                    ManufactureDate = table.Column<DateTime>(nullable: false),
                    MakerID = table.Column<int>(nullable: false),
                    OwnerID = table.Column<int>(nullable: true),
                    MotorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cars_Makers_MakerID",
                        column: x => x.MakerID,
                        principalTable: "Makers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cars_Motors_MotorID",
                        column: x => x.MotorID,
                        principalTable: "Motors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cars_Owners_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Owners",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Njemačka" },
                    { 2, "Francuska" },
                    { 3, "Sjedinjene Američke Države" },
                    { 4, "Italija" },
                    { 5, "Hrvatska" },
                    { 6, "Švedska" }
                });

            migrationBuilder.InsertData(
                table: "Motors",
                columns: new[] { "ID", "Configuration", "Name", "Power", "Torque", "Type" },
                values: new object[,]
                {
                    { 1, "Multipoint fuel injection straight-three engine", "1.2 L I3 MPI", 55, 112, 0 },
                    { 2, "V8 twin turbo", "4.6 L V8 918", 400, 800, 0 },
                    { 3, "Electric motor", "Electric 918", 126, 240, 2 }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "ID", "FirstName", "LastName", "OIB" },
                values: new object[,]
                {
                    { 1, "Maja", "Tkalčević", "1234567890" },
                    { 2, "Filip", "Lukinić", "0987654321" },
                    { 3, "Dora", "Batinjan", "1337733142" }
                });

            migrationBuilder.InsertData(
                table: "Makers",
                columns: new[] { "ID", "CountryID", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Porsche" },
                    { 2, 1, "Audi" },
                    { 3, 1, "Volkswagen" },
                    { 4, 2, "Peugeot" },
                    { 5, 2, "Renault" },
                    { 6, 3, "Ford" },
                    { 7, 3, "Tesla" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_MakerID",
                table: "Cars",
                column: "MakerID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_MotorID",
                table: "Cars",
                column: "MotorID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerID",
                table: "Cars",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Makers_CountryID",
                table: "Makers",
                column: "CountryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Makers");

            migrationBuilder.DropTable(
                name: "Motors");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
