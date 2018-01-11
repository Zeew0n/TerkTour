using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrekTour.Areas.Admin.Models;
using TrekTour.Areas.Admin.Providers;

namespace TrekTour.Areas.Admin.Controllers
{
    public class ContentItemsController : Controller
    {
        ContentItemProvider pro = new ContentItemProvider();
        public ActionResult Index(string ContentType)
        {
            ContentItemModel model = new ContentItemModel();
            model.ContentItem = ContentType;
            
            model.ContetnItemList = pro.GetContentList(ContentType);
            return PartialView("/Areas/Admin/Views/Shared/ContentType.cshtml", model);
        }

        public ActionResult GetList(string ContentType)
        {
            return Json(pro.GetContentList(ContentType), JsonRequestBehavior.AllowGet);
        }

    }
}
