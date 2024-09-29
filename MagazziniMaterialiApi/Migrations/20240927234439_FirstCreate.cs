using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazziniMaterialiAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classificazioni",
                columns: table => new
                {
                    CodiceClassificazione = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NomeClassificazione = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classificazioni", x => x.CodiceClassificazione);
                });

            migrationBuilder.CreateTable(
                name: "Magazzini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceMagazzino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeMagazzino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescrizioneMagazzino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazzini", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materiali",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceMateriale = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiali", x => x.Id);
                    table.UniqueConstraint("AK_Materiali_CodiceMateriale", x => x.CodiceMateriale);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissioniPrelievo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceUnivoco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipologiaDestinazione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperatoreId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissioniPrelievo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissioniPrelievo_AspNetUsers_OperatoreId",
                        column: x => x.OperatoreId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassificazioneMateriale",
                columns: table => new
                {
                    ClassificazioniCodiceClassificazione = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaterialiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassificazioneMateriale", x => new { x.ClassificazioniCodiceClassificazione, x.MaterialiId });
                    table.ForeignKey(
                        name: "FK_ClassificazioneMateriale_Classificazioni_ClassificazioniCodiceClassificazione",
                        column: x => x.ClassificazioniCodiceClassificazione,
                        principalTable: "Classificazioni",
                        principalColumn: "CodiceClassificazione",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassificazioneMateriale_Materiali_MaterialiId",
                        column: x => x.MaterialiId,
                        principalTable: "Materiali",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Giacenze",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceMateriale = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MagazzinoId = table.Column<int>(type: "int", nullable: false),
                    QuantitaDisponibile = table.Column<int>(type: "int", nullable: false),
                    QuantitaImpegnata = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giacenze", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Giacenze_Magazzini_MagazzinoId",
                        column: x => x.MagazzinoId,
                        principalTable: "Magazzini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Giacenze_Materiali_CodiceMateriale",
                        column: x => x.CodiceMateriale,
                        principalTable: "Materiali",
                        principalColumn: "CodiceMateriale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialeImmagini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlImmagine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrincipale = table.Column<bool>(type: "bit", nullable: false),
                    QRCodeData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialeImmagini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialeImmagini_Materiali_MaterialeId",
                        column: x => x.MaterialeId,
                        principalTable: "Materiali",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialeMagazzini",
                columns: table => new
                {
                    MaterialeMagazzinoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceMateriale = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MagazzinoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialeMagazzini", x => x.MaterialeMagazzinoID);
                    table.ForeignKey(
                        name: "FK_MaterialeMagazzini_Magazzini_MagazzinoID",
                        column: x => x.MagazzinoID,
                        principalTable: "Magazzini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialeMagazzini_Materiali_CodiceMateriale",
                        column: x => x.CodiceMateriale,
                        principalTable: "Materiali",
                        principalColumn: "CodiceMateriale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimentazioni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoMovimentazione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodiceMateriale = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MagazzinoId = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    DataMovimentazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nota = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentazioni", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentazioni_Magazzini_MagazzinoId",
                        column: x => x.MagazzinoId,
                        principalTable: "Magazzini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentazioni_Materiali_CodiceMateriale",
                        column: x => x.CodiceMateriale,
                        principalTable: "Materiali",
                        principalColumn: "CodiceMateriale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DettagliMissione",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissionePrelievoId = table.Column<int>(type: "int", nullable: false),
                    CodiceMateriale = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuantitaPrelevata = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DettagliMissione", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DettagliMissione_Materiali_CodiceMateriale",
                        column: x => x.CodiceMateriale,
                        principalTable: "Materiali",
                        principalColumn: "CodiceMateriale",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DettagliMissione_MissioniPrelievo_MissionePrelievoId",
                        column: x => x.MissionePrelievoId,
                        principalTable: "MissioniPrelievo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClassificazioneMateriale_MaterialiId",
                table: "ClassificazioneMateriale",
                column: "MaterialiId");

            migrationBuilder.CreateIndex(
                name: "IX_DettagliMissione_CodiceMateriale",
                table: "DettagliMissione",
                column: "CodiceMateriale");

            migrationBuilder.CreateIndex(
                name: "IX_DettagliMissione_MissionePrelievoId",
                table: "DettagliMissione",
                column: "MissionePrelievoId");

            migrationBuilder.CreateIndex(
                name: "IX_Giacenze_CodiceMateriale",
                table: "Giacenze",
                column: "CodiceMateriale");

            migrationBuilder.CreateIndex(
                name: "IX_Giacenze_MagazzinoId",
                table: "Giacenze",
                column: "MagazzinoId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialeImmagini_MaterialeId",
                table: "MaterialeImmagini",
                column: "MaterialeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialeMagazzini_CodiceMateriale",
                table: "MaterialeMagazzini",
                column: "CodiceMateriale");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialeMagazzini_MagazzinoID",
                table: "MaterialeMagazzini",
                column: "MagazzinoID");

            migrationBuilder.CreateIndex(
                name: "IX_MissioniPrelievo_OperatoreId",
                table: "MissioniPrelievo",
                column: "OperatoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentazioni_CodiceMateriale",
                table: "Movimentazioni",
                column: "CodiceMateriale");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentazioni_MagazzinoId",
                table: "Movimentazioni",
                column: "MagazzinoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClassificazioneMateriale");

            migrationBuilder.DropTable(
                name: "DettagliMissione");

            migrationBuilder.DropTable(
                name: "Giacenze");

            migrationBuilder.DropTable(
                name: "MaterialeImmagini");

            migrationBuilder.DropTable(
                name: "MaterialeMagazzini");

            migrationBuilder.DropTable(
                name: "Movimentazioni");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Classificazioni");

            migrationBuilder.DropTable(
                name: "MissioniPrelievo");

            migrationBuilder.DropTable(
                name: "Magazzini");

            migrationBuilder.DropTable(
                name: "Materiali");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
