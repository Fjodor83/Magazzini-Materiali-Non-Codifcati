﻿// <auto-generated />
using System;
using MagazziniMaterialiAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagazziniMaterialiAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240929152833_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClassificazioneMateriale", b =>
                {
                    b.Property<string>("ClassificazioniCodiceClassificazione")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MaterialiId")
                        .HasColumnType("int");

                    b.HasKey("ClassificazioniCodiceClassificazione", "MaterialiId");

                    b.HasIndex("MaterialiId");

                    b.ToTable("ClassificazioneMateriale");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.Classificazione", b =>
                {
                    b.Property<string>("CodiceClassificazione")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NomeClassificazione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CodiceClassificazione");

                    b.ToTable("Classificazioni");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.DettaglioMissione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodiceMateriale")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MissionePrelievoId")
                        .HasColumnType("int");

                    b.Property<int>("QuantitaPrelevata")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CodiceMateriale");

                    b.HasIndex("MissionePrelievoId");

                    b.ToTable("DettagliMissione");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.Giacenza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodiceMateriale")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MagazzinoId")
                        .HasColumnType("int");

                    b.Property<int>("QuantitaDisponibile")
                        .HasColumnType("int");

                    b.Property<int>("QuantitaImpegnata")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CodiceMateriale");

                    b.HasIndex("MagazzinoId");

                    b.ToTable("Giacenze");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.Magazzino", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodiceMagazzino")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescrizioneMagazzino")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeMagazzino")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Magazzini");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.Materiale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodiceMateriale")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataCreazione")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Materiali");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.MaterialeImmagine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsPrincipale")
                        .HasColumnType("bit");

                    b.Property<int>("MaterialeId")
                        .HasColumnType("int");

                    b.Property<string>("QRCodeData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlImmagine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MaterialeId");

                    b.ToTable("MaterialeImmagini");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.MaterialeMagazzino", b =>
                {
                    b.Property<int>("MaterialeMagazzinoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialeMagazzinoID"));

                    b.Property<string>("CodiceMateriale")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MagazzinoID")
                        .HasColumnType("int");

                    b.HasKey("MaterialeMagazzinoID");

                    b.HasIndex("CodiceMateriale");

                    b.HasIndex("MagazzinoID");

                    b.ToTable("MaterialeMagazzini");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.MissionePrelievo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodiceUnivoco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatoreId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Stato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipologiaDestinazione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OperatoreId");

                    b.ToTable("MissioniPrelievo");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.Movimentazione", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodiceMateriale")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataMovimentazione")
                        .HasColumnType("datetime2");

                    b.Property<int>("MagazzinoId")
                        .HasColumnType("int");

                    b.Property<string>("Nota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.Property<string>("TipoMovimentazione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CodiceMateriale");

                    b.HasIndex("MagazzinoId");

                    b.ToTable("Movimentazioni");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ClassificazioneMateriale", b =>
                {
                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Classificazione", null)
                        .WithMany()
                        .HasForeignKey("ClassificazioniCodiceClassificazione")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Materiale", null)
                        .WithMany()
                        .HasForeignKey("MaterialiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.DettaglioMissione", b =>
                {
                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Materiale", "Materiale")
                        .WithMany()
                        .HasForeignKey("CodiceMateriale")
                        .HasPrincipalKey("CodiceMateriale")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.MissionePrelievo", "MissionePrelievo")
                        .WithMany("DettagliMissione")
                        .HasForeignKey("MissionePrelievoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Materiale");

                    b.Navigation("MissionePrelievo");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.Giacenza", b =>
                {
                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Materiale", "Materiale")
                        .WithMany()
                        .HasForeignKey("CodiceMateriale")
                        .HasPrincipalKey("CodiceMateriale")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Magazzino", "Magazzino")
                        .WithMany()
                        .HasForeignKey("MagazzinoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Magazzino");

                    b.Navigation("Materiale");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.MaterialeImmagine", b =>
                {
                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Materiale", "Materiale")
                        .WithMany("Immagini")
                        .HasForeignKey("MaterialeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Materiale");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.MaterialeMagazzino", b =>
                {
                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Materiale", "Materiale")
                        .WithMany("MaterialeMagazzini")
                        .HasForeignKey("CodiceMateriale")
                        .HasPrincipalKey("CodiceMateriale")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Magazzino", "Magazzino")
                        .WithMany("MaterialeMagazzini")
                        .HasForeignKey("MagazzinoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Magazzino");

                    b.Navigation("Materiale");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.MissionePrelievo", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Operatore")
                        .WithMany()
                        .HasForeignKey("OperatoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operatore");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.Movimentazione", b =>
                {
                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Materiale", "Materiale")
                        .WithMany()
                        .HasForeignKey("CodiceMateriale")
                        .HasPrincipalKey("CodiceMateriale")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MagazziniMaterialiAPI.Models.Entity.Magazzino", "Magazzino")
                        .WithMany()
                        .HasForeignKey("MagazzinoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Magazzino");

                    b.Navigation("Materiale");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.Magazzino", b =>
                {
                    b.Navigation("MaterialeMagazzini");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.Materiale", b =>
                {
                    b.Navigation("Immagini");

                    b.Navigation("MaterialeMagazzini");
                });

            modelBuilder.Entity("MagazziniMaterialiAPI.Models.Entity.MissionePrelievo", b =>
                {
                    b.Navigation("DettagliMissione");
                });
#pragma warning restore 612, 618
        }
    }
}
