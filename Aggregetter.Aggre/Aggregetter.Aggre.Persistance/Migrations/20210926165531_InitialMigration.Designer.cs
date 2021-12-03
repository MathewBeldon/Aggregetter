﻿// <auto-generated />
using System;
using Aggregetter.Aggre.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aggregetter.Aggre.Persistance.Migrations
{
    [DbContext(typeof(AggreDbContext))]
    [Migration("20210926165531_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Article", b =>
                {
                    b.Property<Guid>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

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

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TranslatedBody")
                        .HasColumnType("text");

                    b.Property<string>("TranslatedTitle")
                        .HasColumnType("tinytext");

                    b.HasKey("ArticleId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProviderId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("tinytext");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Language", b =>
                {
                    b.Property<Guid>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("tinytext");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Provider", b =>
                {
                    b.Property<Guid>("ProviderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("BaseAddress")
                        .HasColumnType("tinytext");

                    b.Property<DateTime>("CreatedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ModifiedDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("tinytext");

                    b.HasKey("ProviderId");

                    b.HasIndex("LanguageId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Article", b =>
                {
                    b.HasOne("Aggregetter.Aggre.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aggregetter.Aggre.Domain.Entities.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Aggregetter.Aggre.Domain.Entities.Provider", b =>
                {
                    b.HasOne("Aggregetter.Aggre.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });
#pragma warning restore 612, 618
        }
    }
}
