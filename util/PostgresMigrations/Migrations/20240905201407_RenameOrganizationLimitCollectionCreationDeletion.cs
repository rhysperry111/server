﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bit.PostgresMigrations.Migrations;

/// <inheritdoc />
public partial class RenameOrganizationLimitCollectionCreationDeletion : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<bool>(
            name: "LimitCollectionCreationDeletion",
            table: "Organization",
            type: "boolean",
            nullable: false,
            defaultValue: false,
            oldClrType: typeof(bool),
            oldType: "boolean",
            oldDefaultValue: true);

        migrationBuilder.AddColumn<bool>(
            name: "LimitCollectionCreation",
            table: "Organization",
            type: "boolean",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "LimitCollectionCreation",
            table: "Organization");

        migrationBuilder.AlterColumn<bool>(
            name: "LimitCollectionCreationDeletion",
            table: "Organization",
            type: "boolean",
            nullable: false,
            defaultValue: true,
            oldClrType: typeof(bool),
            oldType: "boolean",
            oldDefaultValue: false);
    }
}
