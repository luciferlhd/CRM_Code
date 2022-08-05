using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_Code.Migrations
{
    public partial class Created_Reader_Entity : Migration
    {
        // chỗ này là hướng dẫn liên kết nhiều nhiều  và có cả  trường enum trong table
        // 1 sách có thể cho nhiều người thuê và 1 người thuê có thể thuê nhiều sách để đọc (n-n)
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppReaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", maxLength: 9999, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    // đây là trường dung enum
                    Gender = table.Column<int>(type: "int", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppReaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppReaderBook",
                columns: table => new
                {
                    // khởi tạo Id giữa 2 bảng để lk n-n
                    ReaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {// cái này tạo ra 1 table thứ 3  mới chỗ này chỉ để liên kết 2 table book và reader 
                    table.PrimaryKey("PK_AppReaderBook", x => new { x.ReaderId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AppReaderBook_AppBooks_BookId",
                        column: x => x.BookId,
                        principalTable: "AppBooks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppReaderBook_AppReaders_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "AppReaders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppReaderBook_BookId",
                table: "AppReaderBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_AppReaderBook_ReaderId_BookId",
                table: "AppReaderBook",
                columns: new[] { "ReaderId", "BookId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppReaderBook");

            migrationBuilder.DropTable(
                name: "AppReaders");
        }
    }
}
