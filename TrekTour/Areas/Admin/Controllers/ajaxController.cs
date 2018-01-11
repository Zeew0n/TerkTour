using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrekTour.Areas.Admin.Providers;

namespace TrekTour.Areas.Admin.Provider
{
    public class ajaxController : Controller
    {
        TrekTourEntities ent = new TrekTourEntities();
        TagHelperProvider hlpPro = new TagHelperProvider();

        public JsonResult getTags()
        {
            List<string> tagList = new List<string>();
            var collection = ent.Tags;

            foreach (var item in collection)
            {

                tagList.Add(item.Name);
            }
            return Json(tagList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getContentTags(int? id)
        {
            List<string> tagList = new List<string>();
            tagList = hlpPro.GetPackageGroupTagValue(id.Value);
            return Json(tagList, JsonRequestBehavior.AllowGet);

        }
    }
}