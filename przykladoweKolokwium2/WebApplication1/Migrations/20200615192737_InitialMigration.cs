using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klient",
                columns: table => new
                {
                    IdKlient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisk = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klient", x => x.IdKlient);
                });

            migrationBuilder.CreateTable(
                name: "Pracownik",
                columns: table => new
                {
                    IdPracown = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownik", x => x.IdPracown);
                });

            migrationBuilder.CreateTable(
                name: "WyrobCukierniczy",
                columns: table => new
                {
                    IdWyrobuCukierniczego = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CenaZaSzt = table.Column<float>(type: "real", nullable: false),
                    Typ = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WyrobCukierniczy", x => x.IdWyrobuCukierniczego);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienie",
                columns: table => new
                {
                    IdZamowienia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataPrzyjecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRealizacji = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Uwagi = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IdKlient = table.Column<int>(type: "int", nullable: false),
                    KlientIdKlient = table.Column<int>(type: "int", nullable: true),
                    IdPracownik = table.Column<int>(type: "int", nullable: false),
                    PracownikIdPracown = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienie", x => x.IdZamowienia);
                    table.ForeignKey(
                        name: "FK_Zamowienie_Klient_KlientIdKlient",
                        column: x => x.KlientIdKlient,
                        principalTable: "Klient",
                        principalColumn: "IdKlient",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zamowienie_Pracownik_PracownikIdPracown",
                        column: x => x.PracownikIdPracown,
                        principalTable: "Pracownik",
                        principalColumn: "IdPracown",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienie_WyrobCukierniczy",
                columns: table => new
                {
                    IdZamowienia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZamowienieIdZamowienia = table.Column<int>(type: "int", nullable: true),
                    IdWyrobuCukierniczego = table.Column<int>(type: "int", nullable: false),
                    WyrobCukierniczyIdWyrobuCukierniczego = table.Column<int>(type: "int", nullable: true),
                    Ilosc = table.Column<int>(type: "int", nullable: false),
                    Uwagi = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienie_WyrobCukierniczy", x => x.IdZamowienia);
                    table.ForeignKey(
                        name: "FK_Zamowienie_WyrobCukierniczy_WyrobCukierniczy_WyrobCukierniczyIdWyrobuCukierniczego",
                        column: x => x.WyrobCukierniczyIdWyrobuCukierniczego,
                        principalTable: "WyrobCukierniczy",
                        principalColumn: "IdWyrobuCukierniczego",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zamowienie_WyrobCukierniczy_Zamowienie_ZamowienieIdZamowienia",
                        column: x => x.ZamowienieIdZamowienia,
                        principalTable: "Zamowienie",
                        principalColumn: "IdZamowienia",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Klient",
                columns: new[] { "IdKlient", "Imie", "Nazwisk" },
                values: new object[] { 1, "Ala", "Kota" });

            migrationBuilder.InsertData(
                table: "Pracownik",
                columns: new[] { "IdPracown", "Imie", "Nazwisko" },
                values: new object[] { 1, "Ola", "Koti" });

            migrationBuilder.InsertData(
                table: "WyrobCukierniczy",
                columns: new[] { "IdWyrobuCukierniczego", "CenaZaSzt", "Nazwa", "Typ" },
                values: new object[] { 1, 6f, "ciastko", "kot" });

            migrationBuilder.InsertData(
                table: "Zamowienie",
                columns: new[] { "IdZamowienia", "DataPrzyjecia", "DataRealizacji", "IdKlient", "IdPracownik", "KlientIdKlient", "PracownikIdPracown", "Uwagi" },
                values: new object[] { 1, new DateTime(2020, 6, 15, 21, 27, 37, 625, DateTimeKind.Local).AddTicks(1885), new DateTime(2020, 6, 15, 21, 27, 37, 628, DateTimeKind.Local).AddTicks(8962), 1, 1, "aalaaaa" });

            migrationBuilder.InsertData(
                table: "Zamowienie_WyrobCukierniczy",
                columns: new[] { "IdZamowienia", "IdWyrobuCukierniczego", "Ilosc", "Uwagi", "WyrobCukierniczyIdWyrobuCukierniczego", "ZamowienieIdZamowienia" },
                values: new object[] { 1, 1, 2, "aaaaaa" });

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienie_KlientIdKlient",
                table: "Zamowienie",
                column: "KlientIdKlient");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienie_PracownikIdPracown",
                table: "Zamowienie",
                column: "PracownikIdPracown");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienie_WyrobCukierniczy_WyrobCukierniczyIdWyrobuCukierniczego",
                table: "Zamowienie_WyrobCukierniczy",
                column: "WyrobCukierniczyIdWyrobuCukierniczego");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienie_WyrobCukierniczy_ZamowienieIdZamowienia",
                table: "Zamowienie_WyrobCukierniczy",
                column: "ZamowienieIdZamowienia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zamowienie_WyrobCukierniczy");

            migrationBuilder.DropTable(
                name: "WyrobCukierniczy");

            migrationBuilder.DropTable(
                name: "Zamowienie");

            migrationBuilder.DropTable(
                name: "Klient");

            migrationBuilder.DropTable(
                name: "Pracownik");
        }
    }
}
