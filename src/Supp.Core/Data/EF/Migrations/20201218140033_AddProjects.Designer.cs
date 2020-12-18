﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Supp.Core.Data.EF;

namespace Supp.Core.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201218140033_AddProjects")]
    partial class AddProjects
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Supp.Core.Posts.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Archived")
                        .HasColumnType("bit");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Supp.Core.Posts.PostRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChildId");

                    b.HasIndex("ParentId");

                    b.ToTable("PostRelations");
                });

            modelBuilder.Entity("Supp.Core.Projects.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Archived")
                        .HasColumnType("bit");

                    b.Property<int>("ProjectOptionsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectOptionsId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Supp.Core.Projects.ProjectOptions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("FlexibleTags")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("ProjectOptions");
                });

            modelBuilder.Entity("Supp.Core.Posts.Post", b =>
                {
                    b.HasOne("Supp.Core.Projects.Project", "Project")
                        .WithMany("Posts")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Supp.Core.Posts.PostRelation", b =>
                {
                    b.HasOne("Supp.Core.Posts.Post", "Child")
                        .WithMany("Children")
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Supp.Core.Posts.Post", "Parent")
                        .WithMany("Parents")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Child");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Supp.Core.Projects.Project", b =>
                {
                    b.HasOne("Supp.Core.Projects.ProjectOptions", "ProjectOptions")
                        .WithMany()
                        .HasForeignKey("ProjectOptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectOptions");
                });

            modelBuilder.Entity("Supp.Core.Posts.Post", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Parents");
                });

            modelBuilder.Entity("Supp.Core.Projects.Project", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
