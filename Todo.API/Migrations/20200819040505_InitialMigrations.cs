using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.API.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(nullable: false),
                    MainCategory = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1500, nullable: true),
                    AuthorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "DateOfBirth", "FirstName", "LastName", "MainCategory", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(603), new DateTimeOffset(new DateTime(1950, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Berry", "Griffin Beak Eldritch", "Ships", new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(1754) },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2138), new DateTimeOffset(new DateTime(1968, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Nancy", "Swashbuckler Rye", "Rum", new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2156) },
                    { new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2169), new DateTimeOffset(new DateTime(1901, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Eli", "Ivory Bones Sweet", "Singing", new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2171) },
                    { new Guid("102b566b-ba1f-404c-b2df-e2cde39ade09"), new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2174), new DateTimeOffset(new DateTime(1902, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Arnold", "The Unseen Stafford", "Singing", new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2175) },
                    { new Guid("5b3621c0-7b12-4e80-9c8b-3398cba7ee05"), new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2179), new DateTimeOffset(new DateTime(1990, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Seabury", "Toxic Reyson", "Maps", new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2180) },
                    { new Guid("2aadd2df-7caf-45ab-9355-7f6332985a87"), new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2184), new DateTimeOffset(new DateTime(1923, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Rutherford", "Fearless Cloven", "General debauchery", new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2185) },
                    { new Guid("2ee49fe3-edf2-4f91-8409-3eb25ce6ca51"), new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2188), new DateTimeOffset(new DateTime(1921, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, -6, 0, 0, 0)), "Atherton", "Crow Ridley", "Rum", new DateTime(2020, 8, 18, 22, 5, 4, 941, DateTimeKind.Local).AddTicks(2189) }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Description", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTime(2020, 8, 18, 22, 5, 4, 942, DateTimeKind.Local).AddTicks(2676), "Commandeering a ship in rough waters isn't easy.  Commandeering it without getting caught is even harder.  In this course you'll learn how to sail away and avoid those pesky musketeers.", "Commandeering a Ship Without Getting Caught", new DateTime(2020, 8, 18, 22, 5, 4, 942, DateTimeKind.Local).AddTicks(2685) },
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new DateTime(2020, 8, 18, 22, 5, 4, 942, DateTimeKind.Local).AddTicks(2738), "In this course, the author provides tips to avoid, or, if needed, overthrow pirate mutiny.", "Overthrowing Mutiny", new DateTime(2020, 8, 18, 22, 5, 4, 942, DateTimeKind.Local).AddTicks(2739) },
                    { new Guid("d173e20d-159e-4127-9ce9-b0ac2564ad97"), new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), new DateTime(2020, 8, 18, 22, 5, 4, 942, DateTimeKind.Local).AddTicks(2743), "Every good pirate loves rum, but it also has a tendency to get you into trouble.  In this course you'll learn how to avoid that.  This new exclusive edition includes an additional chapter on how to run fast without falling while drunk.", "Avoiding Brawls While Drinking as Much Rum as You Desire", new DateTime(2020, 8, 18, 22, 5, 4, 942, DateTimeKind.Local).AddTicks(2744) },
                    { new Guid("40ff5488-fdab-45b5-bc3a-14302d59869a"), new Guid("2902b665-1190-4c70-9915-b9c2d7680450"), new DateTime(2020, 8, 18, 22, 5, 4, 942, DateTimeKind.Local).AddTicks(2749), "In this course you'll learn how to sing all-time favourite pirate songs without sounding like you actually know the words or how to hold a note.", "Singalong Pirate Hits", new DateTime(2020, 8, 18, 22, 5, 4, 942, DateTimeKind.Local).AddTicks(2750) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
