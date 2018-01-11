using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrekTour.Areas.Admin.Providers
{
    public class TagHelperProvider
    {
        TrekTourEntities ent = new TrekTourEntities();

        private void SaveNewTages(string Tages)
        {
            string[] collection = Tages.Split(',');

            foreach (var item in collection)
            {
                if (!isAlreadyExistTag(item.Trim()))
                    Insert(item.Trim());
            }
        }

        private bool isAlreadyExistTag(string Name)
        {
            var count = ent.Tags.Where(x => x.Name == Name).Count();

            if (count > 0)
                return true;
            else
                return false;
        }

        private void Insert(string TagName)
        {
            var obj = new Tags
            {
                Name = TagName,
            };
            ent.Tags.Add(obj);
            ent.SaveChanges();
        }

        private int GetTagIdbyName(string TagName)
        {
            return ent.Tags.Where(x => x.Name == TagName).FirstOrDefault().TagId;
        }

        public void ApplyTagOnPackageGroup(string TagList,int ContentId)
        {
            if (TagList!=null)
            {
                SaveNewTages(TagList);
                string[] collection = TagList.Split(',');

                foreach (var item in collection)
                {
                    int _TagId = GetTagIdbyName(item.Trim());
                    if (!isAlreadyExistTagOnPackageGroup(_TagId, ContentId))
                    {
                        var obj = new ContentTags
                        {
                            TagId = _TagId,
                            ContentId = ContentId,
                        };

                        ent.ContentTags.Add(obj);
                        ent.SaveChanges();
                    }
                }
            }

            UnTagOnPackageGroup(TagList, ContentId);


        }


        private void UnTagOnPackageGroup(string TagList, int ContentId)
        {
            if (TagList!=null)
            {
                var PreviousTagged = ent.ContentTags.Where(x => x.ContentId == ContentId);
                string[] NewTagList = TagList.Split(',');

                foreach (var item in PreviousTagged)
                {
                    if (!NewTagList.Contains(GetTagNameByTagId(item.TagId)))
                    {
                        var result = ent.ContentTags.Where(x => x.TagId == item.TagId).FirstOrDefault();
                        ent.ContentTags.Remove(result);
                    }
                }
                ent.SaveChanges();
            }
            else
            {
                foreach (var entity in ent.ContentTags.Where(x => x.ContentId == ContentId))
                {
                    ent.ContentTags.Remove(entity);
                }
                ent.SaveChanges();
            }
        }

        private bool isAlreadyExistTagOnPackageGroup(int TagId, int ContentId)
        {
            var count = ent.ContentTags.Where(x => x.TagId == TagId && x.ContentId == ContentId).Count();
            if (count > 0)
                return true;
            else
                return false;
        }


        public List<string> GetPackageGroupTagValue(int ContentId)
        {
            var collection = ent.ContentTags.Where(X => X.ContentId == ContentId).ToList();

            List<string> data = new List<string>();

            foreach (var item in collection)
            {
                data.Add(GetTagNameByTagId(item.TagId));
            }
            return data;
        }

        private string GetTagNameByTagId(int _tagid)
        {
            return ent.Tags.Where(x => x.TagId == _tagid).FirstOrDefault().Name;
        }
       
    }
}