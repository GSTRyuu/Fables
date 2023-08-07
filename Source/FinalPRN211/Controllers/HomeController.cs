using FinalPRN211.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalPRN211.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //checkLogIn
            ViewBag.IsLoggedIn = checkLogIn();
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (checkLogIn())
            {
                ViewBag.LogOut = "2";
                //checkLogIn
                ViewBag.IsLoggedIn = checkLogIn();
                return View("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(string uname, string pass, string name)
        {

            User u = new User();
            User n = new User();
            using (Online_Exam_PRNContext context = new Online_Exam_PRNContext())
            {
                n = context.Users.FirstOrDefault(x => x.UserName == uname);
                if (n != null)
                {
                    return View("SignUp");
                }
                u.Name = name;
                u.UserName = uname;
                u.Password = pass;
                context.Add(u);
                context.SaveChanges();
                ViewBag.LogOut = "4";
                return View("Login");
            }
        }



        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("uid");
            HttpContext.Session.Remove("name");
            ViewBag.LogOut = "1";
            //checkLogIn
            ViewBag.IsLoggedIn = checkLogIn();
            return View("Index");
        }

        [HttpPost]
        public IActionResult Login(string uname, string pass)
        {
            using (Online_Exam_PRNContext context = new Online_Exam_PRNContext())
            {
                User u = new User();
                u = context.Users.FirstOrDefault(u => u.UserName == uname && u.Password == pass);
                if (u == null)
                {
                    return View();
                }
                else
                {
                    HttpContext.Session.SetString("name", u.Name);
                    HttpContext.Session.SetInt32("uid", u.Id);
                    ViewBag.Name = u.Name;
                    ViewBag.LogOut = "3";
                    //checkLogIn
                    ViewBag.IsLoggedIn = checkLogIn();
                    return View("Index");
                }
            }
        }


        [HttpGet]
        public IActionResult List()
        {
            if (!checkLogIn())
            {
                ViewBag.LogOut = "2";
                //checkLogIn
                ViewBag.IsLoggedIn = checkLogIn();
                return View("Index");
            }
            using (Online_Exam_PRNContext context = new Online_Exam_PRNContext())
            {
                List<Grade> g = new List<Grade>();
                g = context.Grades.ToList();
                //checkLogIn
                ViewBag.IsLoggedIn = checkLogIn();
                return View(g);

            };
        }


        public IActionResult Exam(int id)
        {
            using (Online_Exam_PRNContext context = new Online_Exam_PRNContext())
            {
                List<Test> t = new List<Test>();
                t = context.Tests.Where(t => t.GradeId == id).ToList();
                //checkLogIn
                ViewBag.IsLoggedIn = checkLogIn();
                return View(t);


            };
        }


        public IActionResult Set(int teid)
        {

            ViewBag.Teid = teid;
            return View();

        }

        public IActionResult Test(int teid, int tim)
        {

            List<Question> q = new List<Question>();
            HttpContext.Session.SetInt32("teid", teid);
            q = GetQuestions(teid);
            //checkLogIn
            ViewBag.IsLoggedIn = checkLogIn();
            ViewBag.Tim = tim;
            return View(q);

        }


        public IActionResult Check(int totalA, int timeSpent)
        {

            int minutes = timeSpent / 60;
            int seconds = timeSpent % 60;
            Console.WriteLine(minutes + " " + seconds);
            List<int> selectedAnswers = new List<int>();
            List<int> allQuestion = new List<int>();
            int correctA = 0;
            int mark = 0;
            var form = Request.Form;
            int? teid = HttpContext.Session.GetInt32("teid");
            int? uid = HttpContext.Session.GetInt32("uid");
            using (Online_Exam_PRNContext context = new Online_Exam_PRNContext())
            {
                //Lay ra list id cua answer correct va question de luu vao database
                foreach (var key in form.Keys)
                {
                    if (key.StartsWith("answer_"))
                    {
                        var answerId = form[key];
                        if (answerId.Count() > 1)
                        {
                            foreach (var x in answerId)
                            {
                                selectedAnswers.Add(int.Parse(x));
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(answerId))
                            {
                                selectedAnswers.Add(int.Parse(answerId));
                            }
                        }

                    }

                    if (key.StartsWith("question_"))
                    {
                        var questionId = form[key];
                        if (!string.IsNullOrEmpty(questionId))
                        {
                            allQuestion.Add(int.Parse(questionId));
                        }
                    }
                }


                //Tinh diem

                if (totalA == 0)
                {
                    mark = 0;
                }
                else
                {
                    foreach (int i in selectedAnswers)
                    {
                        Answer ans = new Answer();
                        ans = context.Answers.FirstOrDefault(ans => ans.Id == i);
                        if (ans.IsCorrect)
                        {
                            correctA++;
                        }
                    }
                    mark = correctA * (100 / totalA);
                }


                ViewBag.Mark = mark;

                //Luu du lieu bai lam vao History

                History h = new History();
                h.TestId = (int)teid;
                h.UserId = (int)uid;
                h.Answer = string.Join(" ", selectedAnswers);
                h.Question = string.Join(" ", allQuestion);
                h.Mark = mark;
                h.Date = DateTime.Now;

                context.Add(h);
                context.SaveChanges();

                //Hien thi lai de da thi
                List<Question> q = new List<Question>();
                Question que = new Question();
                foreach (int x in allQuestion)
                {
                    que = context.Questions.FirstOrDefault(que => que.Id == x);
                    q.Add(que);

                }
                List<Answer> a = new List<Answer>();
                foreach (Question qq in q)
                {
                    a = context.Answers.Where(a => a.QuestionId == qq.Id).ToList();
                    qq.Answers = a;

                }

                //Hien thi lai dap an da chon va danh gia correct hay incorrect
                ViewBag.SelectedAnswer = selectedAnswers;
                //checkLogIn
                ViewBag.IsLoggedIn = checkLogIn();

                return View(q);
            };
        }


        public IActionResult History()
        {
            if (!checkLogIn())
            {
                ViewBag.LogOut = "2";
                //checkLogIn
                ViewBag.IsLoggedIn = checkLogIn();
                return View("Index");
            }
            List<History> h = new List<History>();
            int? uid = HttpContext.Session.GetInt32("uid");
            int x = (int)uid;
            using (Online_Exam_PRNContext context = new Online_Exam_PRNContext())
            {
                h = context.Histories.Where(h => h.UserId == x).ToList();
                //checkLogIn
                ViewBag.IsLoggedIn = checkLogIn();
                return View(h);
            };


        }


        public IActionResult HistoryTest(int id)
        {
            if (!checkLogIn())
            {
                ViewBag.LogOut = "2";
                //checkLogIn
                ViewBag.IsLoggedIn = checkLogIn();
                return View("Index");
            }
            History h = new History();
            List<int> selectedAnswers = new List<int>();
            List<Question> q = new List<Question>();
            Question que = new Question();

            using (Online_Exam_PRNContext context = new Online_Exam_PRNContext())
            {
                //Lay du lieu History tuong ung
                h = context.Histories.FirstOrDefault(h => h.Id == id);

                //Lay va tach string answer va question
                string qtext = h.Question;
                string atext = h.Answer;
                string[] numberQS = qtext.Split(' ');
                string[] numberAS = atext.Split(' ');

                foreach (string numberString in numberQS)
                {
                    if (int.TryParse(numberString, out int number))
                    {
                        que = context.Questions.FirstOrDefault(que => que.Id == number);
                        q.Add(que);
                    }
                }


                foreach (string numberString in numberAS)
                {
                    if (int.TryParse(numberString, out int number))
                    {
                        selectedAnswers.Add(number);
                    }
                }
                ViewBag.SelectedAnswer = selectedAnswers;


                //Lay cac cau tra loi ung voi cac cau hoi
                List<Answer> a = new List<Answer>();
                foreach (Question qq in q)
                {
                    a = context.Answers.Where(a => a.QuestionId == qq.Id).ToList();
                    qq.Answers = a;
                }


                //Lay diem
                ViewBag.Mark = h.Mark;
                //checkLogIn
                ViewBag.IsLoggedIn = checkLogIn();
                return View(q);
            };


        }


        public List<Question> GetQuestions(int teid)
        {
            using (Online_Exam_PRNContext context = new Online_Exam_PRNContext())
            {
                List<Question> q = new List<Question>();
                List<Answer> a = new List<Answer>();
                q = context.Questions.Where(q => q.TestId == teid).Where(q => q.TestId == teid)
                                                                  .OrderBy(q => Guid.NewGuid())
                                                                  .Take(10)
                                                                  .ToList();

                foreach (Question qq in q)
                {
                    a = context.Answers.Where(a => a.QuestionId == qq.Id).ToList();
                    qq.Answers = a;

                }
                return q;
            };
        }


        public bool checkLogIn()
        {
            bool isl = true;
            int? uid = HttpContext.Session.GetInt32("uid");
            if (uid == null)
            {
                isl = false;
            }
            return isl;
        }




    }
}
