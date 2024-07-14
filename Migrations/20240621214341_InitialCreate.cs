using Microsoft.EntityFrameworkCore.Migrations;

namespace socialMedia.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Users (
                    Id int NOT NULL AUTO_INCREMENT,
                    Username varchar(45) NOT NULL,
                    Email varchar(45) NOT NULL,
                    Password varchar(200) NOT NULL,
                    Name varchar(45) NOT NULL,
                    CoverPic varchar(500) NULL,
                    ProfilePic varchar(500) NULL,
                    City varchar(45) NULL,
                    Website varchar(255) NULL,
                    PRIMARY KEY (Id)
                )
                CHARSET=utf8mb4;
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Posts (
                    Id int NOT NULL AUTO_INCREMENT,
                    `Desc` varchar(200) NOT NULL,
                    Img varchar(200) NULL,
                    UserId int NOT NULL,
                    CreatedAt datetime(6) NULL,
                    Content longtext NULL,
                    PRIMARY KEY (Id),
                    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
                )
                CHARSET=utf8mb4;
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Relationships (
                    Id int NOT NULL AUTO_INCREMENT,
                    FollowerUserId int NOT NULL,
                    FollowedUserId int NOT NULL,
                    PRIMARY KEY (Id),
                    FOREIGN KEY (FollowerUserId) REFERENCES Users(Id) ON DELETE RESTRICT,
                    FOREIGN KEY (FollowedUserId) REFERENCES Users(Id) ON DELETE RESTRICT
                )
                CHARSET=utf8mb4;
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Stories (
                    Id int NOT NULL AUTO_INCREMENT,
                    Img varchar(300) NULL,
                    UserId int NOT NULL,
                    CreatedAt datetime(6) NULL,
                    PRIMARY KEY (Id),
                    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
                )
                CHARSET=utf8mb4;
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Comments (
                    Id int NOT NULL AUTO_INCREMENT,
                    `Desc` varchar(200) NOT NULL,
                    CreatedAt datetime(6) NULL,
                    UserId int NOT NULL,
                    PostId int NOT NULL,
                    PRIMARY KEY (Id),
                    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
                    FOREIGN KEY (PostId) REFERENCES Posts(Id) ON DELETE CASCADE
                )
                CHARSET=utf8mb4;
            ");

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS Likes (
                    UserId int NOT NULL,
                    PostId int NOT NULL,
                    Id int NOT NULL AUTO_INCREMENT,
                    PRIMARY KEY (UserId, PostId),
                    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
                    FOREIGN KEY (PostId) REFERENCES Posts(Id) ON DELETE CASCADE
                )
                CHARSET=utf8mb4;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Comments");
            migrationBuilder.DropTable(name: "Likes");
            migrationBuilder.DropTable(name: "Relationships");
            migrationBuilder.DropTable(name: "Stories");
            migrationBuilder.DropTable(name: "Posts");
            migrationBuilder.DropTable(name: "Users");
        }
    }
}
