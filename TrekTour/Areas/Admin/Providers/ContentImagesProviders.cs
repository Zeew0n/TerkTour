using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TrekTour.Areas.Admin.Models;

namespace TrekTour.Areas.Admin.Providers
{
    public class ContentImagesProviders
    {
        TrekTourEntities ent = new TrekTourEntities();

        public List<ContentImagesModels> GetImageList()
        {
            List<ContentImagesModels> model = new List<ContentImagesModels>();

            var collection = ent.ContentImages.OrderByDescending(x => x.CreatedDate);

            foreach (var item in collection)
            {
                ContentImagesModels data = new ContentImagesModels()
                {
                    ImageName = item.ImageName,
                    ContentId = item.ContentId,
                    ContentImageId = item.ContentImageId,
                    ImageFolderName = GetContentImageFolderName(item.ContentId),
                };

                model.Add(data);
            }
            return model;
        }

        public void Insert(ContentImagesModels model)
        {
            var file = model.UploadedFile;

            string dirToUploadFile = ConfigurationManager.AppSettings["ImageRootPath"] + "\\" + model.ImageFolderName;

            string UploadedFileName = ManageImage(file, dirToUploadFile);

            ContentImages objTosave = new ContentImages()
            {
                ContentId = model.ContentId,
                ImageName = UploadedFileName,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            ent.ContentImages.Add(objTosave);
            ent.SaveChanges();
        }

        public string GetContentImageFolderName(int ContentId)
        {
            return ent.Contents.Where(x => x.ContentId == ContentId).FirstOrDefault().ImageFolderName;
        }


        private string ManageImage(HttpPostedFileBase file, string ImageRootFolder)
        {
            try
            {
                string UploadedFileName = AppUploader.UploadFileAndRename(file, AppUploader.ContentPathMode.absolute, ImageRootFolder);
                return UploadedFileName;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public void Delete(int ContentImageId)
        {
            int ContentId = ent.ContentImages.Where(x => x.ContentImageId == ContentImageId).FirstOrDefault().ContentId;
            string FileDirPath = ConfigurationManager.AppSettings["ImageRootPath"] + "\\" + GetContentImageFolderName(ContentId);

            string ImageName = ent.ContentImages.Where(x => x.ContentImageId == ContentImageId).FirstOrDefault().ImageName;

            var objToDelete = ent.ContentImages.Where(x => x.ContentImageId == ContentImageId).FirstOrDefault();
            ent.ContentImages.Remove(objToDelete);
            ent.SaveChanges();

            try
            {
                AppUploader.DeleteFileByName(FileDirPath, ImageName);
            }
            catch
            {

            }


        }
    }
}