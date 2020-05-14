using System;
using System.Linq;
using System.Web.Mvc;
using Univer.PlanTask.Web.Models;
using Univer.PlanTask.Web.NHibernate;

namespace Univer.PlanTask.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        public TaskController()
        {

        }

        public PartialViewResult SearchResult(string search)
        {
            var session = NHibernateHelper.GetCurrentSession();

            var model = session.Query<Task>();
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(task => task.Name.Contains(search) ||
                    task.Description.Contains(search));
            }
            return PartialView("SearchResult", model.ToList());
        }


        public ActionResult Done(long id)
        {

            var session = NHibernateHelper.GetCurrentSession();
            var Task = session.Get<Task>(id);
            if (Task.Status == TaskStatus.Done)

                return RedirectToAction("List");

            Task.Status = TaskStatus.Done;

            using (var tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(Task);
                tx.Commit();
            }

            return RedirectToAction("List");
        }


        public ActionResult Add()
        {
            var model = new Task();

            return View(model);
        }

        public ActionResult Details(long id)
        {
            var session = NHibernateHelper.GetCurrentSession();

            var females = session
                .Query<Task>()
                .Where(c => c.IsSimple == false)
                .ToList();

            var model = females.FirstOrDefault();

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var session = NHibernateHelper.GetCurrentSession();

            var model = session.Get<Task>(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Task model)
        {
            if (model.DateEnd < model.DateStart)
            {
                ModelState.AddModelError("", "Что-то пошло не так");
                ModelState.AddModelError("DateEnd", "Дедлайн не наступит раньше начала");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.User = GetCurrentUser();

            var session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (var tx = session.BeginTransaction())
                {
                    session.SaveOrUpdate(model);
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }


            return RedirectToAction("List");
            //return View(model);
        }


        private User GetCurrentUser()
        {
            var session = NHibernateHelper.GetCurrentSession();

            return session
                .Query<User>()
                .FirstOrDefault(user => user.Login == User.Identity.Name);
        }

        public ActionResult MyTasks()
        {
            var curUser = GetCurrentUser();

            return View("List", curUser.Tasks);
        }

        public ActionResult List(string search )
        {
            var session = NHibernateHelper.GetCurrentSession();

            var model = session.Query<Task>();
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(task => task.Name.Contains(search) ||
               task.Description.Contains(search));
            }
            ViewBag.Search = search;
            return View("List", model.ToList());
        }
    }
}