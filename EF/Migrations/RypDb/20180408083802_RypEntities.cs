using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Project.Migrations.RypDb
{
    public partial class RypEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Shifr = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ryps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<string>(nullable: true),
                    FullCheck = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OperatorCheck = table.Column<bool>(nullable: false),
                    SpecialtyId = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ryps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ryps_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElectiveGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Credits = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Pr = table.Column<int>(nullable: false),
                    Shifr = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectiveGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectiveGroups_SubjectTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SubjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Credits = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    Lab = table.Column<int>(nullable: false),
                    Lec = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Pr = table.Column<int>(nullable: false),
                    Shifr = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_SubjectTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "SubjectTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RypId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_Ryps_RypId",
                        column: x => x.RypId,
                        principalTable: "Ryps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectElectiveGroups",
                columns: table => new
                {
                    ElectiveGroupId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectElectiveGroups", x => new { x.ElectiveGroupId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_SubjectElectiveGroups_ElectiveGroups_ElectiveGroupId",
                        column: x => x.ElectiveGroupId,
                        principalTable: "ElectiveGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectElectiveGroups_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectPrerequisiteSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrimaryId = table.Column<int>(nullable: false),
                    RelatedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPrerequisiteSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjectPrerequisiteSubjects_Subjects_PrimaryId",
                        column: x => x.PrimaryId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectPrerequisiteSubjects_Subjects_RelatedId",
                        column: x => x.RelatedId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SemesterElectiveGroups",
                columns: table => new
                {
                    SemesterId = table.Column<int>(nullable: false),
                    ElectiveGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterElectiveGroups", x => new { x.SemesterId, x.ElectiveGroupId });
                    table.ForeignKey(
                        name: "FK_SemesterElectiveGroups_ElectiveGroups_ElectiveGroupId",
                        column: x => x.ElectiveGroupId,
                        principalTable: "ElectiveGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterElectiveGroups_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SemesterSubjects",
                columns: table => new
                {
                    SemesterId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterSubjects", x => new { x.SemesterId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_SemesterSubjects_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectiveGroups_TypeId",
                table: "ElectiveGroups",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ryps_SpecialtyId",
                table: "Ryps",
                column: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterElectiveGroups_ElectiveGroupId",
                table: "SemesterElectiveGroups",
                column: "ElectiveGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_RypId",
                table: "Semesters",
                column: "RypId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubjects_SubjectId",
                table: "SemesterSubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectElectiveGroups_SubjectId",
                table: "SubjectElectiveGroups",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPrerequisiteSubjects_PrimaryId",
                table: "SubjectPrerequisiteSubjects",
                column: "PrimaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPrerequisiteSubjects_RelatedId",
                table: "SubjectPrerequisiteSubjects",
                column: "RelatedId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TypeId",
                table: "Subjects",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterElectiveGroups");

            migrationBuilder.DropTable(
                name: "SemesterSubjects");

            migrationBuilder.DropTable(
                name: "SubjectElectiveGroups");

            migrationBuilder.DropTable(
                name: "SubjectPrerequisiteSubjects");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "ElectiveGroups");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Ryps");

            migrationBuilder.DropTable(
                name: "SubjectTypes");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
