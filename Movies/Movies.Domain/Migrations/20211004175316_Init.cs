using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Genre = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FavouriteGenre = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieRent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentId = table.Column<int>(nullable: false),
                    MovieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieRent_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieRent_Rents_RentId",
                        column: x => x.RentId,
                        principalTable: "Rents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Genre", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "This is a comedy movie.", 1, "The Hangover", 2009 },
                    { 2, "This is an action movie.", 2, "Mortal Kombat", 2021 },
                    { 3, "This is a Sci-fi movie.", 3, "Inception", 2010 },
                    { 4, "This is a comedy movie.", 1, "Spy", 2015 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FavouriteGenre", "FullName", "Password", "Username" },
                values: new object[,]
                {
                    { 1, 1, "Jon Jonsky", "jonjon", "jonS" },
                    { 2, 2, "Jill Jillsky", "jilljill", "jillJ" },
                    { 3, 1, "Greg Gregsky", "greggreg", "gregG" }
                });

            migrationBuilder.InsertData(
                table: "Rents",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Rents",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 3, 1 });

            migrationBuilder.InsertData(
                table: "Rents",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "MovieRent",
                columns: new[] { "Id", "MovieId", "RentId" },
                values: new object[,]
                {
                    { 1, 2, 1 },
                    { 2, 3, 1 },
                    { 5, 1, 3 },
                    { 3, 1, 2 },
                    { 4, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieRent_MovieId",
                table: "MovieRent",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRent_RentId",
                table: "MovieRent",
                column: "RentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_UserId",
                table: "Rents",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieRent");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
