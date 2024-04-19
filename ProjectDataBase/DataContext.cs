using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.ProjectDataBase.Domain;

namespace Project.ProjectDataBase
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }//10
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Collegian> Collegians { get; set; }//7
        public virtual DbSet<CollegianGroup> CollegianGroups { get; set; }//11
        public virtual DbSet<InternshipLocation> InternshipLocations { get; set; }//9
        public virtual DbSet<Group> Groups { get; set; }//12
        public virtual DbSet<Master> Masters { get; set; }//8
        public virtual DbSet<City> Cities { get; set; }//13
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<MasterEvaluation> MasterEvaluations { get; set; }//1
        public virtual DbSet<MasterEvaluationScore> MasterEvaluationScores { get; set; }//2
        public virtual DbSet<InternshipRequest> InternshipRequests { get; set; }//6
        public virtual DbSet<SupervisorEvaluationScore> SupervisorEvaluationScores { get; set; }//4
        public virtual DbSet<SupervisorEvaluation> SupervisorEvaluations { get; set; }//3
        public virtual DbSet<CaptchaCode> CaptchaCodes { get; set; }
        public virtual DbSet<UserImage> UserImages { get; set; }//5
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<FormField> FormFields { get; set; }//14
        public virtual DbSet<CollegianGroupFormField> CollegianGroupFormFields { get; set; }//15
        public virtual DbSet<Attendance> Attendances { get; set; }//15
        public virtual DbSet<University> Universities { get; set; }//15


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MasterEvaluation>()//1
                .HasOne(x => x.CollegianGroup)
                .WithMany(x => x.MasterEvaluations)
                .HasForeignKey(x => new { x.CollegianId, x.GroupId })
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<CollegianGroupFormField>()//15
                .HasOne(x => x.CollegianGroup)
                .WithMany(x => x.CollegianGroupFormFields)
                .HasForeignKey(x => new { x.CollegianId, x.GroupId })
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<MasterEvaluationScore>()//2
                .HasOne(x => x.MasterEvaluation)
                .WithMany(x => x.MasterEvaluationScores)
                .HasForeignKey(x => x.MasterEvaluationId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<SupervisorEvaluation>()//3
                .HasOne(x => x.CollegianGroup)
                .WithMany(x => x.SupervisorEvaluations)
                .HasForeignKey(x => new { x.CollegianId, x.GroupId })
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<SupervisorEvaluationScore>()//4
                .HasOne(x => x.SupervisorEvaluation)
                .WithMany(x => x.SupervisorEvaluationScores)
                .HasForeignKey(x => x.SupervisorEvaluationId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<FormField>()//14
                .HasOne(x => x.Form)
                .WithMany(x => x.FormFields)
                .HasForeignKey(x => x.FormId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserImage>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserImages)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<InternshipRequest>()//6
                .HasOne(x => x.Collegian)
                .WithMany(x => x.InternshipRequests)
                .HasForeignKey(x => x.CollegianId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<InternshipRequest>()
                .HasOne(x => x.Field)
                .WithMany(x => x.InternshipRequests)
                .HasForeignKey(x => x.FieldId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<InternshipRequest>()
                .HasOne(x => x.Term)
                .WithMany(x => x.InternshipRequests)
                .HasForeignKey(x => x.TermId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Collegian>().HasOne(x => x.User)//7
                .WithOne(x => x.Collegian)
                .HasForeignKey<Collegian>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Collegian>().HasOne(x => x.City)//7
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Master>().HasOne(x => x.User)//8
                .WithOne(x => x.Master)
                .HasForeignKey<Master>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
            builder.Entity<InternshipLocation>().HasOne(x => x.User)//9
                .WithOne(x => x.InternshipLocation)
                .HasForeignKey<InternshipLocation>(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<AppUser>()//10
                .HasMany(x => x.RefreshTokens)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CollegianGroup>().HasOne(x => x.Collegian)//11
                .WithMany(x => x.CollegianGroups)
                .HasForeignKey(x => x.CollegianId);
            builder.Entity<CollegianGroup>().HasOne(x => x.Group)
                .WithMany(x => x.CollegianGroups)
                .HasForeignKey(x => x.GroupId);
            builder.Entity<CollegianGroup>().HasKey(x => new { x.CollegianId, x.GroupId });

            builder.Entity<Group>().HasOne(x => x.InternshipLocation)//12
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.InternshipLocationId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Group>().HasOne(x => x.Master)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.MasterId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Group>().HasOne(x => x.Term)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.TermId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Group>().HasOne(x => x.University)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.UniversityId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Group>().HasOne(x => x.Field)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.FieldId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Group>().HasOne(x => x.Form)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.FormId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<City>().HasOne(x => x.Province)//13
               .WithMany(x => x.Cities)
               .HasForeignKey(x => x.ProvinceId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Attendance>()
               .HasOne(x => x.CollegianGroup)
               .WithMany(x => x.Attendances)
               .HasForeignKey(x => new { x.CollegianId, x.GroupId })
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
