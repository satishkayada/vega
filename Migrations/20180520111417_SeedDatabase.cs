using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace vega.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Makes(Name) Values('Make1')");
            migrationBuilder.Sql("Insert into Makes(Name) Values('Make2')");
            migrationBuilder.Sql("Insert into Makes(Name) Values('Make3')");

            migrationBuilder.Sql("Insert into Models(Name,MakeId) Values('ModelA1',(Select Id From Makes where Name='Make1') )");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) Values('ModelB2',(Select Id From Makes where Name='Make1'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) Values('ModelC3',(Select Id From Makes where Name='Make1'))");

            migrationBuilder.Sql("Insert into Models(Name,MakeId) Values('ModelA1',(Select Id From Makes where Name='Make2'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) Values('ModelB2',(Select Id From Makes where Name='Make2'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) Values('ModelC3',(Select Id From Makes where Name='Make2'))");

            migrationBuilder.Sql("Insert into Models(Name,MakeId) Values('ModelA1',(Select Id From Makes where Name='Make3'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) Values('ModelB2',(Select Id From Makes where Name='Make3'))");
            migrationBuilder.Sql("Insert into Models(Name,MakeId) Values('ModelC3',(Select Id From Makes where Name='Make3'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From Makes Where Name in ('Make1','Make2','Make3')");

        }
    }
}
