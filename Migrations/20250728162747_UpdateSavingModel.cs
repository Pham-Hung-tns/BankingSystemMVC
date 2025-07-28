using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSavingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Savings_SavingPackages_savingPackagesPackageId",
            //    table: "Savings");

            //migrationBuilder.DropIndex(
            //    name: "IX_Savings_savingPackagesPackageId",
            //    table: "Savings");

            //migrationBuilder.DropColumn(
            //    name: "savingPackagesPackageId",
            //    table: "Savings");

            migrationBuilder.CreateIndex(
                name: "IX_Savings_PackageId",
                table: "Savings",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_SavingPackages_PackageId",
                table: "Savings",
                column: "PackageId",
                principalTable: "SavingPackages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Savings_SavingPackages_PackageId",
                table: "Savings");

            migrationBuilder.DropIndex(
                name: "IX_Savings_PackageId",
                table: "Savings");

            migrationBuilder.AddColumn<int>(
                name: "savingPackagesPackageId",
                table: "Savings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Savings_savingPackagesPackageId",
                table: "Savings",
                column: "savingPackagesPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Savings_SavingPackages_savingPackagesPackageId",
                table: "Savings",
                column: "savingPackagesPackageId",
                principalTable: "SavingPackages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
