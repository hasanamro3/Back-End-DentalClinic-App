using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DentalClinic.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblPatients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PatientAge = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPatients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "tblTreatments",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    TretmentType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTreatments", x => x.TreatmentId);
                });

            migrationBuilder.CreateTable(
                name: "tblAdmins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TreatmentStatus = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAdmins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_tblAdmins_tblPatients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "tblPatients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAdmins_tblTreatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "tblTreatments",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblPatients",
                columns: new[] { "PatientId", "Gender", "PatientAge", "PatientName", "Phone" },
                values: new object[,]
                {
                    { 1, 0, 30, "John Doe", null },
                    { 2, 1, 25, "Jane Smith", null }
                });

            migrationBuilder.InsertData(
                table: "tblTreatments",
                columns: new[] { "TreatmentId", "Cost", "Description", "TretmentType" },
                values: new object[,]
                {
                    { 1, 30.0, "Teeth Cleaning", 0 },
                    { 2, 30.0, "Cavity Filling", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAdmins_PatientId",
                table: "tblAdmins",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_tblAdmins_TreatmentId",
                table: "tblAdmins",
                column: "TreatmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAdmins");

            migrationBuilder.DropTable(
                name: "tblPatients");

            migrationBuilder.DropTable(
                name: "tblTreatments");
        }
    }
}
