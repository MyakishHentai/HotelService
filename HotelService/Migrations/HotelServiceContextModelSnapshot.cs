﻿// <auto-generated />
using System;
using HotelService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelService.Migrations
{
    [DbContext(typeof(HotelServiceContext))]
    partial class HotelServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelService.Models.Base.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Review")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("WritingDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("ArticleId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("HotelService.Models.Base.Basket", b =>
                {
                    b.Property<int>("BasketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CostTotal")
                        .HasColumnType("money");

                    b.Property<DateTime>("PaymentDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("BasketId");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("HotelService.Models.Base.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AdministratorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Descriprion")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("BuildingId");

                    b.HasIndex(new[] { "AdministratorId" }, "UQ__Building__ACDEFED2DDF9785D")
                        .IsUnique()
                        .HasFilter("[AdministratorId] IS NOT NULL");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("HotelService.Models.Base.Feedback", b =>
                {
                    b.Property<string>("ClientId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValueSql("((5.0))");

                    b.Property<string>("Review")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("WritingDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("ClientId", "ServiceId")
                        .HasName("PK__Feedback__5A2FA124CFC962CE");

                    b.HasIndex("ServiceId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("HotelService.Models.Base.Request", b =>
                {
                    b.Property<int>("BasketId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ConclusionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<decimal>("CostTotal")
                        .HasColumnType("money");

                    b.Property<int>("RepeatTally")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("BasketId", "ServiceId")
                        .HasName("PK__Requests__338BCCB5D857870C");

                    b.HasIndex("ContractId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("HotelService.Models.Base.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HotelService.Models.Base.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BuildingId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallmoney")
                        .HasDefaultValueSql("((5000.0000))");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("SleepingPlaces")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValueSql("('STD')");

                    b.HasKey("RoomId");

                    b.HasIndex("BuildingId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelService.Models.Base.RoomContract", b =>
                {
                    b.Property<int>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CheckInDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime>("CheckOutDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ConclusionDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("ContractId")
                        .HasName("PK__RoomCont__C90D346998627DD1");

                    b.HasIndex("ClientId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomContracts");
                });

            modelBuilder.Entity("HotelService.Models.Base.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool?>("AvailableState")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("smallmoney");

                    b.Property<string>("Descriprion")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.Property<bool?>("RepeatState")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("ResponsWorker")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Subtitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasDefaultValueSql("('Daily')");

                    b.HasKey("ServiceId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ResponsWorker");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("HotelService.Models.Base.ServiceCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descriprion")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int?>("SubCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Subtitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("CategoryId")
                        .HasName("PK__ServiceC__19093A0B201580AA");

                    b.HasIndex("SubCategoryId");

                    b.HasIndex(new[] { "Title" }, "UQ__ServiceC__2CB664DC3FFC1DB2")
                        .IsUnique();

                    b.ToTable("ServiceCategories");
                });

            modelBuilder.Entity("HotelService.Models.Base.ServiceRequest", b =>
                {
                    b.Property<string>("WorkerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int>("BasketId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasDefaultValueSql("('Work done')");

                    b.Property<DateTime?>("OrderDoneDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderTakeDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("WorkerId", "ServiceId", "BasketId")
                        .HasName("PK__ServiceR__0FA2E95187395B03");

                    b.HasIndex("BasketId", "ServiceId");

                    b.ToTable("ServiceRequests");
                });

            modelBuilder.Entity("HotelService.Models.Base.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasComment("Дата рождения");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("Имя");

                    b.Property<bool?>("ForeignerStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))")
                        .HasComment("Является инностранцем");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(3)")
                        .HasComment("Пол");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasComment("Путь изображения");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("Фамилия");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasComment("Паспорт");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Отчество/Матчество");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegistrationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("(getdate())")
                        .HasComment("Дата регистрации");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("Passport")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("HotelService.Models.Base.Building", b =>
                {
                    b.HasOne("HotelService.Models.Base.User", "Administrator")
                        .WithOne("Building")
                        .HasForeignKey("HotelService.Models.Base.Building", "AdministratorId")
                        .HasConstraintName("FK__Buildings__Admin__2A4B4B5E")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Administrator");
                });

            modelBuilder.Entity("HotelService.Models.Base.Feedback", b =>
                {
                    b.HasOne("HotelService.Models.Base.User", "Client")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK__Feedback__Client__66603565")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelService.Models.Base.Service", "Service")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("FK__Feedback__Servic__6754599E")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("HotelService.Models.Base.Request", b =>
                {
                    b.HasOne("HotelService.Models.Base.Basket", "Basket")
                        .WithMany("Requests")
                        .HasForeignKey("BasketId")
                        .HasConstraintName("FK__Requests__Basket__5812160E")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelService.Models.Base.RoomContract", "Contract")
                        .WithMany("Requests")
                        .HasForeignKey("ContractId")
                        .HasConstraintName("FK__Requests__Contra__59FA5E80")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelService.Models.Base.Service", "Service")
                        .WithMany("Requests")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("FK__Requests__Servic__59063A47")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Contract");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("HotelService.Models.Base.Room", b =>
                {
                    b.HasOne("HotelService.Models.Base.Building", "Building")
                        .WithMany("Rooms")
                        .HasForeignKey("BuildingId")
                        .HasConstraintName("FK__Rooms__BuildingI__31EC6D26")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("HotelService.Models.Base.RoomContract", b =>
                {
                    b.HasOne("HotelService.Models.Base.User", "Client")
                        .WithMany("RoomContracts")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK__RoomContr__Clien__36B12243")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelService.Models.Base.Room", "Room")
                        .WithMany("RoomContracts")
                        .HasForeignKey("RoomId")
                        .HasConstraintName("FK__RoomContr__RoomI__37A5467C")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelService.Models.Base.Service", b =>
                {
                    b.HasOne("HotelService.Models.Base.ServiceCategory", "Category")
                        .WithMany("Services")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__Services__Catego__5070F446")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelService.Models.Base.User", "ResponsWorkerNavigation")
                        .WithMany("Services")
                        .HasForeignKey("ResponsWorker")
                        .HasConstraintName("FK__Services__Respon__4F7CD00D")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");

                    b.Navigation("ResponsWorkerNavigation");
                });

            modelBuilder.Entity("HotelService.Models.Base.ServiceCategory", b =>
                {
                    b.HasOne("HotelService.Models.Base.ServiceCategory", "SubCategory")
                        .WithMany("InverseSubCategory")
                        .HasForeignKey("SubCategoryId")
                        .HasConstraintName("FK__ServiceCa__SubCa__4222D4EF");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("HotelService.Models.Base.ServiceRequest", b =>
                {
                    b.HasOne("HotelService.Models.Base.User", "Worker")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("WorkerId")
                        .HasConstraintName("FK__ServiceRe__Worke__5FB337D6")
                        .IsRequired();

                    b.HasOne("HotelService.Models.Base.Request", "Request")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("BasketId", "ServiceId")
                        .HasConstraintName("FK__ServiceRequests__60A75C0F")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("Worker");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("HotelService.Models.Base.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HotelService.Models.Base.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HotelService.Models.Base.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("HotelService.Models.Base.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelService.Models.Base.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HotelService.Models.Base.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HotelService.Models.Base.Basket", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("HotelService.Models.Base.Building", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelService.Models.Base.Request", b =>
                {
                    b.Navigation("ServiceRequests");
                });

            modelBuilder.Entity("HotelService.Models.Base.Room", b =>
                {
                    b.Navigation("RoomContracts");
                });

            modelBuilder.Entity("HotelService.Models.Base.RoomContract", b =>
                {
                    b.Navigation("Requests");
                });

            modelBuilder.Entity("HotelService.Models.Base.Service", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("HotelService.Models.Base.ServiceCategory", b =>
                {
                    b.Navigation("InverseSubCategory");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("HotelService.Models.Base.User", b =>
                {
                    b.Navigation("Building");

                    b.Navigation("Feedbacks");

                    b.Navigation("RoomContracts");

                    b.Navigation("ServiceRequests");

                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
