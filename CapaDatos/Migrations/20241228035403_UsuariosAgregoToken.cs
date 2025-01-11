using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaEntidades.Migrations
{
    /// <inheritdoc />
    public partial class UsuariosAgregoToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "Token",
            table: "USUARIO",
            type: "nvarchar(max)", // Tipo en SQL Server
            nullable: true); // Permitir valores nulos
        }

           

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Token",
                table: "USUARIO");
        }
    }
}
