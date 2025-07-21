using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace videoportalAPI.Migrations
{
    /// <inheritdoc />
    public partial class migv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlobUrl",
                table: "TrainingVideos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TrainingVideos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TrainingVideos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "TrainingVideos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobUrl",
                table: "TrainingVideos");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TrainingVideos");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TrainingVideos");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "TrainingVideos");
        }
    }
}
