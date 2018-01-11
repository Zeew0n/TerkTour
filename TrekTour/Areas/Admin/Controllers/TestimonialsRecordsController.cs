using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrekTour.Areas.Admin.Providers;
using TrekTour.Areas.Admin.Models;
namespace TrekTour.Areas.Admin.Controllers
{
    public class TestimonialsRecordsController : Controller
    {
        TestimonialsRecordsProvider provider = new TestimonialsRecordsProvider();
        TestimonialsRecordsModel model = new TestimonialsRecordsModel();
       
        public ActionResult Index()
        {
            TestimonialsRecordsModel model = new TestimonialsRecordsModel();
            model.TestimonialsRecordsList = provider.GetTestimonilasList();
            return View(model);
        }

        public ActionResult Create()
        {
            TestimonialsRecordsModel model = new TestimonialsRecordsModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TestimonialsRecordsModel model)
        {

            if (ModelState.IsValid)
            {
           
               provider.Insert(model);
                    return RedirectToAction("Index");
            
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var model = provider.GetTestimonilasList().Where(x => x.TestimonialsRecordId == id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(TestimonialsRecordsModel model)
        {

            if (ModelState.IsValid)
            {
                
                    provider.Update(model);
                    return RedirectToAction("Index");
                }
               
            
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            provider.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
