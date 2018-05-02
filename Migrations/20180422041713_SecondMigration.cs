using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace auction.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "new_bid",
                table: "auctions",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "new_bid",
                table: "auctions");
        }
    }
}
