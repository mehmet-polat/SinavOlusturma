using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using SinavOlusturma.ExtensionMethods;
using SinavOlusturma.Models.ViewModels;
using SinavOlusturma.Models.Entity;
using SinavOlusturma.Models;

namespace SinavOlusturma.Controllers
{
    public class ExamController : Controller
    {
        public IActionResult Index()
        {
            var db = new SinavDbContext();
            var ExamList = db.Exams.ToList();
            return View(ExamList);
        }

        [Route("/Exam/{ExamId:int}")]
        public IActionResult Exam(int ExamId)
        {


            var ExamView = new ExamViewModel();
            try
            {
                var Db = new SinavDbContext();
                var Exam = Db.Exams.FirstOrDefault(x => x.Id == ExamId);

                if (Exam != null)
                {
                    ExamView.Id = Exam.Id;
                    ExamView.WiredId = Exam.WiredId;
                    ExamView.Description = Exam.Description;
                    ExamView.Title = Exam.Title;
                    ExamView.Description = Exam.Description;
                    ExamView.ExamQuestions = new List<ExamQuestionViewModel>();

                    var Questions = Db.ExamQuestions.Where(x => x.ExamId == Exam.Id).ToList();
                    foreach (var Quest in Questions)
                    {
                        ExamQuestionViewModel q = new ExamQuestionViewModel
                        {
                            Id = Quest.Id,
                            ExamId = Exam.Id,
                            Question = Quest.Question,
                            OrderNo = Quest.OrderNo,
                        };
                        q.QuestionsOptions = new List<QuestionsOptionViewModel>();
                        var Options = Db.QuestionsOptions.Where(x => x.ExamQuestionId == Quest.Id).ToList();

                        foreach (var Opt in Options)
                        {
                            QuestionsOptionViewModel o = new QuestionsOptionViewModel
                            {

                                Id = Opt.Id,
                                Description = Opt.Description,
                                ExamQuestionId = Quest.Id,
                                OptionName = Opt.OptionName,

                            };

                            q.QuestionsOptions.Add(o);
                        }

                        ExamView.ExamQuestions.Add(q);
                    }
                }
            }
            catch (Exception e)
            {

            }


            return View(ExamView);

        }

        public IActionResult AddExam()
        {
            var url = "https://www.wired.com/feed/rss";
            using var reader = XmlReader.Create(url);
            var PostList = SyndicationFeed.Load(reader).Items.OrderByDescending(x => x.PublishDate).Take(5).ToList();
            TempData["PostList"] = PostList;
            var Mdl = new ExamViewModel();

            return View(Mdl);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamViewModel ExamModel)
        {
            ResultViewModel Result = new ResultViewModel();
            try
            {
                var db = new SinavDbContext();

                if (ModelState.IsValid)
                {

                    var Dt = DateTime.Now.ToString("G");
                    Exam newExam = new Exam
                    {
                        Description = ExamModel.Description,
                        Title = ExamModel.Title,
                        WiredId = ExamModel.WiredId,
                        Date = Dt

                    };

                    db.Exams.Add(newExam);
                    db.SaveChanges();

                    foreach (var question in ExamModel.ExamQuestions)
                    {
                        ExamQuestion q = new ExamQuestion
                        {
                            ExamId = newExam.Id,
                            OrderNo = question.OrderNo,
                            Question = question.Question,
                        };
                        db.ExamQuestions.Add(q);
                        db.SaveChanges();
                        question.Id = q.Id;
                        foreach (var option in question.QuestionsOptions)
                        {
                            QuestionsOption op = new QuestionsOption
                            {
                                Description = option.Description,
                                ExamQuestionId = q.Id,
                                OptionName = option.OptionName,

                            };

                            op.CorrectResponse = (op.OptionName == question.CorrectOption);

                            db.QuestionsOptions.Add(op);
                            db.SaveChanges();
                            option.Id = op.Id;
                        }
                    }
                    ExamModel.Id = newExam.Id;
                    Result.Id = newExam.Id;
                    Result.Status = true;
                    Result.Code = 1;
                    Result.Messsage = "Sınav Başarı ile Kaydedildi!";
                }
            }
            catch
            {

                Result.Status = false;
                Result.Code = 1;
                Result.Messsage = "Bir Sorun Oluştu Daha Sonra Tekrar Deneyin!";
            }


            return Redirect("/Exam");
        }

        public JsonResult GetWiredPos(string WiredId)
        {

            var url = "https://www.wired.com/feed/rss";
            using var reader = XmlReader.Create(url);
            var Post = SyndicationFeed.Load(reader).Items.FirstOrDefault(x => x.Id == WiredId);

            if (Post != null)
            {
                return Json(Post);
            }
            return Json("");
        }

        public JsonResult DeleteExam(int ExamId)
        {

            try
            {
                var Db = new SinavDbContext();
                var Exam = Db.Exams.FirstOrDefault(x => x.Id == ExamId);

                if (Exam != null)
                {
                    var Questions = Db.ExamQuestions.Where(x => x.ExamId == Exam.Id).ToList();
                    foreach (var item in Questions)
                    {
                        var Options = Db.QuestionsOptions.Where(x => x.ExamQuestionId == item.Id);
                        Db.RemoveRange(Options);
                    }
                    Db.RemoveRange(Questions);
                    Db.Remove(Exam);

                    Db.SaveChanges();
                }


            }
            catch (Exception e)
            {
                return Json("Error : " + e.Message);
            }
            return Json("Ok");
        }



        public JsonResult ControlExam(ExamDoneModel ExamDone)
        {

            List< QuestionsOption > QuestionsOptions = new List< QuestionsOption >();

            try
            {
                var Db = new SinavDbContext();

                foreach (var item in ExamDone.SelectedOptions)
                {
                    var Opt =  Db.QuestionsOptions.FirstOrDefault(x => x.Id == item.Id);
                    if (Opt!=null)
                    {
                        item.CorrectResponse = Opt.CorrectResponse;
                    }
                }
            }
            catch (Exception e)
            {
                return Json("Error : " + e.Message);
            }
            return Json(ExamDone);
        }

    }
}
