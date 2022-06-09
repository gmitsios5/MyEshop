using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyEshop.Migrations
{
    public partial class SeedRoles : Migration


    {

        private string ManagerRoleId = Guid.NewGuid().ToString();
        private string UserRoleId = Guid.NewGuid().ToString();
        private string AdminRoleId = Guid.NewGuid().ToString();

        private string User3Id = Guid.NewGuid().ToString();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);

            SeedUser(migrationBuilder);

            SeedUserRoles(migrationBuilder);

        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{AdminRoleId}', 'Administrator', 'ADMINISTRATOR', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{ManagerRoleId}', 'Manager', 'MANAGER', null);");
            migrationBuilder.Sql(@$"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{UserRoleId}', 'User', 'USER', null);");
        }

        private void SeedUser(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               @$"INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], 
                [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], 
                [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
                VALUES 
                (N'{User3Id}', N'manos', N'manos', N'manos@manos.com', N'MANOS@MANOS.COM', 
                N'manos@manos.com', N'MANOS@MANOS.COM', 0, 
                N'AQAAAAEAACcQAAAAEFGxpIQqgb81OBnPLhIbSstfba6ooUzPW93Fq52uNeZjVGQGoXmsNTMQ5hGmTj7W7A==', 
                N'YR3H35UZOCJNF4XY4BMNNYTKYFPJFFML', N'85e25918-4310-49b8-b048-2abfa36ecdb1', NULL, 0, 0, NULL, 1, 0)");
        }

       
      


    private void SeedUserRoles(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"
            INSERT INTO [dbo].[AspNetUserRoles]
               ([UserId]
               ,[RoleId])
            VALUES
               ('{User3Id}', '{AdminRoleId}');

");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
