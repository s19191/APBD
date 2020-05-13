using Microsoft.EntityFrameworkCore;

namespace cw3.Models
{
    public partial class s19191Context : DbContext
    {
        public s19191Context()
        {
        }

        public s19191Context(DbContextOptions<s19191Context> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentRole> StudentRole { get; set; }
        public virtual DbSet<Studies> Studies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s19191;Integrated Security=True")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(x => x.IdEnrollment)
                    .HasName("Enrollment_pk");

                entity.Property(e => e.IdEnrollment).ValueGeneratedNever();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.IdStudyNavigation)
                    .WithMany(p => p.Enrollment)
                    .HasForeignKey(x => x.IdStudy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Enrollment_Studies");
            });
            

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(x => x.IndexNumber)
                    .HasName("Student_pk");

                entity.Property(e => e.IndexNumber).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RefreshToken)
                    .HasColumnName("refreshToken")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEnrollmentNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(x => x.IdEnrollment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Enrollment");
            });

            modelBuilder.Entity<StudentRole>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.IndexNumber).HasColumnName("indexNumber");
            });

            modelBuilder.Entity<Studies>(entity =>
            {
                entity.HasKey(x => x.IdStudy)
                    .HasName("Studies_pk");

                entity.Property(e => e.IdStudy).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
