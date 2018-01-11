using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TrekTour.Areas.Admin.Models;

namespace TrekTour.Areas.Admin.Providers
{
    public class ContentsProviders
    {
        TrekTourEntities ent = new TrekTourEntities();
        TagHelperProvider pro = new TagHelperProvider();

        public List<ContentsModels> GetList()
        {
            var model = new List<ContentsModels>(AutoMapper.Mapper.Map<IEnumerable<Contents>, IEnumerable<ContentsModels>>(ent.Contents).OrderByDescending(x=>x.ContentId));
            return model;
        }

        public void Insert(ContentsModels model)
        {
            Guid ImageFolderName = Guid.NewGuid();
            var objToSave = AutoMapper.Mapper.Map<ContentsModels, Contents>(model);

            objToSave.ImageFolderName = ImageFolderName.ToString();

            ent.Contents.Add(objToSave);
            ent.SaveChanges();

            model.ContentId = objToSave.ContentId;

            SaveFunctionsId(model);
            pro.ApplyTagOnPackageGroup(model.TagValues,objToSave.ContentId);
            AppUploader.CreateDirectory(ImageFolderName.ToString(), ConfigurationManager.AppSettings["ImageRootPath"]);
        }

        public void Update(ContentsModels model)
        {
            var objToEdit = ent.Contents.Where(x => x.ContentId == model.ContentId).FirstOrDefault();
            AutoMapper.Mapper.Map(model, objToEdit);

            ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
            
            SaveFunctionsId(model);
            pro.ApplyTagOnPackageGroup(model.TagValues, model.ContentId);
        }

        public void Delete(int ContentId)
        {
            RemoveAllFunctions(ContentId);
            RemoveAllTags(ContentId);

            var objToDelete = ent.Contents.Where(x => x.ContentId == ContentId).FirstOrDefault();
            ent.Contents.Remove(objToDelete);
            ent.SaveChanges();
        }

        private void SaveFunctionsId(ContentsModels model)
        {

            RemoveAllFunctions(model.ContentId);

            if (model.SelectedFunctionIDs != null)
            {
                foreach (var item in model.SelectedFunctionIDs)
                {
                    addFunctions(item, model.ContentId);

                }
            }
        }

        private void addFunctions(int FunctionId, int ContentId)
        {
            using (TrekTourEntities ent = new TrekTourEntities())
            {
                var objToSave = new ContentFunctions()
                {
                    ContentId = ContentId,
                    FunctionId = FunctionId
                };

                ent.ContentFunctions.Add(objToSave);
                ent.SaveChanges();
            }
        }


        private void RemoveAllFunctions(int ContentId)
        {
            var collection = ent.ContentFunctions.Where(x => x.ContentId==ContentId);

            foreach (var item in collection)
            {
                 var objToDelete = ent.ContentFunctions.Where(x=>x.ContentFunctionId == item.ContentFunctionId).FirstOrDefault();
                ent.ContentFunctions.Remove(objToDelete);
               
            }
            ent.SaveChanges();
        }

        private void RemoveAllTags(int ContentId)
        {
            var collection = ent.ContentTags.Where(x => x.ContentId == ContentId);

            foreach (var item in collection)
            {
                 var objToDelete = ent.ContentTags.Where(x=>x.ContentTagId == item.ContentTagId).FirstOrDefault();
                ent.ContentTags.Remove(objToDelete);
            }
            ent.SaveChanges();
        }
    }
}