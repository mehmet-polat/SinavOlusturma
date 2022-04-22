using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SinavOlusturma.Models.ViewModels;

namespace SinavOlusturma.ViewComponents
{
    public class ExamQuestionViewComponent: ViewComponent
    {


        public ViewViewComponentResult Invoke(int OrderNo)
        {
            var Mdl = new ExamQuestionViewModel { OrderNo = OrderNo, Question="dfglkjhgfkljfjsdl" };
            return View(Mdl);
        }
    }
}
