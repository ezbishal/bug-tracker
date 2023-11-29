﻿// <auto-generated />
using System;
using BugTrackerApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BugTrackerApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("BugTrackerApi.BugModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("project", (string)null);
                });

            modelBuilder.Entity("BugTrackerApi.CommentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BugId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BugId");

                    b.ToTable("comment", (string)null);
                });

            modelBuilder.Entity("BugTrackerApi.Models.ProjectModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("course", (string)null);
                });

            modelBuilder.Entity("BugTrackerApi.BugModel", b =>
                {
                    b.HasOne("BugTrackerApi.Models.ProjectModel", "Project")
                        .WithMany("Bugs")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("BugTrackerApi.CommentModel", b =>
                {
                    b.HasOne("BugTrackerApi.BugModel", "Bug")
                        .WithMany("Comments")
                        .HasForeignKey("BugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bug");
                });

            modelBuilder.Entity("BugTrackerApi.BugModel", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("BugTrackerApi.Models.ProjectModel", b =>
                {
                    b.Navigation("Bugs");
                });
#pragma warning restore 612, 618
        }
    }
}
