using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundraising.App.Core.Migrations
{
    public partial class FundraisingAppmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Member_BackerId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Reward_RewardId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Member_CreatorId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Reward_Project_ProjectId",
                table: "Reward");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reward",
                table: "Reward");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "Reward",
                newName: "Rewards");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.RenameIndex(
                name: "IX_Reward_ProjectId",
                table: "Rewards",
                newName: "IX_Rewards_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_CreatorId",
                table: "Projects",
                newName: "IX_Projects_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_RewardId",
                table: "Payments",
                newName: "IX_Payments_RewardId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_BackerId",
                table: "Payments",
                newName: "IX_Payments_BackerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rewards",
                table: "Rewards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Members_BackerId",
                table: "Payments",
                column: "BackerId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Rewards_RewardId",
                table: "Payments",
                column: "RewardId",
                principalTable: "Rewards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Members_CreatorId",
                table: "Projects",
                column: "CreatorId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rewards_Projects_ProjectId",
                table: "Rewards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Members_BackerId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Rewards_RewardId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Members_CreatorId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Rewards_Projects_ProjectId",
                table: "Rewards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rewards",
                table: "Rewards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Rewards",
                newName: "Reward");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.RenameIndex(
                name: "IX_Rewards_ProjectId",
                table: "Reward",
                newName: "IX_Reward_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CreatorId",
                table: "Project",
                newName: "IX_Project_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_RewardId",
                table: "Payment",
                newName: "IX_Payment_RewardId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BackerId",
                table: "Payment",
                newName: "IX_Payment_BackerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reward",
                table: "Reward",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Member_BackerId",
                table: "Payment",
                column: "BackerId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Reward_RewardId",
                table: "Payment",
                column: "RewardId",
                principalTable: "Reward",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Member_CreatorId",
                table: "Project",
                column: "CreatorId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reward_Project_ProjectId",
                table: "Reward",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
