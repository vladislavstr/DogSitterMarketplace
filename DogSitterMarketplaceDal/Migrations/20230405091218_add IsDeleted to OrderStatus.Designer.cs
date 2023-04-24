﻿// <auto-generated />
using System;
using DogSitterMarketplaceDal;
using DogSitterMarketplaceDal.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DogSitterMarketplaceDal.Migrations
{
    [DbContext(typeof(OrdersAndPetsAndCommentsContext))]
    [Migration("20230405091218_add IsDeleted to OrderStatus")]
    partial class addIsDeletedtoOrderStatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Appeals.AppealEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppealFromUserId")
                        .HasColumnType("int");

                    b.Property<int?>("AppealToUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppealFromUserId");

                    b.HasIndex("AppealToUserId");

                    b.HasIndex("OrderId");

                    b.HasIndex("StatusId");

                    b.HasIndex("AnimalTypeId");

                    b.ToTable("AppealEntity");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Appeals.AppealStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppealStatusEntity");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Appeals.AppealTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppealTypeEntity");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Orders.CommentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CommentFromUserId")
                        .HasColumnType("int");

                    b.Property<int>("CommentToUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommentFromUserId");

                    b.HasIndex("CommentToUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Orders.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<int>("SitterWorkId")
                        .HasColumnType("int");

                    b.Property<int>("Summ")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("SitterWorkId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Orders.OrderStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Orders.PetsInOrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PetId");

                    b.ToTable("PetsInOrders");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Pets.AnimalTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Parameters")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("AnimalsTypes");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Pets.PetEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Characteristics")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AnimalTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Users.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PassportDataId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PassportDataId");

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Users.UserPassportDataEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserPassportDataEntity");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Users.UserRoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRoleEntity");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Users.UserStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserStatusEntity");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Works.LocationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Works.SitterWorkEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WorkTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WorkTypeId");

                    b.ToTable("SitterWork");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Works.WorkTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WorkTypeEntity");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Appeals.AppealEntity", b =>
                {
                    b.HasOne("DogSitterMarketplaceDal.Models.Users.UserEntity", "AppealFromUser")
                        .WithMany()
                        .HasForeignKey("AppealFromUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Users.UserEntity", "AppealToUser")
                        .WithMany()
                        .HasForeignKey("AppealToUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DogSitterMarketplaceDal.Models.Orders.OrderEntity", "Order")
                        .WithMany("Appeals")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DogSitterMarketplaceDal.Models.Appeals.AppealStatusEntity", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Appeals.AppealTypeEntity", "AnimalType")
                        .WithMany()
                        .HasForeignKey("AnimalTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppealFromUser");

                    b.Navigation("AppealToUser");

                    b.Navigation("Order");

                    b.Navigation("Status");

                    b.Navigation("AnimalType");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Orders.CommentEntity", b =>
                {
                    b.HasOne("DogSitterMarketplaceDal.Models.Users.UserEntity", "CommentFromUser")
                        .WithMany()
                        .HasForeignKey("CommentFromUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Users.UserEntity", "CommentToUser")
                        .WithMany()
                        .HasForeignKey("CommentToUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Orders.OrderEntity", "Order")
                        .WithMany("Comments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CommentFromUser");

                    b.Navigation("CommentToUser");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Orders.OrderEntity", b =>
                {
                    b.HasOne("DogSitterMarketplaceDal.Models.Works.LocationEntity", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Orders.OrderStatusEntity", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Works.SitterWorkEntity", "SitterWork")
                        .WithMany()
                        .HasForeignKey("SitterWorkId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("OrderStatus");

                    b.Navigation("SitterWork");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Orders.PetsInOrderEntity", b =>
                {
                    b.HasOne("DogSitterMarketplaceDal.Models.Orders.OrderEntity", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Pets.PetEntity", "Pet")
                        .WithMany()
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Pets.PetEntity", b =>
                {
                    b.HasOne("DogSitterMarketplaceDal.Models.Pets.AnimalTypeEntity", "AnimalType")
                        .WithMany()
                        .HasForeignKey("AnimalTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Users.UserEntity", "User")
                        .WithMany("Pets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AnimalType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Users.UserEntity", b =>
                {
                    b.HasOne("DogSitterMarketplaceDal.Models.Users.UserPassportDataEntity", "PassportData")
                        .WithMany()
                        .HasForeignKey("PassportDataId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DogSitterMarketplaceDal.Models.Users.UserRoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Users.UserStatusEntity", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PassportData");

                    b.Navigation("Role");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Works.SitterWorkEntity", b =>
                {
                    b.HasOne("DogSitterMarketplaceDal.Models.Users.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DogSitterMarketplaceDal.Models.Works.WorkTypeEntity", "WorkType")
                        .WithMany()
                        .HasForeignKey("WorkTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkType");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Orders.OrderEntity", b =>
                {
                    b.Navigation("Appeals");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("DogSitterMarketplaceDal.Models.Users.UserEntity", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
