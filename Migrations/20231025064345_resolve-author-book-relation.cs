using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenticationApp.Migrations
{
    /// <inheritdoc />
    public partial class resolveauthorbookrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Author_BooksId1",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Book_BooksId",
                table: "AuthorBook");

            migrationBuilder.RenameColumn(
                name: "BooksId1",
                table: "AuthorBook",
                newName: "Book_Id");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "AuthorBook",
                newName: "Author_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorBook_BooksId1",
                table: "AuthorBook",
                newName: "IX_AuthorBook_Book_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Author_Author_Id",
                table: "AuthorBook",
                column: "Author_Id",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Book_Book_Id",
                table: "AuthorBook",
                column: "Book_Id",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Author_Author_Id",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Book_Book_Id",
                table: "AuthorBook");

            migrationBuilder.RenameColumn(
                name: "Book_Id",
                table: "AuthorBook",
                newName: "BooksId1");

            migrationBuilder.RenameColumn(
                name: "Author_Id",
                table: "AuthorBook",
                newName: "BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorBook_Book_Id",
                table: "AuthorBook",
                newName: "IX_AuthorBook_BooksId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Author_BooksId1",
                table: "AuthorBook",
                column: "BooksId1",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Book_BooksId",
                table: "AuthorBook",
                column: "BooksId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
