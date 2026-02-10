using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RoomBookingBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingAndFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    BookedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Purpose = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusUpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, 120, "Ruangan luas di lantai 1 gedung D4. Fasilitas: 6 Kipas Angin, layar proyektor.", false, "Hall D4" },
                    { 2, 40, "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.201" },
                    { 3, 60, "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.202" },
                    { 4, 40, "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.203" },
                    { 5, 120, "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.204" },
                    { 6, 40, "Ruangan kelas di lantai 2 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.205" },
                    { 7, 120, "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "A.301" },
                    { 8, 60, "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "A.302" },
                    { 9, 120, "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "A.303" },
                    { 10, 40, "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "A.304" },
                    { 11, 60, "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.301" },
                    { 12, 40, "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.302" },
                    { 13, 120, "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.303" },
                    { 14, 60, "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.304" },
                    { 15, 40, "Ruangan kelas di lantai 3 gedung D4. Fasilitas: 1 Kipas Angin, 1 AC, layar proyektor.", false, "B.305" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
