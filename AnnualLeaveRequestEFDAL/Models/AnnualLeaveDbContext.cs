using Microsoft.EntityFrameworkCore;

namespace AnnualLeaveRequestEFDAL.Models
{
    public partial class AnnualLeaveDbContext : DbContext
    {
        public AnnualLeaveDbContext()
        {
        }

        public AnnualLeaveDbContext(DbContextOptions<AnnualLeaveDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnnualLeaveRequest> AnnualLeaveRequests { get; set; } = null!;
        public virtual DbSet<AnnualLeaveRequestsOverview> AnnualLeaveRequestsOverviews { get; set; } = null!;
        public virtual DbSet<AnnualLeaveYear> AnnualLeaveYears { get; set; } = null!;

        public IQueryable<AnnualLeaveRequestsBetweenTwoDates> GetAnnualLeaveRequestsBetweenTwoDates(DateTime startDate, DateTime returnDate)
                => FromExpression(() => GetAnnualLeaveRequestsBetweenTwoDates(startDate, returnDate));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnnualLeaveRequest>(entity =>
            {
                entity.ToTable("AnnualLeaveRequest");

                entity.Property(e => e.AnnualLeaveRequestId).HasColumnName("AnnualLeaveRequestID");

                entity.Property(e => e.AssociatedAnnualLeaveRequestId).HasColumnName("AssociatedAnnualLeaveRequestID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LeaveType).HasMaxLength(25);

                entity.Property(e => e.Notes).HasMaxLength(150);

                entity.Property(e => e.NumberOfAnnualLeaveDays).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfDays).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfPublicLeaveDays).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidLeaveType).HasMaxLength(6);

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<AnnualLeaveRequestsOverview>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AnnualLeaveRequestsOverview");

                entity.Property(e => e.AnnualLeaveRequestId).HasColumnName("AnnualLeaveRequestID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LeaveType).HasMaxLength(25);

                entity.Property(e => e.Notes).HasMaxLength(150);

                entity.Property(e => e.NumberOfAnnualLeaveDays).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfAnnualLeaveDaysLeftForYear).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfAnnualLeaveDaysRequested).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfDays).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfDaysLeftForYear).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfDaysRequested).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfPublicLeaveDays).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfPublicLeaveDaysLeftForYear).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfPublicLeaveDaysRequested).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PaidLeaveType).HasMaxLength(6);

                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<AnnualLeaveYear>(entity =>
            {
                entity.HasKey(e => e.Year);

                entity.ToTable("AnnualLeaveYear");

                entity.Property(e => e.Year).ValueGeneratedNever();

                entity.Property(e => e.NumberOfAnnualLeaveDays).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfAnnualLeaveDaysLeft).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfDays).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfDaysLeft).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfPublicLeaveDays).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NumberOfPublicLeaveDaysLeft).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.HasDbFunction(typeof(AnnualLeaveDbContext)
                        .GetMethod(nameof(GetAnnualLeaveRequestsBetweenTwoDates)
                            , new[] { typeof(DateTime), typeof(DateTime) }))
                        .HasName("NumberOfAnnualLeaveDaysBetweenTwoDatesGet");

            modelBuilder.Entity<AnnualLeaveRequestsBetweenTwoDates>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
