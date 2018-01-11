using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrekTour.Areas.Admin.Models;
using AutoMapper;
using System.Data;

namespace TrekTour.Areas.Admin.Providers
{
    public class NewsRecordsProvider
    {
        TrekTourEntities ent = new TrekTourEntities();

        public List<NewsRecordsModel> GetNewsList()
        {
            var model = new List<NewsRecordsModel> ( Mapper.Map< IEnumerable<NewsRecords>, IEnumerable<NewsRecordsModel>> (ent.NewsRecords).OrderByDescending(x => x.NewsRecordId));
            return model;
        }
        public void Insert(NewsRecordsModel model)
        {
            var objToSave = Mapper.Map<NewsRecordsModel, NewsRecords>(model);
            objToSave.CreatedBy = 1;
            objToSave.CreatedDate = DateTime.Now;

            ent.NewsRecords.Add(objToSave);
            ent.SaveChanges();
        }

        public void Update(NewsRecordsModel model)
        {
            var ObjToEdit = ent.NewsRecords.Where(x => x.NewsRecordId == model.NewsRecordId).FirstOrDefault();
            Mapper.Map(model, ObjToEdit);
            ent.Entry(ObjToEdit).State = EntityState.Modified;
            ent.SaveChanges();
        }

        public void Delete(int id)
        {
            var ObjToDelete = ent.NewsRecords.Where(x => x.NewsRecordId == id).FirstOrDefault();
            ent.NewsRecords.Remove(ObjToDelete);
            ent.SaveChanges();
        }
    }
}