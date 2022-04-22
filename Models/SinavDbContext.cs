using Microsoft.EntityFrameworkCore;
using SinavOlusturma.Models.Entity;
using SinavOlusturma.Models.ViewModels;

namespace SinavOlusturma.Models
{

    public class SinavDbContext : DbContext
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<QuestionsOption> QuestionsOptions { get; set; }
        public DbSet<User> Users { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source =.\\AppData\\SinavOlusturma.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>().ToTable("Exams");
            modelBuilder.Entity<ExamQuestion>().ToTable("ExamQuestions");
            modelBuilder.Entity<QuestionsOption>().ToTable("QuestionsOptions");
            modelBuilder.Entity<User>().ToTable("Users");
        }

        public DbSet<SinavOlusturma.Models.ViewModels.ExamViewModel> ExamViewModel { get; set; }
    }
}
