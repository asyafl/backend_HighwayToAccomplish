﻿// <auto-generated />
using System;
using DAL_HWTA;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL_HWTA.Migrations
{
    [DbContext(typeof(HwtaDbContext))]
    partial class HwtaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10");

            modelBuilder.Entity("DAL_HWTA.Entities.Goal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("GoalType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Regularity")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ValueType")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("DAL_HWTA.Entities.GoalProgress", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ActivityDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("UserGoalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserGoalId");

                    b.ToTable("GoalProgress");
                });

            modelBuilder.Entity("DAL_HWTA.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentType")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("BLOB");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL_HWTA.Entities.UserGoal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FinishDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("GoalId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<long>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GoalId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGoals");
                });

            modelBuilder.Entity("DAL_HWTA.Entities.GoalProgress", b =>
                {
                    b.HasOne("DAL_HWTA.Entities.UserGoal", "UserGoal")
                        .WithMany("GoalProgresses")
                        .HasForeignKey("UserGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL_HWTA.Entities.UserGoal", b =>
                {
                    b.HasOne("DAL_HWTA.Entities.Goal", "Goal")
                        .WithMany("UserGoals")
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL_HWTA.Entities.User", "User")
                        .WithMany("UserGoals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
