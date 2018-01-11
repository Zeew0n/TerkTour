using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrekTour.Areas.Admin.Models;
using TrekTour.Areas.Admin.Providers;

namespace TrekTour.Areas.Admin.Controllers
{
    public class ContentsController : Controller
    {
        ContentsProviders pro = new ContentsProviders();

        public ActionResult Index()
        {
            ContentsModels model = new ContentsModels();
            model.ContentsList = pro.GetList();
            return View(model);
        }

        public ActionResult Create()
        {
            ContentsModels model = new ContentsModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ContentsModels model)
        {
            if (ModelState.IsValid)
            {
                pro.Insert(model); //Create Folder
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ContentsModels model = new ContentsModels();
            model = pro.GetList().Where(x => x.ContentId == id).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, ContentsModels model)
        {
            if (ModelState.IsValid)
            {
                pro.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            pro.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
