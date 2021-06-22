﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rewind.Data;

namespace Rewind.Data.Migrations
{
    [DbContext(typeof(RewindDB))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "g",
                            ConcurrencyStamp = "5b6033de-fb0f-4517-bb62-c5251d757bbd",
                            Name = "Gestor",
                            NormalizedName = "GESTOR"
                        },
                        new
                        {
                            Id = "u",
                            ConcurrencyStamp = "2b73b980-f0cc-4f5f-ac14-c0084a3edfd4",
                            Name = "Utilizador",
                            NormalizedName = "UTILIZADOR"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
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

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "ADMIN",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f35af4e0-8157-4b8f-b806-d9a85d38d15b",
                            Email = "a@aa.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "A@AA.com",
                            NormalizedUserName = "A@AA.com",
                            PasswordHash = "AQAAAAEAACcQAAAAEBIEthzu0GG3PatHhZ4tw+ELX5FQqmGyVS1JSS8IQyknb3OsoCZlzk9QhlgOQZljOw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "830f5d23-ba95-448d-98c9-c5fe79f4fcb0",
                            TwoFactorEnabled = false,
                            UserName = "a@aa.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "ADMIN",
                            RoleId = "g"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Rewind.Models.Comentarios", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comentario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Estrelas")
                        .HasColumnType("int");

                    b.Property<int>("SeriesID")
                        .HasColumnType("int");

                    b.Property<int>("UtilizadoresID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SeriesID");

                    b.HasIndex("UtilizadoresID");

                    b.ToTable("Comentarios");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Comentario = "bom",
                            Data = new DateTime(2021, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Estado = "visivel",
                            Estrelas = 5,
                            SeriesID = 1,
                            UtilizadoresID = 1
                        },
                        new
                        {
                            ID = 2,
                            Comentario = "mau",
                            Data = new DateTime(2021, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Estado = "invisivel",
                            Estrelas = 4,
                            SeriesID = 2,
                            UtilizadoresID = 2
                        },
                        new
                        {
                            ID = 3,
                            Comentario = "mais ou menos",
                            Data = new DateTime(2020, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Estado = "visivel",
                            Estrelas = 3,
                            SeriesID = 3,
                            UtilizadoresID = 3
                        });
                });

            modelBuilder.Entity("Rewind.Models.Estudios", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Estudio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Estudios");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Estudio = "ABC",
                            Pais = "Portugal"
                        },
                        new
                        {
                            ID = 2,
                            Estudio = "ACR",
                            Pais = "França"
                        },
                        new
                        {
                            ID = 3,
                            Estudio = "TCB",
                            Pais = "Espanha"
                        });
                });

            modelBuilder.Entity("Rewind.Models.Series", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("Episodios")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstudioID")
                        .HasColumnType("int");

                    b.Property<string>("Imagem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sinopse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("EstudioID");

                    b.ToTable("Series");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Ano = 2004,
                            Data = new DateTime(2021, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Episodios = 1,
                            Estado = "continuando",
                            EstudioID = 1,
                            Imagem = "a.jpg",
                            Sinopse = "Morbi laoreet neque",
                            Titulo = "Lorem ipsum"
                        },
                        new
                        {
                            ID = 2,
                            Ano = 2005,
                            Data = new DateTime(2021, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Episodios = 4,
                            Estado = "terminado",
                            EstudioID = 2,
                            Imagem = "b.jpg",
                            Sinopse = "ut erat gravida",
                            Titulo = "dolor sit amet"
                        },
                        new
                        {
                            ID = 3,
                            Ano = 2012,
                            Data = new DateTime(2020, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Episodios = 2,
                            Estado = "continuando",
                            EstudioID = 3,
                            Imagem = "c.jpg",
                            Sinopse = "Integer mattis lorem et lorem",
                            Titulo = "consectetur adipiscing elit"
                        });
                });

            modelBuilder.Entity("Rewind.Models.Utilizadores", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Utilizador")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Utilizadores");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "a@aa.com",
                            UserName = "ADMIN",
                            Utilizador = "admin"
                        },
                        new
                        {
                            ID = 2,
                            Email = "b@bb.com",
                            UserName = "UTILIZADOR",
                            Utilizador = "antonio"
                        },
                        new
                        {
                            ID = 3,
                            Email = "c@cc.com",
                            Utilizador = "tomas"
                        });
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

            modelBuilder.Entity("Rewind.Models.Comentarios", b =>
                {
                    b.HasOne("Rewind.Models.Series", "Serie")
                        .WithMany("ListaDeComentarios")
                        .HasForeignKey("SeriesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rewind.Models.Utilizadores", "Utilizador")
                        .WithMany("ListaDeComentarios")
                        .HasForeignKey("UtilizadoresID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Serie");

                    b.Navigation("Utilizador");
                });

            modelBuilder.Entity("Rewind.Models.Series", b =>
                {
                    b.HasOne("Rewind.Models.Estudios", "Estudio")
                        .WithMany("ListaDeSeries")
                        .HasForeignKey("EstudioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudio");
                });

            modelBuilder.Entity("Rewind.Models.Estudios", b =>
                {
                    b.Navigation("ListaDeSeries");
                });

            modelBuilder.Entity("Rewind.Models.Series", b =>
                {
                    b.Navigation("ListaDeComentarios");
                });

            modelBuilder.Entity("Rewind.Models.Utilizadores", b =>
                {
                    b.Navigation("ListaDeComentarios");
                });
#pragma warning restore 612, 618
        }
    }
}
