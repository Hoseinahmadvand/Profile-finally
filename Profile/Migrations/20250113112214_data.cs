using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Profile.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "LogoPath", "ShowContactUsAr", "ShowContactUsDe", "ShowContactUsFa", "ShowGalleryAr", "ShowGalleryDe", "ShowGalleryFa", "ShowProjectsAr", "ShowProjectsDe", "ShowProjectsFa", "ShowSkillsAr", "ShowSkillsDe", "ShowSkillsFa", "TitleSiteAr", "TitleSiteDe", "TitleSiteFa" },
                values: new object[] { 1, "", true, true, true, true, true, true, true, true, true, true, true, true, "العربیه", "Germany", "فارسی" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
