﻿using HotelService.Models.Base;
using HotelService.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#nullable disable

namespace HotelService.Models
{
    public sealed class HotelServiceContext : IdentityDbContext<User, Role, string>
    {
        public HotelServiceContext()
        {
        }

        public HotelServiceContext(DbContextOptions<HotelServiceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public  DbSet<Article> Articles { get; set; }
        public  DbSet<Basket> Baskets { get; set; }
        public  DbSet<Building> Buildings { get; set; }
        public  DbSet<Feedback> Feedbacks { get; set; }
        public  DbSet<Request> Requests { get; set; }
        public  DbSet<Room> Rooms { get; set; }
        public  DbSet<RoomContract> RoomContracts { get; set; }
        public  DbSet<Base.Service> Services { get; set; }
        public  DbSet<ServiceCategory> ServiceCategories { get; set; }
        public  DbSet<ServiceRequest> ServiceRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);Database=HotelService;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.ImagePath).HasMaxLength(1000);
                entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
                entity.Property(e => e.ForeignerStatus)
                    .IsRequired()
                    .HasDefaultValueSql("((0))");
            });
            modelBuilder.Entity<User>().HasIndex(u => u.Passport).IsUnique();

            modelBuilder.Entity<User>().ToTable(nameof(User) + 's');
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<Role>().ToTable(nameof(Role) + 's');

            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WritingDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.Property(e => e.CostTotal).HasColumnType("money");

                entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.HasIndex(e => e.AdministratorId, "UQ__Building__ACDEFED26368AF29")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Administrator)
                    .WithOne(p => p.Building)
                    .HasForeignKey<Building>(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Buildings__Admin__29572725");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.ServiceId })
                    .HasName("PK__Feedback__5A2FA124DD1DECCD");

                entity.ToTable("Feedback");

                entity.Property(e => e.Rating).HasDefaultValueSql("((5.0))");

                entity.Property(e => e.Review).HasMaxLength(500);

                entity.Property(e => e.WritingDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Feedback__Client__66603565");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Feedback__Servic__6754599E");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => new { e.BasketId, e.ServiceId })
                    .HasName("PK__Requests__338BCCB5262ABF82");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.ConclusionDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CostTotal).HasColumnType("money");

                entity.Property(e => e.RepeatTally).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Basket)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.BasketId)
                    .HasConstraintName("FK__Requests__Basket__5812160E");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK__Requests__Contra__59FA5E80");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Requests__Servic__59063A47");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Cost)
                    .HasColumnType("smallmoney")
                    .HasDefaultValueSql("((5000.0000))");

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.SleepingPlaces).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('STD')");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK__Rooms__BuildingI__30F848ED");
            });

            modelBuilder.Entity<RoomContract>(entity =>
            {
                entity.HasKey(e => e.ContractId)
                    .HasName("PK__RoomCont__C90D346912EA1C68");

                entity.Property(e => e.CheckInDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ConclusionDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.RoomContracts)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__RoomContr__Clien__35BCFE0A");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomContracts)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__RoomContr__RoomI__36B12243");
            });

            modelBuilder.Entity<Base.Service>(entity =>
            {
                entity.Property(e => e.AddedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AvailableState)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Cost).HasColumnType("smallmoney");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.EmployeeId).HasMaxLength(450);

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.RepeatState)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Subtitle).HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('Additional')");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Services__Catego__5070F446");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Services__Employ__4F7CD00D");
            });

            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__ServiceC__19093A0B075D7507");

                entity.HasIndex(e => e.Title, "UQ__ServiceC__2CB664DC35C0D9A4")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImagePath).HasMaxLength(1000);

                entity.Property(e => e.Subtitle).HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.InverseSubCategory)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK__ServiceCa__SubCa__4222D4EF");
            });

            modelBuilder.Entity<ServiceRequest>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.ServiceId, e.BasketId })
                    .HasName("PK__ServiceR__720E2E66E7523092");

                entity.Property(e => e.Comment)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('Work done')");

                entity.Property(e => e.OrderTakeDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServiceRe__Emplo__5FB337D6");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.ServiceRequests)
                    .HasForeignKey(d => new { d.BasketId, d.ServiceId })
                    .HasConstraintName("FK__ServiceRequests__60A75C0F");
            });
        }
    }
}
