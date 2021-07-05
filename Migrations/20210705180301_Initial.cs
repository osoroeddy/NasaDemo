using Microsoft.EntityFrameworkCore.Migrations;

namespace NasaImagesDemo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApodImages",
                columns: table => new
                {
                    ApodImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    copyright = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    explanation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    hdurl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    media_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    service_version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApodImages", x => x.ApodImageID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApodImages");
        }
    }
}
