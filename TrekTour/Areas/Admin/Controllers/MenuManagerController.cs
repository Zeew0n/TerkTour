using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrekTour.Areas.Admin.Models;
using TrekTour.Areas.Admin.Providers;

namespace TrekTour.Areas.Admin.Controllers
{
    public class MenuManagerController : Controller
    {
        MenuManagerProvider pro = new MenuManagerProvider();

        public ActionResult Index()
        {
            MenuManagerModels model = new MenuManagerModels();
            model.MenuItemEntries = new List<MenuManagerModels>();
            model.MenuItemEntries = pro.GetMenus();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(MenuManagerModels model)
        {
            pro.SaveMenuPositions(model.MenuItemEntries);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            MenuManagerModels model = new MenuManagerModels();
            model.ContentItemTitle = "Empty";
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(MenuManagerModels model)
        {
            pro.Insert(model);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            MenuManagerModels model = pro.GetDateForEdit(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MenuManagerModels model, int id)
        {
            model.MenuId = id;
            pro.Update(model);
            model.ContentItemTitle = pro.MenuLinkName(model.ContentId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            pro.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
