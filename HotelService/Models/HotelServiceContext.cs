using HotelService.Models.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HotelService.Models
{
    //[Remote(action: "CheckEmail", controller: "Home", ErrorMessage = "Email уже используется")]
    public partial class HotelServiceContext : IdentityDbContext<User, Role, int>
    {
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<CategoriesService> CategoriesServices { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomContract> RoomContracts { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<WorkStaff> WorkStaffs { get; set; }
        public HotelServiceContext()
        {
        }

        public HotelServiceContext(DbContextOptions<HotelServiceContext> options)
            : base(options)
        {
        }

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

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Passport)
                .IsUnique();

            modelBuilder.Entity<User>().ToTable(nameof(User) + 's');
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<Role>().ToTable(nameof(Role) + 's');

            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.Property(e => e.CostTotal).HasColumnType("money");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Descriprion).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Administrator)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.AdministratorId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Buildings__Admin__300424B4");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Buildings)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Buildings__Image__2F10007B");
            });

            modelBuilder.Entity<CategoriesService>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__Categori__19093A0B4F1DEC63");

                entity.ToTable("CategoriesService");

                entity.HasIndex(e => e.Name, "UQ__Categori__737584F6C4D2B1B1")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.CategoriesServices)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Categorie__Image__5535A963");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.InverseSubCategory)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("FK__Categorie__SubCa__5441852A");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.ServiceId })
                    .HasName("PK__Feedback__5A2FA124FB71E561");

                entity.ToTable("Feedback");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rating).HasDefaultValueSql("((5.0))");

                entity.Property(e => e.Review)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Feedback__Client__7E37BEF6");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Feedback__Servic__7F2BE32F");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.ImageDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImageFormat)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => new { e.BasketId, e.ServiceId })
                    .HasName("PK__Requests__338BCCB5D7AEC6E9");

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.CostTotal).HasColumnType("money");

                entity.Property(e => e.FormationDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RepeatTally).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Basket)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.BasketId)
                    .HasConstraintName("FK__Requests__Basket__693CA210");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK__Requests__Contra__6B24EA82");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Requests__Servic__6A30C649");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Cost)
                    .HasColumnType("smallmoney")
                    .HasDefaultValueSql("((5000.0000))");

                entity.Property(e => e.SleepingPlaces).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('STD')");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK__Rooms__BuildingI__38996AB5");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Rooms__ImageId__37A5467C");
            });

            modelBuilder.Entity<RoomContract>(entity =>
            {
                entity.HasKey(e => e.ContractId)
                    .HasName("PK__RoomCont__C90D3469A56248D2");

                entity.Property(e => e.CheckInDate).HasColumnType("smalldatetime");

                entity.Property(e => e.CheckOutDate).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.RoomContracts)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__RoomContr__Clien__3B75D760");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomContracts)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__RoomContr__RoomI__3C69FB99");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("smallmoney");

                entity.Property(e => e.Descriprion).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RepeatState)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('Daily')");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Services__Catego__619B8048");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Services__ImageI__628FA481");

                entity.HasOne(d => d.ResponsWorkerNavigation)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.ResponsWorker)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Services__Respon__60A75C0F");
            });

            

            modelBuilder.Entity<WorkStaff>(entity =>
            {
                entity.HasKey(e => new { e.WorkerId, e.ServiceId, e.BasketId })
                    .HasName("PK__WorkStaf__0FA2E951BFFB8515");

                entity.ToTable("WorkStaff");

                entity.Property(e => e.Comment)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Work done')");

                entity.Property(e => e.OrderDoneDate).HasColumnType("smalldatetime");

                entity.Property(e => e.OrderTakeDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.WorkStaffs)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WorkStaff__Worke__778AC167");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.WorkStaffs)
                    .HasForeignKey(d => new { d.BasketId, d.ServiceId })
                    .HasConstraintName("FK__WorkStaff__787EE5A0");
            });
        }
    }
}
