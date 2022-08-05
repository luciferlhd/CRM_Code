using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_Code.Migrations
{
    public partial class Created_Book_Entity : Migration
    { 
        // liên kếu 1-n (1 tác giả có thể có sáng tác ra nhiều cuốn sách)
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppBooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PageCount = table.Column<int>(type: "int", maxLength: 99999, nullable: false),
                    Pirce = table.Column<float>(type: "real", nullable: false),
                    //khai báo ID tác giả ở trong  bảng Books.cs để gen ra sql có khoác  chính kháo phụ với bảng author
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_AppBooks", x => x.Id); 
                    // chỗ này là chỗ  viết đoạn khóa chính phụ bảng author
                    table.ForeignKey(
                        name: "FK_AppBooks_AppAuthors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AppAuthors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBooks_AuthorId",
                table: "AppBooks",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBooks");
        }
    }
}
