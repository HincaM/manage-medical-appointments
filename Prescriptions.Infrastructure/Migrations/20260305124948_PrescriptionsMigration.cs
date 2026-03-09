using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prescriptions.Infrastructure.Migrations
{
    public partial class PrescriptionsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prescription",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescription", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescription");
        }
    }
}
