using System.ComponentModel.DataAnnotations;

namespace SinavOlusturma.Models.Entity
{
    public class Exam
    {


        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string WiredId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }

    }
    public class ExamQuestion
    {
        [Key]
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Question { get; set; }
        public int OrderNo { get; set; }
    }

    public class QuestionsOption

    {
        [Key]
        public int Id { get; set; }
        public int ExamQuestionId { get; set; }
        public string Description { get; set; }
        public bool CorrectResponse { get; set; }
        public string OptionName { get; set; }


    }
    public class User

    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


    }
}
