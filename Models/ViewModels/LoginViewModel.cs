using SinavOlusturma.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace SinavOlusturma.Models.ViewModels
{

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Zorunludur!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Parola Zorunludur!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ExamViewModel
    {
        public ExamViewModel()
        {
            ExamQuestions = new List<ExamQuestionViewModel>();
            ExamQuestions.Add(new ExamQuestionViewModel { OrderNo = 1 });
            ExamQuestions.Add(new ExamQuestionViewModel { OrderNo = 2 });
            ExamQuestions.Add(new ExamQuestionViewModel { OrderNo = 3 });
            ExamQuestions.Add(new ExamQuestionViewModel { OrderNo = 4 });
            ExamQuestions = ExamQuestions.OrderBy(x => x.OrderNo).ToList();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string WiredId { get; set; }
        public string Description { get; set; }

        public List<ExamQuestionViewModel> ExamQuestions { get; set; }


    }

    public class ExamQuestionViewModel
    {
        public ExamQuestionViewModel()
        {
            QuestionsOptions = new List<QuestionsOptionViewModel>();
            QuestionsOptions.Add(new QuestionsOptionViewModel { OptionName = "A" });
            QuestionsOptions.Add(new QuestionsOptionViewModel { OptionName = "B" });
            QuestionsOptions.Add(new QuestionsOptionViewModel { OptionName = "C" });
            QuestionsOptions.Add(new QuestionsOptionViewModel { OptionName = "D" });
            QuestionsOptions = QuestionsOptions.OrderBy(x => x.OptionName).ToList();
        }
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Question { get; set; }
        public int OrderNo { get; set; }

        public string CorrectOption { get; set; }
        public List<QuestionsOptionViewModel> QuestionsOptions { get; set; }


    }

    public class ExamDoneModel

    {

        public int ExamId { get; set; }

        public List<QuestionsOptionViewModel> SelectedOptions { get; set; }
       


    }
    public class QuestionsOptionViewModel

    {

        public int Id { get; set; }
        public int ExamQuestionId { get; set; }
        public string Description { get; set; }
        public bool CorrectResponse { get; set; }
        public string OptionName { get; set; }


    }
}
