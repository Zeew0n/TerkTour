using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrekTour.Areas.Admin.Models;
using AutoMapper;
using System.Data;
namespace TrekTour.Areas.Admin.Providers
{
    public class TestimonialsRecordsProvider
    {
        TrekTourEntities ent = new TrekTourEntities();
        public List<TestimonialsRecordsModel> GetTestimonilasList()
        {
            var model = new List<TestimonialsRecordsModel>(Mapper.Map<IEnumerable<TestimonialsRecords>, IEnumerable<TestimonialsRecordsModel>>(ent.TestimonialsRecords).OrderByDescending(x => x.TestimonialsRecordId));
            return model;
        }
        public void Insert(TestimonialsRecordsModel model)
        {
            var ObjToSave = Mapper.Map<TestimonialsRecordsModel, TestimonialsRecords>(model);
            ObjToSave.CreatedBy = 1;
            ObjToSave.CreatedDate = DateTime.Now;
            ent.TestimonialsRecords.Add(ObjToSave);
            ent.SaveChanges();
        }
        public void Update(TestimonialsRecordsModel model)
        {
            var ObjToEdit = ent.TestimonialsRecords.Where(x => x.TestimonialsRecordId == model.TestimonialsRecordId).FirstOrDefault();
            Mapper.Map(model, ObjToEdit);
            ent.Entry(ObjToEdit).State = EntityState.Modified;
            ent.SaveChanges();
        }
        public void Delete(int id)
        {
            var ObjToDelete = ent.TestimonialsRecords.Where(x => x.TestimonialsRecordId == id).FirstOrDefault();
            ent.TestimonialsRecords.Remove(ObjToDelete);
            ent.SaveChanges();
        }
    }
}