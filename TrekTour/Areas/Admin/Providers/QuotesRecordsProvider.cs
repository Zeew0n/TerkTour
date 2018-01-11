using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrekTour.Areas.Admin.Models;
using AutoMapper;
using System.Data;
namespace TrekTour.Areas.Admin.Providers
{
    public class QuotesRecordsProvider
    {
        TrekTourEntities ent=new TrekTourEntities();

        public List<QuotesRecordsModel> GetQuotesList()
        { 
            var model = new List<QuotesRecordsModel>(Mapper.Map<IEnumerable<QuotesRecords>,IEnumerable<QuotesRecordsModel>>(ent.QuotesRecords).OrderByDescending(x=>x.QuotesRecordId));
            return model;
        }

        public void Insert(QuotesRecordsModel model)
        {
            var ObjToSave = Mapper.Map<QuotesRecordsModel, QuotesRecords>(model);
            ObjToSave.CreatedBy=1;
            ObjToSave.CreatedDate = DateTime.Now;

            ent.QuotesRecords.Add(ObjToSave);
            ent.SaveChanges();
        }

        public void Update(QuotesRecordsModel model)
        {
            var ObjToEdit = ent.QuotesRecords.Where(x => x.QuotesRecordId == model.QuotesRecordId).FirstOrDefault();
            Mapper.Map(model, ObjToEdit);
            ent.Entry(ObjToEdit).State = EntityState.Modified; ;
            ent.SaveChanges();
        }
        public void Delete( int id)
        {
            var ObjToDelete = ent.QuotesRecords.Where(x => x.QuotesRecordId == id).FirstOrDefault();
            ent.QuotesRecords.Remove(ObjToDelete);
            ent.SaveChanges();
        }
    }
}