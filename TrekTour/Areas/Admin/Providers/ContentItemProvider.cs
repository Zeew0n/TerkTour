using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrekTour.Areas.Admin.Models;

namespace TrekTour.Areas.Admin.Providers
{
    public class ContentItemProvider
    {
        TrekTourEntities ent = new TrekTourEntities();


        public List<ContentItemModel> GetContentList(string ContentType)
        {
            List<ContentItemModel> model = new List<ContentItemModel>();

            var collection = ent.Contents.Where(x => x.isActive == true).OrderByDescending(x => x.ContentId);

            foreach (var item in collection)
            {
                ContentItemModel data = new ContentItemModel
                {
                    Title = item.Title,
                    ContentId = item.ContentId,
                };
                model.Add(data);
            }

            return model;
        }
    }
}