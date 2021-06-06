using HotelService.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<Article> Articles { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PriceChange> PriceChanges { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestState> RequestStates { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomContract> RoomContracts { get; set; }
        public DbSet<Base.Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<State> States { get; set; }

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
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.AuthorId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ImagePath).HasMaxLength(1024);

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.WriteDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Articles__Author__5CD6CB2B");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.AdminId).HasMaxLength(450);

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.ImagePath).HasMaxLength(1024);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Buildings__Admin__2E1BDC42");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.ServiceId })
                    .HasName("PK__Favorite__5A2FA124030F8C14");

                entity.Property(e => e.ShowState)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Favorites__Clien__6477ECF3");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Favorites__Servi__656C112C");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.ServiceId })
                    .HasName("PK__Feedback__5A2FA124A658E71D");

                entity.ToTable("Feedback");

                entity.Property(e => e.Rating).HasDefaultValueSql("((10))");

                entity.Property(e => e.Review).HasMaxLength(512);

                entity.Property(e => e.WriteDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Feedback__Client__5165187F");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Feedback__Servic__52593CB8");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CostTotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CreditCardNumber)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentDetails).HasMaxLength(256);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.RoomContract)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RoomContractId)
                    .HasConstraintName("FK__Orders__RoomCont__6A30C649");
            });

            modelBuilder.Entity<PriceChange>(entity =>
            {
                entity.Property(e => e.ChangeDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NewPriceValue).HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.PriceChanges)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__PriceChan__Servi__60A75C0F");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(256);

                entity.Property(e => e.DeliveryDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Requests__OrderI__6FE99F9F");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Requests__Servic__70DDC3D8");
            });

            modelBuilder.Entity<RequestState>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.StateId })
                    .HasName("PK__RequestS__8F93F2C96C0BBB06");

                entity.Property(e => e.ChangeDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Comment).HasMaxLength(256);

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestStates)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__RequestSt__Reque__778AC167");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.RequestStates)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK__RequestSt__State__787EE5A0");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((5000.00))");

                entity.Property(e => e.ImagePath).HasMaxLength(1024);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.SleepingPlaces).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasDefaultValueSql("('STD')");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK__Rooms__BuildingI__35BCFE0A");
            });

            modelBuilder.Entity<RoomContract>(entity =>
            {
                entity.Property(e => e.CheckInDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ConclusionDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.RoomContracts)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__RoomContr__Clien__571DF1D5");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomContracts)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__RoomContr__RoomI__5812160E");
            });

            modelBuilder.Entity<Base.Service>(entity =>
            {
                entity.Property(e => e.AddedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AvailableState)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((1000.00))");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.ImagePath).HasMaxLength(1024);

                entity.Property(e => e.RepeatState)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Subtitle)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.BuildingId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Services__Buildi__4BAC3F29");

                entity.HasOne(d => d.ServiceCategory)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ServiceCategoryId)
                    .HasConstraintName("FK__Services__Servic__4CA06362");
            });

            modelBuilder.Entity<ServiceCategory>(entity =>
            {
                entity.HasIndex(e => e.Title, "UQ__ServiceC__2CB664DCDE3B876F")
                    .IsUnique();

                entity.Property(e => e.AvailableState)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.ImagePath).HasMaxLength(1024);

                entity.Property(e => e.Subtitle).HasMaxLength(256);

                entity.Property(e => e.SystemEmployeeId).HasMaxLength(450);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.InverseSubCategory)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK__ServiceCa__SubCa__403A8C7D");

                entity.HasOne(d => d.SystemEmployee)
                    .WithMany(p => p.ServiceCategories)
                    .HasForeignKey(d => d.SystemEmployeeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__ServiceCa__Syste__3F466844");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasIndex(e => e.Value, "UQ__States__07D9BBC262EA0A55")
                    .IsUnique();

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Passport, "UQ__Users__Passport")
                    .IsUnique();

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ForeignerStatus).HasDefaultValueSql("((0))");

                entity.Property(e => e.Gender).HasMaxLength(16);

                entity.Property(e => e.ImagePath).HasMaxLength(1024);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Passport)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date");

                entity.Property(e => e.Patronymic).HasMaxLength(256);

                entity.Property(e => e.RegistrationDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<User>().ToTable(nameof(User) + 's');
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<Role>().ToTable(nameof(Role) + 's');
        }
    }
}
