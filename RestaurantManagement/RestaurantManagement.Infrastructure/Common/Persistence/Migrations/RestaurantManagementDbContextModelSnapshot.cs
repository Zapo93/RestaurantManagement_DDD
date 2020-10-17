﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantManagement.Infrastructure.Common.Persistence;

namespace RestaurantManagement.Infrastructure.Common.Persistence.Migrations
{
    [DbContext(typeof(RestaurantManagementDbContext))]
    partial class RestaurantManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RestaurantManagement.Domain.Hosting.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Hosting.Models.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Hosting.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<string>("RestaurantName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Kitchen.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("QuantityInGrams")
                        .HasColumnType("int");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Kitchen.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Preparation")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Kitchen.Models.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatorReferenceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Kitchen.Models.RequestItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int?>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestItems");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Serving.Models.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Serving.Models.KitchenRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("RequestId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("KitchenRequests");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Serving.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssigneeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Open")
                        .HasColumnType("bit");

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Serving.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DishId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Hosting.Models.Reservation", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.Hosting.Models.Schedule", null)
                        .WithMany("Reservations")
                        .HasForeignKey("ScheduleId");

                    b.OwnsOne("RestaurantManagement.Domain.Common.DateTimeRange", "TimeRange", b1 =>
                        {
                            b1.Property<int>("ReservationId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("End")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("datetime2");

                            b1.HasKey("ReservationId");

                            b1.ToTable("Reservations");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");
                        });

                    b.OwnsOne("RestaurantManagement.Domain.Hosting.Models.Guest", "Guest", b1 =>
                        {
                            b1.Property<int>("ReservationId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ReservationId");

                            b1.ToTable("Reservations");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Hosting.Models.Schedule", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.Hosting.Models.Table", null)
                        .WithMany("Schedules")
                        .HasForeignKey("TableId");

                    b.OwnsOne("RestaurantManagement.Domain.Common.DateTimeRange", "TimeRange", b1 =>
                        {
                            b1.Property<int>("ScheduleId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("End")
                                .HasColumnType("datetime2");

                            b1.Property<DateTime>("Start")
                                .HasColumnType("datetime2");

                            b1.HasKey("ScheduleId");

                            b1.ToTable("Schedules");

                            b1.WithOwner()
                                .HasForeignKey("ScheduleId");
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Hosting.Models.Table", b =>
                {
                    b.OwnsOne("RestaurantManagement.Domain.Hosting.Models.TableDescription", "Description", b1 =>
                        {
                            b1.Property<int>("TableId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<bool>("AreSmokersAllowed")
                                .HasColumnType("bit");

                            b1.Property<bool>("IsIndoor")
                                .HasColumnType("bit");

                            b1.Property<string>("Location")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TableId");

                            b1.ToTable("Tables");

                            b1.WithOwner()
                                .HasForeignKey("TableId");
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Kitchen.Models.Ingredient", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.Kitchen.Models.Recipe", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Kitchen.Models.Request", b =>
                {
                    b.OwnsOne("RestaurantManagement.Domain.Kitchen.Models.RequestStatus", "Status", b1 =>
                        {
                            b1.Property<int>("RequestId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("RequestId");

                            b1.ToTable("Requests");

                            b1.WithOwner()
                                .HasForeignKey("RequestId");
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Kitchen.Models.RequestItem", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.Kitchen.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantManagement.Domain.Kitchen.Models.Request", null)
                        .WithMany("Items")
                        .HasForeignKey("RequestId");

                    b.OwnsOne("RestaurantManagement.Domain.Kitchen.Models.RequestStatus", "Status", b1 =>
                        {
                            b1.Property<int>("RequestItemId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("RequestItemId");

                            b1.ToTable("RequestItems");

                            b1.WithOwner()
                                .HasForeignKey("RequestItemId");
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Serving.Models.Dish", b =>
                {
                    b.OwnsOne("RestaurantManagement.Domain.Serving.Models.Money", "Price", b1 =>
                        {
                            b1.Property<int>("DishId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("CurrencyAbbreviation")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<double>("Value")
                                .HasColumnType("float");

                            b1.HasKey("DishId");

                            b1.ToTable("Dishes");

                            b1.WithOwner()
                                .HasForeignKey("DishId");
                        });
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Serving.Models.KitchenRequest", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.Serving.Models.Order", null)
                        .WithMany("KitchenRequests")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("RestaurantManagement.Domain.Serving.Models.OrderItem", b =>
                {
                    b.HasOne("RestaurantManagement.Domain.Serving.Models.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantManagement.Domain.Serving.Models.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
