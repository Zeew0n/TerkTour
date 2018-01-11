using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrekTour.Areas.Admin.Models;
using TrekTour.Areas.Admin.Providers;

namespace TrekTour.Areas.Admin.Controllers
{
    public class QuotesRecordsController : Controller
    {
        QuotesRecordsProvider Provider = new QuotesRecordsProvider();

        public ActionResult Index()
        {
            QuotesRecordsModel model = new QuotesRecordsModel();
            model.QuotesList = Provider.GetQuotesList();
            return View(model);
        }

        public ActionResult Create()
        {
            QuotesRecordsModel model = new QuotesRecordsModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(QuotesRecordsModel model)
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
            QuotesRecordsModel model = new QuotesRecordsModel();
            model = Provider.GetQuotesList().Where(x => x.QuotesRecordId == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(QuotesRecordsModel model)
        {
            if (ModelState.IsValid)
            {

                Provider.Update(model);
                return RedirectToAction("Index");

            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            Provider.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
