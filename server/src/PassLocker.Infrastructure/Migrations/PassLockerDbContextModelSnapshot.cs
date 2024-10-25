﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PassLocker.Infrastructure.Connection;

#nullable disable

namespace PassLocker.Infrastructure.Migrations
{
    [DbContext(typeof(PassLockerDbContext))]
    partial class PassLockerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PassLocker.Domain.Entity.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("account_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("account_password");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("account_name");

                    b.HasIndex("PlatformId");

                    b.HasIndex("TagId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("PassLocker.Domain.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("category_state");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("category_name");

                    b.HasKey("Id")
                        .HasName("category_id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("PassLocker.Domain.Entity.Owner", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    b.Property<string>("MasterPassword")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("varchar(120)")
                        .HasColumnName("owner_password");

                    b.HasKey("Id")
                        .HasName("owner_id");

                    b.ToTable("Owner", (string)null);
                });

            modelBuilder.Entity("PassLocker.Domain.Entity.Platform", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("platform_state");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("platform_name");

                    b.Property<string>("UrlImage")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("platform_urlimage");

                    b.HasKey("Id")
                        .HasName("platform_id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Platform", (string)null);
                });

            modelBuilder.Entity("PassLocker.Domain.Entity.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("tag_description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("tag_state");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasColumnName("tag_name");

                    b.HasKey("Id")
                        .HasName("tag_id");

                    b.ToTable("Tag", (string)null);
                });

            modelBuilder.Entity("PassLocker.Domain.Entity.Account", b =>
                {
                    b.HasOne("PassLocker.Domain.Entity.Platform", "Platform")
                        .WithMany("Accounts")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PassLocker.Domain.Entity.Tag", "Tag")
                        .WithMany("Accounts")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Platform");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("PassLocker.Domain.Entity.Platform", b =>
                {
                    b.HasOne("PassLocker.Domain.Entity.Category", "Category")
                        .WithMany("Platforms")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PassLocker.Domain.Entity.Category", b =>
                {
                    b.Navigation("Platforms");
                });

            modelBuilder.Entity("PassLocker.Domain.Entity.Platform", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("PassLocker.Domain.Entity.Tag", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}