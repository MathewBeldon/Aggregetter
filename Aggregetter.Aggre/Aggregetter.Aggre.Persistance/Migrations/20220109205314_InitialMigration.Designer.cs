﻿// <auto-generated />
using System;
using Aggregetter.Aggre.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aggregetter.Aggre.Persistance.Migrations
{
    [DbContext(typeof(AggreDbContext))]
    [Migration("20220109205314_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ArticleSlug")
                        .HasColumnType("tinytext");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Endpoint")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OriginalBody")
                        .HasColumnType("text");

                    b.Property<string>("OriginalTitle")
                        .HasColumnType("tinytext");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.Property<string>("TranslatedBody")
                        .HasColumnType("text");

                    b.Property<string>("TranslatedTitle")
                        .HasColumnType("tinytext");

                    b.HasKey("Id");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("tinytext");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("tinytext");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BaseAddress")
                        .HasColumnType("tinytext");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("tinytext");

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Links.ArticleCategory", b =>
                {
                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ArticleId", "CategoryId");

                    b.ToTable("ArticleCategories");
                });
#pragma warning restore 612, 618
        }
    }
}