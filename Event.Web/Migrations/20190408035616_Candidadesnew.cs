using Microsoft.EntityFrameworkCore.Migrations;

namespace Event.Web.Migrations
{
    public partial class Candidadesnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnableBlankVote",
                table: "Votings");

            migrationBuilder.DropColumn(
                name: "QuantityBlankVotes",
                table: "Votings");

            migrationBuilder.DropColumn(
                name: "QuantityVotes",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "VotingId",
                table: "Candidates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_VotingId",
                table: "Candidates",
                column: "VotingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Votings_VotingId",
                table: "Candidates",
                column: "VotingId",
                principalTable: "Votings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Votings_VotingId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_VotingId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "VotingId",
                table: "Candidates");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnableBlankVote",
                table: "Votings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QuantityBlankVotes",
                table: "Votings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityVotes",
                table: "Candidates",
                nullable: false,
                defaultValue: 0);
        }
    }
}
