﻿// <auto-generated />
using System;
using Gp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gp.Migrations
{
    [DbContext(typeof(SystemDbContext))]
    partial class SystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gp.Models.Branch", b =>
                {
                    b.Property<int>("BranchID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BranchID"));

                    b.Property<TimeSpan?>("CloseTime")
                        .IsRequired()
                        .HasColumnType("time");

                    b.Property<bool>("HasIndoorSeating")
                        .HasColumnType("bit");

                    b.Property<bool>("HasOutdoorSeating")
                        .HasColumnType("bit");

                    b.Property<string>("ImageFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumOfTables")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("OpenTime")
                        .IsRequired()
                        .HasColumnType("time");

                    b.Property<int>("RestaurantID")
                        .HasColumnType("int");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("BranchID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("Gp.Models.Dish", b =>
                {
                    b.Property<int>("DishID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DishID"));

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DishCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("DishName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("ImageFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("DishID");

                    b.HasIndex("DishCategoryID");

                    b.ToTable("Dish");
                });

            modelBuilder.Entity("Gp.Models.DishCategory", b =>
                {
                    b.Property<int>("DishCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DishCategoryID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RestaurantID")
                        .HasColumnType("int");

                    b.HasKey("DishCategoryID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("DishCategory");
                });

            modelBuilder.Entity("Gp.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("BranchID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeTypeID")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("EmployeeID");

                    b.HasIndex("BranchID");

                    b.HasIndex("EmployeeTypeID");

                    b.HasIndex("PhoneNum")
                        .IsUnique();

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Gp.Models.EmployeeType", b =>
                {
                    b.Property<int>("EmployeeTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeTypeID"));

                    b.Property<string>("EmpType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeTypeID");

                    b.ToTable("EmployeeType");
                });

            modelBuilder.Entity("Gp.Models.Favorite", b =>
                {
                    b.Property<int>("FavoriteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavoriteID"));

                    b.Property<int>("BranchID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("FavoriteID");

                    b.HasIndex("BranchID");

                    b.HasIndex("UserID");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("Gp.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<int>("ID_Reservation_Dish")
                        .HasColumnType("int");

                    b.Property<bool>("IsTaken")
                        .HasColumnType("bit");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TableNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderID");

                    b.HasIndex("ID_Reservation_Dish");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Gp.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationID"));

                    b.Property<int>("BranchID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumOfPeople")
                        .HasColumnType("int");

                    b.Property<int>("ReservationStatusID")
                        .HasColumnType("int");

                    b.Property<string>("TableZone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ReservationID");

                    b.HasIndex("BranchID");

                    b.HasIndex("ReservationStatusID");

                    b.HasIndex("UserID");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("Gp.Models.ReservationDish", b =>
                {
                    b.Property<int>("ID_Reservation_Dish")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Reservation_Dish"));

                    b.Property<int>("ID_Dish")
                        .HasColumnType("int");

                    b.Property<int>("ID_Reservation")
                        .HasColumnType("int");

                    b.HasKey("ID_Reservation_Dish");

                    b.HasIndex("ID_Dish");

                    b.HasIndex("ID_Reservation");

                    b.ToTable("ReservationDish");
                });

            modelBuilder.Entity("Gp.Models.ReservationStatus", b =>
                {
                    b.Property<int>("ReservationStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationStatusID"));

                    b.Property<string>("ReservationnStatus")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ReservationStatusID");

                    b.HasIndex("ReservationnStatus")
                        .IsUnique();

                    b.ToTable("ReservationStatus");
                });

            modelBuilder.Entity("Gp.Models.Restaurant", b =>
                {
                    b.Property<int>("RestaurantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestaurantID"));

                    b.Property<string>("ImageFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestaurantName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("RestaurantID");

                    b.HasIndex("RestaurantName")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("Gp.Models.Review", b =>
                {
                    b.Property<int>("ReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewID"));

                    b.Property<int>("BranchID")
                        .HasColumnType("int");

                    b.Property<string>("CustomerReview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ReviewID");

                    b.HasIndex("BranchID");

                    b.HasIndex("UserID");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("Gp.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UserTypeID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.HasIndex("UserTypeID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Gp.Models.UserType", b =>
                {
                    b.Property<int>("UserTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTypeID"));

                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeID");

                    b.ToTable("UserType");
                });

            modelBuilder.Entity("Gp.Models.Branch", b =>
                {
                    b.HasOne("Gp.Models.Restaurant", "Restaurant")
                        .WithMany("Branches")
                        .HasForeignKey("RestaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Gp.Models.Dish", b =>
                {
                    b.HasOne("Gp.Models.DishCategory", "DishCategory")
                        .WithMany("Dishes")
                        .HasForeignKey("DishCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DishCategory");
                });

            modelBuilder.Entity("Gp.Models.DishCategory", b =>
                {
                    b.HasOne("Gp.Models.Restaurant", "Restaurant")
                        .WithMany("DishCategories")
                        .HasForeignKey("RestaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Gp.Models.Employee", b =>
                {
                    b.HasOne("Gp.Models.Branch", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gp.Models.EmployeeType", "EmployeeType")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeeTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("EmployeeType");
                });

            modelBuilder.Entity("Gp.Models.Favorite", b =>
                {
                    b.HasOne("Gp.Models.Branch", "Branch")
                        .WithMany("Favorites")
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gp.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gp.Models.Order", b =>
                {
                    b.HasOne("Gp.Models.ReservationDish", "ReservationDish")
                        .WithMany("Orders")
                        .HasForeignKey("ID_Reservation_Dish")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReservationDish");
                });

            modelBuilder.Entity("Gp.Models.Reservation", b =>
                {
                    b.HasOne("Gp.Models.Branch", "Branch")
                        .WithMany("Reservations")
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gp.Models.ReservationStatus", "ReservationStatus")
                        .WithMany("Reservations")
                        .HasForeignKey("ReservationStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gp.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("ReservationStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gp.Models.ReservationDish", b =>
                {
                    b.HasOne("Gp.Models.Dish", "Dish")
                        .WithMany("ReservationDishes")
                        .HasForeignKey("ID_Dish")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gp.Models.Reservation", "Reservation")
                        .WithMany("ReservationDishes")
                        .HasForeignKey("ID_Reservation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("Gp.Models.Restaurant", b =>
                {
                    b.HasOne("Gp.Models.User", "User")
                        .WithMany("Restaurants")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gp.Models.Review", b =>
                {
                    b.HasOne("Gp.Models.Branch", "Branch")
                        .WithMany("Reviews")
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gp.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gp.Models.User", b =>
                {
                    b.HasOne("Gp.Models.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("Gp.Models.Branch", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Favorites");

                    b.Navigation("Reservations");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Gp.Models.Dish", b =>
                {
                    b.Navigation("ReservationDishes");
                });

            modelBuilder.Entity("Gp.Models.DishCategory", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("Gp.Models.EmployeeType", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Gp.Models.Reservation", b =>
                {
                    b.Navigation("ReservationDishes");
                });

            modelBuilder.Entity("Gp.Models.ReservationDish", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Gp.Models.ReservationStatus", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Gp.Models.Restaurant", b =>
                {
                    b.Navigation("Branches");

                    b.Navigation("DishCategories");
                });

            modelBuilder.Entity("Gp.Models.User", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Reservations");

                    b.Navigation("Restaurants");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Gp.Models.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
