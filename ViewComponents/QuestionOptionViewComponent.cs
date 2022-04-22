using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SinavOlusturma.Models.ViewModels;

namespace SinavOlusturma.ViewComponents
{
    public class QuestionOptionViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke(string OptionName)
        {

            var Mdl = new QuestionsOptionViewModel { OptionName = OptionName, Description="dasdasdasdgfdhb" };

            return View(Mdl);
        }
    }
}
