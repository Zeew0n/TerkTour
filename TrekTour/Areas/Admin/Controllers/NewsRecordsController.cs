using System.Linq;
using System.Web.Mvc;
using TrekTour.Areas.Admin.Models;
using TrekTour.Areas.Admin.Providers;

namespace TrekTour.Areas.Admin.Controllers
{
    public class NewsRecordsController : Controller
    {
        NewsRecordsProvider Provider = new NewsRecordsProvider();

        public ActionResult Index()
        {
            NewsRecordsModel model = new NewsRecordsModel();
            model.NewsList = Provider.GetNewsList();
            return View(model);
        }

        public ActionResult Create()
        {
            NewsRecordsModel model = new NewsRecordsModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(NewsRecordsModel model)
        {
            if (ModelState.IsValid)
            {
                Provider.Insert(model);
                return RedirectToAction("Index");
            }
            return View(model);

        }

        public ActionResult Edit(int id)
        {
            NewsRecordsModel model = new NewsRecordsModel();
            model = Provider.GetNewsList().Where(x => x.NewsRecordId == id).FirstOrDefault();
            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(NewsRecordsModel model)
        {
            if (ModelState.IsValid)
            {
                Provider.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Provider.Delete(id);
            return RedirectToAction("Index");
        }


    }
}