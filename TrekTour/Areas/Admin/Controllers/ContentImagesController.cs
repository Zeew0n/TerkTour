using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrekTour.Areas.Admin.Models;
using TrekTour.Areas.Admin.Providers;

namespace TrekTour.Areas.Admin.Controllers
{
    public class ContentImagesController : Controller
    {
        ContentImagesProviders pro = new ContentImagesProviders();

        public ActionResult Index(int id)
        {
            ContentImagesModels model = new ContentImagesModels();
            model.ContentImagesList = pro.GetImageList().Where(x => x.ContentId == id).ToList();
            model.ContentId = id;
            model.ImageFolderName = pro.GetContentImageFolderName(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(ContentImagesModels model)
        {
            pro.Insert(model);
            model.ContentImagesList = pro.GetImageList();
            return RedirectToAction("Index", new { id = model.ContentId });
        }

        public ActionResult Delete(int id, int ContentId)
        {
            pro.Delete(id);
            return RedirectToAction("Index", new { id = ContentId });
        }

    }
}
