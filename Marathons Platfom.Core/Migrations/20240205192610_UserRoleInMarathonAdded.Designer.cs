﻿// <auto-generated />
using System;
using Marathons_Platfom.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Marathons_Platfom.Core.Migrations
{
    [DbContext(typeof(MarathonPlatformContext))]
    [Migration("20240205192610_UserRoleInMarathonAdded")]
    partial class UserRoleInMarathonAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Badge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Claimed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Badges");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayNum")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MarathonId")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MarathonId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Marathon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("NumOfParticipants")
                        .HasColumnType("int");

                    b.Property<int>("ThemeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ThemeId");

                    b.ToTable("Marathons");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Progress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MarathonId")
                        .HasColumnType("int");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarathonId");

                    b.HasIndex("UserId");

                    b.ToTable("Progresses");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AdminStatus")
                        .HasColumnType("bit");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.UserRoleInMarathon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MarathonId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarathonId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoleInMarathon");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.User_Badge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BadgeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WhenClaimed")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BadgeId");

                    b.HasIndex("UserId");

                    b.ToTable("Users_Badges");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Exercise", b =>
                {
                    b.HasOne("Marathons_Platfom.Core.Entities.Marathon", "Marathon")
                        .WithMany("Exercises")
                        .HasForeignKey("MarathonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marathon");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Marathon", b =>
                {
                    b.HasOne("Marathons_Platfom.Core.Entities.Theme", "Theme")
                        .WithMany("Marathons")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Progress", b =>
                {
                    b.HasOne("Marathons_Platfom.Core.Entities.Marathon", "Marathon")
                        .WithMany("Progresses")
                        .HasForeignKey("MarathonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Marathons_Platfom.Core.Entities.User", "User")
                        .WithMany("Progresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marathon");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.UserRoleInMarathon", b =>
                {
                    b.HasOne("Marathons_Platfom.Core.Entities.Marathon", "Marathon")
                        .WithMany("UserRoleInMarathons")
                        .HasForeignKey("MarathonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Marathons_Platfom.Core.Entities.Role", "Role")
                        .WithMany("UserRolesInMarathon")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Marathons_Platfom.Core.Entities.User", "User")
                        .WithMany("UsersRoleInMarathon")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marathon");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.User_Badge", b =>
                {
                    b.HasOne("Marathons_Platfom.Core.Entities.Badge", "Badge")
                        .WithMany("Users_Badge")
                        .HasForeignKey("BadgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Marathons_Platfom.Core.Entities.User", "User")
                        .WithMany("User_Badges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Badge");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Badge", b =>
                {
                    b.Navigation("Users_Badge");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Marathon", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("Progresses");

                    b.Navigation("UserRoleInMarathons");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Role", b =>
                {
                    b.Navigation("UserRolesInMarathon");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.Theme", b =>
                {
                    b.Navigation("Marathons");
                });

            modelBuilder.Entity("Marathons_Platfom.Core.Entities.User", b =>
                {
                    b.Navigation("Progresses");

                    b.Navigation("User_Badges");

                    b.Navigation("UsersRoleInMarathon");
                });
#pragma warning restore 612, 618
        }
    }
}
