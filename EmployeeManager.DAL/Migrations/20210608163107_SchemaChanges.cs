using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManager.DAL.Migrations
{
    public partial class SchemaChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameweekStudent");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Gameweeks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "Id", "Name", "NumberOfGameweeks" },
                values: new object[,]
                {
                    { 1, "Prva hrvatska nogometna liga", 36 },
                    { 2, "Premier liga", 38 },
                    { 3, "Serie A", 40 },
                    { 4, "French Ligue 1", 38 },
                    { 5, "Bundesliga", 38 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gameweeks_StudentId",
                table: "Gameweeks",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gameweeks_Students_StudentId",
                table: "Gameweeks",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gameweeks_Students_StudentId",
                table: "Gameweeks");

            migrationBuilder.DropIndex(
                name: "IX_Gameweeks_StudentId",
                table: "Gameweeks");

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Gameweeks");

            migrationBuilder.CreateTable(
                name: "GameweekStudent",
                columns: table => new
                {
                    GameweeksId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameweekStudent", x => new { x.GameweeksId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_GameweekStudent_Gameweeks_GameweeksId",
                        column: x => x.GameweeksId,
                        principalTable: "Gameweeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameweekStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameweekStudent_StudentsId",
                table: "GameweekStudent",
                column: "StudentsId");
        }
    }
}
