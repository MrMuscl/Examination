using Microsoft.EntityFrameworkCore.Migrations;

namespace Examination.Data.Migrations
{
    public partial class allowNullAttestation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Protocol_Attestation",
                table: "Protocols");

            migrationBuilder.AlterColumn<int>(
                name: "AttestationId",
                table: "Protocols",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Protocol_Attestation",
                table: "Protocols",
                column: "AttestationId",
                principalTable: "Attestations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Protocol_Attestation",
                table: "Protocols");

            migrationBuilder.AlterColumn<int>(
                name: "AttestationId",
                table: "Protocols",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Protocol_Attestation",
                table: "Protocols",
                column: "AttestationId",
                principalTable: "Attestations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
