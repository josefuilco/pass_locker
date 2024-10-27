using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PassLocker.Infrastructure.Migrations
{
    public partial class DefaultInserts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "category_name", "category_state" },
                values: new object[] { "Correo Electronico", true }
            );

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "tag_name", "tag_description", "tag_state" },
                values: new object[,]
                {
                    { "Personal", "Es una cuenta personal.", true },
                    { "Empresarial", "Es una cuenta de la empresa.", true }
                }
            );

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "owner_password" },
                values: new object[]
                {
                    "neasyq3NiuemBwQJdaI1bg==1vRBC7g9arojNgLGJWjT+Q==pZuh/YqtpMHAaChx/E6klw=="
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
