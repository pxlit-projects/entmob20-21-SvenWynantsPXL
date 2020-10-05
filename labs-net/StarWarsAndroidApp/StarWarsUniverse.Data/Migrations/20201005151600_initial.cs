using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWarsUniverse.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Uri = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    EpisodeId = table.Column<int>(nullable: false),
                    OpeningCrawl = table.Column<string>(nullable: true),
                    Director = table.Column<string>(nullable: true),
                    Producer = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Uri);
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Uri = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Edited = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RotationPeriod = table.Column<string>(nullable: true),
                    OrbitalPeriod = table.Column<string>(nullable: true),
                    Diameter = table.Column<string>(nullable: true),
                    Climate = table.Column<string>(nullable: true),
                    Gravity = table.Column<string>(nullable: true),
                    Terrain = table.Column<string>(nullable: true),
                    SurfaceWater = table.Column<string>(nullable: true),
                    Population = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Uri);
                });

            migrationBuilder.CreateTable(
                name: "MoviePlanet",
                columns: table => new
                {
                    MovieUri = table.Column<string>(nullable: false),
                    PlanetUri = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePlanet", x => new { x.MovieUri, x.PlanetUri });
                    table.ForeignKey(
                        name: "FK_MoviePlanet_Movies_MovieUri",
                        column: x => x.MovieUri,
                        principalTable: "Movies",
                        principalColumn: "Uri",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviePlanet_Planets_PlanetUri",
                        column: x => x.PlanetUri,
                        principalTable: "Planets",
                        principalColumn: "Uri",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Uri", "Created", "Director", "Edited", "EpisodeId", "OpeningCrawl", "Producer", "ReleaseDate", "Title" },
                values: new object[] { "http://swapi.dev/api/films/1/", new DateTime(2014, 12, 10, 14, 23, 31, 880, DateTimeKind.Utc), "George Lucas", new DateTime(2014, 12, 20, 19, 49, 45, 256, DateTimeKind.Utc), 4, @"It is a period of civil war.
Rebel spaceships, striking
from a hidden base, have won
their first victory against
the evil Galactic Empire.

During the battle, Rebel
spies managed to steal secret
plans to the Empire's
ultimate weapon, the DEATH
STAR, an armored space
station with enough power
to destroy an entire planet.

Pursued by the Empire's
sinister agents, Princess
Leia races home aboard her
starship, custodian of the
stolen plans that can save her
people and restore
freedom to the galaxy....", "Gary Kurtz, Rick McCallum", new DateTime(1977, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "A New Hope" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Uri", "Created", "Director", "Edited", "EpisodeId", "OpeningCrawl", "Producer", "ReleaseDate", "Title" },
                values: new object[] { "http://swapi.dev/api/films/2/", new DateTime(2014, 12, 12, 11, 26, 24, 656, DateTimeKind.Utc), "Irvin Kershner", new DateTime(2014, 12, 15, 13, 7, 53, 386, DateTimeKind.Utc), 5, @"It is a dark time for the
Rebellion. Although the Death
Star has been destroyed,
Imperial troops have driven the
Rebel forces from their hidden
base and pursued them across
the galaxy.

Evading the dreaded Imperial
Starfleet, a group of freedom
fighters led by Luke Skywalker
has established a new secret
base on the remote ice world
of Hoth.

The evil lord Darth Vader,
obsessed with finding young
Skywalker, has dispatched
thousands of remote probes into
the far reaches of space....", "Gary Kurtz, Rick McCallum", new DateTime(1980, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Empire Strikes Back" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Uri", "Created", "Director", "Edited", "EpisodeId", "OpeningCrawl", "Producer", "ReleaseDate", "Title" },
                values: new object[] { "http://swapi.dev/api/films/3/", new DateTime(2014, 12, 18, 10, 39, 33, 255, DateTimeKind.Utc), "Richard Marquand", new DateTime(2014, 12, 20, 9, 48, 37, 462, DateTimeKind.Utc), 6, @"Luke Skywalker has returned to
his home planet of Tatooine in
an attempt to rescue his
friend Han Solo from the
clutches of the vile gangster
Jabba the Hutt.

Little does Luke know that the
GALACTIC EMPIRE has secretly
begun construction on a new
armored space station even
more powerful than the first
dreaded Death Star.

When completed, this ultimate
weapon will spell certain doom
for the small band of rebels
struggling to restore freedom
to the galaxy...", "Howard G. Kazanjian, George Lucas, Rick McCallum", new DateTime(1983, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Return of the Jedi" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Uri", "Created", "Director", "Edited", "EpisodeId", "OpeningCrawl", "Producer", "ReleaseDate", "Title" },
                values: new object[] { "http://swapi.dev/api/films/4/", new DateTime(2014, 12, 19, 16, 52, 55, 740, DateTimeKind.Utc), "George Lucas", new DateTime(2014, 12, 20, 10, 54, 7, 216, DateTimeKind.Utc), 1, @"Turmoil has engulfed the
Galactic Republic. The taxation
of trade routes to outlying star
systems is in dispute.

Hoping to resolve the matter
with a blockade of deadly
battleships, the greedy Trade
Federation has stopped all
shipping to the small planet
of Naboo.

While the Congress of the
Republic endlessly debates
this alarming chain of events,
the Supreme Chancellor has
secretly dispatched two Jedi
Knights, the guardians of
peace and justice in the
galaxy, to settle the conflict....", "Rick McCallum", new DateTime(1999, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Phantom Menace" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Uri", "Created", "Director", "Edited", "EpisodeId", "OpeningCrawl", "Producer", "ReleaseDate", "Title" },
                values: new object[] { "http://swapi.dev/api/films/5/", new DateTime(2014, 12, 20, 10, 57, 57, 886, DateTimeKind.Utc), "George Lucas", new DateTime(2014, 12, 20, 20, 18, 48, 516, DateTimeKind.Utc), 2, @"There is unrest in the Galactic
Senate. Several thousand solar
systems have declared their
intentions to leave the Republic.

This separatist movement,
under the leadership of the
mysterious Count Dooku, has
made it difficult for the limited
number of Jedi Knights to maintain 
peace and order in the galaxy.

Senator Amidala, the former
Queen of Naboo, is returning
to the Galactic Senate to vote
on the critical issue of creating
an ARMY OF THE REPUBLIC
to assist the overwhelmed
Jedi....", "Rick McCallum", new DateTime(2002, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Attack of the Clones" });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Uri", "Created", "Director", "Edited", "EpisodeId", "OpeningCrawl", "Producer", "ReleaseDate", "Title" },
                values: new object[] { "http://swapi.dev/api/films/6/", new DateTime(2014, 12, 20, 18, 49, 38, 403, DateTimeKind.Utc), "George Lucas", new DateTime(2014, 12, 20, 20, 47, 52, 73, DateTimeKind.Utc), 3, @"War! The Republic is crumbling
under attacks by the ruthless
Sith Lord, Count Dooku.
There are heroes on both sides.
Evil is everywhere.

In a stunning move, the
fiendish droid leader, General
Grievous, has swept into the
Republic capital and kidnapped
Chancellor Palpatine, leader of
the Galactic Senate.

As the Separatist Droid Army
attempts to flee the besieged
capital with their valuable
hostage, two Jedi Knights lead a
desperate mission to rescue the
captive Chancellor....", "Rick McCallum", new DateTime(2005, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revenge of the Sith" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/42/", "temperate", new DateTime(2014, 12, 20, 10, 12, 28, 980, DateTimeKind.Utc), "10120", new DateTime(2014, 12, 20, 20, 58, 18, 491, DateTimeKind.Utc), "0.98", "Haruun Kal", "383", "705300", "25", "unknown", "toxic cloudsea, plateaus, volcanoes" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/41/", "unknown", new DateTime(2014, 12, 20, 10, 7, 29, 578, DateTimeKind.Utc), "12190", new DateTime(2014, 12, 20, 20, 58, 18, 489, DateTimeKind.Utc), "unknown", "Tund", "1770", "0", "48", "unknown", "barren, ash" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/40/", "unknown", new DateTime(2014, 12, 20, 10, 1, 37, 395, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 487, DateTimeKind.Utc), "unknown", "Troiken", "unknown", "unknown", "unknown", "unknown", "desert, tundra, rainforests, mountains" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/39/", "temperate, artic", new DateTime(2014, 12, 20, 9, 56, 58, 874, DateTimeKind.Utc), "14900", new DateTime(2014, 12, 20, 20, 58, 18, 485, DateTimeKind.Utc), "1", "Vulpter", "391", "421000000", "22", "unknown", "urban, barren" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/38/", "unknown", new DateTime(2014, 12, 20, 9, 52, 23, 452, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 483, DateTimeKind.Utc), "unknown", "Aleen Minor", "unknown", "unknown", "unknown", "unknown", "unknown" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/35/", "arid, temperate, tropical", new DateTime(2014, 12, 19, 17, 52, 13, 106, DateTimeKind.Utc), "18880", new DateTime(2014, 12, 20, 20, 58, 18, 478, DateTimeKind.Utc), "1.56", "Malastare", "201", "2000000000", "26", "unknown", "swamps, deserts, jungles, mountains" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/36/", "temperate", new DateTime(2014, 12, 19, 18, 0, 40, 142, DateTimeKind.Utc), "10480", new DateTime(2014, 12, 20, 20, 58, 18, 480, DateTimeKind.Utc), "0.9", "Dathomir", "491", "5200", "24", "unknown", "forests, deserts, savannas" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/43/", "temperate", new DateTime(2014, 12, 20, 10, 14, 48, 178, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 493, DateTimeKind.Utc), "1", "Cerea", "386", "450000000", "27", "20", "verdant" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/34/", "temperate", new DateTime(2014, 12, 19, 17, 47, 54, 403, DateTimeKind.Utc), "7900", new DateTime(2014, 12, 20, 20, 58, 18, 476, DateTimeKind.Utc), "1", "Toydaria", "184", "11000000", "21", "unknown", "swamps, lakes" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/33/", "superheated", new DateTime(2014, 12, 18, 11, 25, 40, 243, DateTimeKind.Utc), "12780", new DateTime(2014, 12, 20, 20, 58, 18, 474, DateTimeKind.Utc), "1", "Sullust", "263", "18500000000", "20", "5", "mountains, volcanoes, rocky deserts" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/32/", "temperate", new DateTime(2014, 12, 18, 11, 11, 51, 872, DateTimeKind.Utc), "13500", new DateTime(2014, 12, 20, 20, 58, 18, 472, DateTimeKind.Utc), "1", "Chandrila", "368", "1200000000", "20", "40", "plains, forests" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/37/", "temperate, arid, subartic", new DateTime(2014, 12, 20, 9, 46, 25, 740, DateTimeKind.Utc), "10600", new DateTime(2014, 12, 20, 20, 58, 18, 481, DateTimeKind.Utc), "1", "Ryloth", "305", "1500000000", "30", "5", "mountains, valleys, deserts, tundra" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/44/", "tropical, temperate", new DateTime(2014, 12, 20, 10, 18, 26, 110, DateTimeKind.Utc), "15600", new DateTime(2014, 12, 20, 20, 58, 18, 495, DateTimeKind.Utc), "1", "Glee Anselm", "206", "500000000", "33", "80", "lakes, islands, swamps, seas" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/47/", "arid, rocky, windy", new DateTime(2014, 12, 20, 10, 31, 32, 413, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 500, DateTimeKind.Utc), "1", "Iktotch", "481", "unknown", "22", "unknown", "rocky" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/46/", "unknown", new DateTime(2014, 12, 20, 10, 28, 31, 117, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 498, DateTimeKind.Utc), "unknown", "Tholoth", "unknown", "unknown", "unknown", "unknown", "unknown" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/31/", "temperate", new DateTime(2014, 12, 18, 11, 7, 1, 792, DateTimeKind.Utc), "11030", new DateTime(2014, 12, 20, 20, 58, 18, 471, DateTimeKind.Utc), "1", "Mon Cala", "398", "27000000000", "21", "100", "oceans, reefs, islands" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/48/", "unknown", new DateTime(2014, 12, 20, 10, 34, 8, 249, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 502, DateTimeKind.Utc), "unknown", "Quermia", "unknown", "unknown", "unknown", "unknown", "unknown" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/49/", "temperate", new DateTime(2014, 12, 20, 10, 48, 36, 141, DateTimeKind.Utc), "13400", new DateTime(2014, 12, 20, 20, 58, 18, 504, DateTimeKind.Utc), "1", "Dorin", "409", "unknown", "22", "unknown", "unknown" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/50/", "temperate", new DateTime(2014, 12, 20, 10, 52, 51, 524, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 506, DateTimeKind.Utc), "1", "Champala", "318", "3500000000", "27", "unknown", "oceans, rainforests, plateaus" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/51/", "unknown", new DateTime(2014, 12, 20, 16, 44, 46, 318, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 508, DateTimeKind.Utc), "unknown", "Mirial", "unknown", "unknown", "unknown", "unknown", "deserts" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/52/", "unknown", new DateTime(2014, 12, 20, 16, 52, 13, 357, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 510, DateTimeKind.Utc), "unknown", "Serenno", "unknown", "unknown", "unknown", "unknown", "rainforests, rivers, mountains" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/53/", "unknown", new DateTime(2014, 12, 20, 16, 54, 39, 909, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 512, DateTimeKind.Utc), "unknown", "Concord Dawn", "unknown", "unknown", "unknown", "unknown", "jungles, forests, deserts" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/54/", "unknown", new DateTime(2014, 12, 20, 16, 56, 37, 250, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 514, DateTimeKind.Utc), "unknown", "Zolan", "unknown", "unknown", "unknown", "unknown", "unknown" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/55/", "frigid", new DateTime(2014, 12, 20, 17, 27, 41, 286, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 516, DateTimeKind.Utc), "unknown", "Ojom", "unknown", "500000000", "unknown", "100", "oceans, glaciers" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/56/", "temperate", new DateTime(2014, 12, 20, 17, 50, 47, 864, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 517, DateTimeKind.Utc), "1", "Skako", "384", "500000000000", "27", "unknown", "urban, vines" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/57/", "temperate", new DateTime(2014, 12, 20, 17, 57, 47, 420, DateTimeKind.Utc), "13800", new DateTime(2014, 12, 20, 20, 58, 18, 519, DateTimeKind.Utc), "1", "Muunilinst", "412", "5000000000", "28", "25", "plains, forests, hills, mountains" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/58/", "temperate", new DateTime(2014, 12, 20, 18, 43, 14, 49, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 521, DateTimeKind.Utc), "1", "Shili", "unknown", "unknown", "unknown", "unknown", "cities, savannahs, seas, plains" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/45/", "unknown", new DateTime(2014, 12, 20, 10, 26, 5, 788, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 497, DateTimeKind.Utc), "unknown", "Iridonia", "413", "unknown", "29", "unknown", "rocky canyons, acid pools" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/30/", "arid", new DateTime(2014, 12, 15, 12, 56, 31, 121, DateTimeKind.Utc), "0", new DateTime(2014, 12, 20, 20, 58, 18, 469, DateTimeKind.Utc), "1 standard", "Socorro", "326", "300000000", "20", "unknown", "deserts, mountains" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/27/", "temperate", new DateTime(2014, 12, 15, 12, 23, 41, 661, DateTimeKind.Utc), "14050", new DateTime(2014, 12, 20, 20, 58, 18, 464, DateTimeKind.Utc), "1 standard", "Ord Mantell", "334", "4000000000", "26", "10", "plains, seas, mesas" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/28/", "unknown", new DateTime(2014, 12, 15, 12, 25, 59, 569, DateTimeKind.Utc), "0", new DateTime(2014, 12, 20, 20, 58, 18, 466, DateTimeKind.Utc), "unknown", "unknown", "0", "unknown", "0", "unknown", "unknown" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/1/", "arid", new DateTime(2014, 12, 9, 13, 50, 49, 641, DateTimeKind.Utc), "10465", new DateTime(2014, 12, 20, 20, 58, 18, 411, DateTimeKind.Utc), "1 standard", "Tatooine", "304", "200000", "23", "1", "desert" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/2/", "temperate", new DateTime(2014, 12, 10, 11, 35, 48, 479, DateTimeKind.Utc), "12500", new DateTime(2014, 12, 20, 20, 58, 18, 420, DateTimeKind.Utc), "1 standard", "Alderaan", "364", "2000000000", "24", "40", "grasslands, mountains" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/3/", "temperate, tropical", new DateTime(2014, 12, 10, 11, 37, 19, 144, DateTimeKind.Utc), "10200", new DateTime(2014, 12, 20, 20, 58, 18, 421, DateTimeKind.Utc), "1 standard", "Yavin IV", "4818", "1000", "24", "8", "jungle, rainforests" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/4/", "frozen", new DateTime(2014, 12, 10, 11, 39, 13, 934, DateTimeKind.Utc), "7200", new DateTime(2014, 12, 20, 20, 58, 18, 423, DateTimeKind.Utc), "1.1 standard", "Hoth", "549", "unknown", "23", "100", "tundra, ice caves, mountain ranges" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/5/", "murky", new DateTime(2014, 12, 10, 11, 42, 22, 590, DateTimeKind.Utc), "8900", new DateTime(2014, 12, 20, 20, 58, 18, 425, DateTimeKind.Utc), "N/A", "Dagobah", "341", "unknown", "23", "8", "swamp, jungles" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/6/", "temperate", new DateTime(2014, 12, 10, 11, 43, 55, 240, DateTimeKind.Utc), "118000", new DateTime(2014, 12, 20, 20, 58, 18, 427, DateTimeKind.Utc), "1.5 (surface), 1 standard (Cloud City)", "Bespin", "5110", "6000000", "12", "0", "gas giant" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/7/", "temperate", new DateTime(2014, 12, 10, 11, 50, 29, 349, DateTimeKind.Utc), "4900", new DateTime(2014, 12, 20, 20, 58, 18, 429, DateTimeKind.Utc), "0.85 standard", "Endor", "402", "30000000", "18", "8", "forests, mountains, lakes" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/8/", "temperate", new DateTime(2014, 12, 10, 11, 52, 31, 66, DateTimeKind.Utc), "12120", new DateTime(2014, 12, 20, 20, 58, 18, 430, DateTimeKind.Utc), "1 standard", "Naboo", "312", "4500000000", "26", "12", "grassy hills, swamps, forests, mountains" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/9/", "temperate", new DateTime(2014, 12, 10, 11, 54, 13, 921, DateTimeKind.Utc), "12240", new DateTime(2014, 12, 20, 20, 58, 18, 432, DateTimeKind.Utc), "1 standard", "Coruscant", "368", "1000000000000", "24", "unknown", "cityscape, mountains" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/10/", "temperate", new DateTime(2014, 12, 10, 12, 45, 6, 577, DateTimeKind.Utc), "19720", new DateTime(2014, 12, 20, 20, 58, 18, 434, DateTimeKind.Utc), "1 standard", "Kamino", "463", "1000000000", "27", "100", "ocean" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/11/", "temperate, arid", new DateTime(2014, 12, 10, 12, 47, 22, 350, DateTimeKind.Utc), "11370", new DateTime(2014, 12, 20, 20, 58, 18, 437, DateTimeKind.Utc), "0.9 standard", "Geonosis", "256", "100000000000", "30", "5", "rock, desert, mountain, barren" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/12/", "temperate, arid, windy", new DateTime(2014, 12, 10, 12, 49, 1, 491, DateTimeKind.Utc), "12900", new DateTime(2014, 12, 20, 20, 58, 18, 439, DateTimeKind.Utc), "1 standard", "Utapau", "351", "95000000", "27", "0.9", "scrublands, savanna, canyons, sinkholes" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/13/", "hot", new DateTime(2014, 12, 10, 12, 50, 16, 526, DateTimeKind.Utc), "4200", new DateTime(2014, 12, 20, 20, 58, 18, 440, DateTimeKind.Utc), "1 standard", "Mustafar", "412", "20000", "36", "0", "volcanoes, lava rivers, mountains, caves" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/14/", "tropical", new DateTime(2014, 12, 10, 13, 32, 0, 124, DateTimeKind.Utc), "12765", new DateTime(2014, 12, 20, 20, 58, 18, 442, DateTimeKind.Utc), "1 standard", "Kashyyyk", "381", "45000000", "26", "60", "jungle, forests, lakes, rivers" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/15/", "artificial temperate ", new DateTime(2014, 12, 10, 13, 33, 46, 405, DateTimeKind.Utc), "0", new DateTime(2014, 12, 20, 20, 58, 18, 444, DateTimeKind.Utc), "0.56 standard", "Polis Massa", "590", "1000000", "24", "0", "airless asteroid" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/16/", "frigid", new DateTime(2014, 12, 10, 13, 43, 39, 139, DateTimeKind.Utc), "10088", new DateTime(2014, 12, 20, 20, 58, 18, 446, DateTimeKind.Utc), "1 standard", "Mygeeto", "167", "19000000", "12", "unknown", "glaciers, mountains, ice canyons" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/17/", "hot, humid", new DateTime(2014, 12, 10, 13, 44, 50, 397, DateTimeKind.Utc), "9100", new DateTime(2014, 12, 20, 20, 58, 18, 447, DateTimeKind.Utc), "0.75 standard", "Felucia", "231", "8500000", "34", "unknown", "fungus forests" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/18/", "temperate, moist", new DateTime(2014, 12, 10, 13, 46, 28, 704, DateTimeKind.Utc), "0", new DateTime(2014, 12, 20, 20, 58, 18, 449, DateTimeKind.Utc), "1 standard", "Cato Neimoidia", "278", "10000000", "25", "unknown", "mountains, fields, forests, rock arches" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/19/", "hot", new DateTime(2014, 12, 10, 13, 47, 46, 874, DateTimeKind.Utc), "14920", new DateTime(2014, 12, 20, 20, 58, 18, 450, DateTimeKind.Utc), "unknown", "Saleucami", "392", "1400000000", "26", "unknown", "caves, desert, mountains, volcanoes" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/20/", "temperate", new DateTime(2014, 12, 10, 16, 16, 26, 566, DateTimeKind.Utc), "0", new DateTime(2014, 12, 20, 20, 58, 18, 452, DateTimeKind.Utc), "1 standard", "Stewjon", "unknown", "unknown", "unknown", "unknown", "grass" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/21/", "polluted", new DateTime(2014, 12, 10, 16, 26, 54, 384, DateTimeKind.Utc), "13490", new DateTime(2014, 12, 20, 20, 58, 18, 454, DateTimeKind.Utc), "1 standard", "Eriadu", "360", "22000000000", "24", "unknown", "cityscape" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/22/", "temperate", new DateTime(2014, 12, 10, 16, 49, 12, 453, DateTimeKind.Utc), "11000", new DateTime(2014, 12, 20, 20, 58, 18, 456, DateTimeKind.Utc), "1 standard", "Corellia", "329", "3000000000", "25", "70", "plains, urban, hills, forests" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/23/", "hot", new DateTime(2014, 12, 10, 17, 3, 28, 110, DateTimeKind.Utc), "7549", new DateTime(2014, 12, 20, 20, 58, 18, 458, DateTimeKind.Utc), "1 standard", "Rodia", "305", "1300000000", "29", "60", "jungles, oceans, urban, swamps" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/24/", "temperate", new DateTime(2014, 12, 10, 17, 11, 29, 452, DateTimeKind.Utc), "12150", new DateTime(2014, 12, 20, 20, 58, 18, 460, DateTimeKind.Utc), "1 standard", "Nal Hutta", "413", "7000000000", "87", "unknown", "urban, oceans, swamps, bogs" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/25/", "temperate", new DateTime(2014, 12, 10, 17, 23, 29, 896, DateTimeKind.Utc), "9830", new DateTime(2014, 12, 20, 20, 58, 18, 461, DateTimeKind.Utc), "1 standard", "Dantooine", "378", "1000", "25", "unknown", "oceans, savannas, mountains, grasslands" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/26/", "temperate", new DateTime(2014, 12, 12, 11, 16, 55, 78, DateTimeKind.Utc), "6400", new DateTime(2014, 12, 20, 20, 58, 18, 463, DateTimeKind.Utc), "unknown", "Bestine IV", "680", "62000000", "26", "98", "rocky islands, oceans" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/59/", "arid, temperate, tropical", new DateTime(2014, 12, 20, 19, 43, 51, 278, DateTimeKind.Utc), "13850", new DateTime(2014, 12, 20, 20, 58, 18, 523, DateTimeKind.Utc), "1", "Kalee", "378", "4000000000", "23", "unknown", "rainforests, cliffs, canyons, seas" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/29/", "arid", new DateTime(2014, 12, 15, 12, 53, 47, 695, DateTimeKind.Utc), "0", new DateTime(2014, 12, 20, 20, 58, 18, 468, DateTimeKind.Utc), "0.62 standard", "Trandosha", "371", "42000000", "25", "unknown", "mountains, seas, grasslands, deserts" });

            migrationBuilder.InsertData(
                table: "Planets",
                columns: new[] { "Uri", "Climate", "Created", "Diameter", "Edited", "Gravity", "Name", "OrbitalPeriod", "Population", "RotationPeriod", "SurfaceWater", "Terrain" },
                values: new object[] { "http://swapi.dev/api/planets/60/", "unknown", new DateTime(2014, 12, 20, 20, 18, 36, 256, DateTimeKind.Utc), "unknown", new DateTime(2014, 12, 20, 20, 58, 18, 525, DateTimeKind.Utc), "unknown", "Umbara", "unknown", "unknown", "unknown", "unknown", "unknown" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/1/", "http://swapi.dev/api/planets/1/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/18/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/17/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/16/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/15/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/14/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/13/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/12/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/5/", "http://swapi.dev/api/planets/11/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/5/", "http://swapi.dev/api/planets/10/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/9/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/5/", "http://swapi.dev/api/planets/9/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/4/", "http://swapi.dev/api/planets/9/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/3/", "http://swapi.dev/api/planets/9/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/8/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/19/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/5/", "http://swapi.dev/api/planets/8/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/3/", "http://swapi.dev/api/planets/8/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/3/", "http://swapi.dev/api/planets/7/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/2/", "http://swapi.dev/api/planets/6/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/5/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/3/", "http://swapi.dev/api/planets/5/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/2/", "http://swapi.dev/api/planets/5/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/2/", "http://swapi.dev/api/planets/4/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/1/", "http://swapi.dev/api/planets/3/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/2/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/1/", "http://swapi.dev/api/planets/2/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/6/", "http://swapi.dev/api/planets/1/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/5/", "http://swapi.dev/api/planets/1/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/4/", "http://swapi.dev/api/planets/1/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/3/", "http://swapi.dev/api/planets/1/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/4/", "http://swapi.dev/api/planets/8/" });

            migrationBuilder.InsertData(
                table: "MoviePlanet",
                columns: new[] { "MovieUri", "PlanetUri" },
                values: new object[] { "http://swapi.dev/api/films/2/", "http://swapi.dev/api/planets/27/" });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePlanet_PlanetUri",
                table: "MoviePlanet",
                column: "PlanetUri");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviePlanet");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Planets");
        }
    }
}
