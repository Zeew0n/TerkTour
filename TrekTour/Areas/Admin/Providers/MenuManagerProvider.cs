using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrekTour.Areas.Admin.Models;

namespace TrekTour.Areas.Admin.Providers
{
    public class MenuManagerProvider
    {
        TrekTourEntities ent = new TrekTourEntities();

        public List<MenuManagerModels> GetMenus()
        {
            var collection = ent.Menus.OrderBy(x => x.MenuPosition);
            List<MenuManagerModels> model = new List<MenuManagerModels>();

            foreach (var item in collection)
            {
                MenuManagerModels data = new MenuManagerModels
                {
                    MenuId = item.MenuId,

                    ContentId = item.ContentId,
                    MenuPosition = item.MenuPosition,
                    MenuText = item.MenuText,
                    MenuLinkName = MenuLinkName(item.ContentId),
                    HasChild = hasChild(item.MenuPosition)

                };
                model.Add(data);
            }
            return model;
        }

        private bool hasChild(string MenuPostion)
        {
            int result = ent.Menus.Where(x => x.MenuPosition.StartsWith(MenuPostion)).Count();
            if (result == 1)
                return false;
            else
                return true;
        }

        public void Insert(MenuManagerModels model)
        {
            var objToSave = new Menus
            {
                ContentId = model.ContentId,
                MenuPosition = GetNewPostion(),
                MenuText = model.MenuText
            };
            ent.Menus.Add(objToSave);
            ent.SaveChanges();
        }

        private string GetNewPostion()
        {
            string CurrenctMaxPosition = ent.Menus.Max(x => x.MenuPosition);
            if (CurrenctMaxPosition != null)
            {
                string[] firstposition = CurrenctMaxPosition.Split('.');
                int MaxPostion = int.Parse(firstposition[0]) + 1;

                return MaxPostion.ToString();
            }
            else
                return "1";
        }

        public void SaveMenuPositions(List<MenuManagerModels> model)
        {
            foreach (var item in model)
            {
                var result = ent.Menus.Where(x => x.MenuId == item.MenuId).FirstOrDefault();
                result.MenuPosition = item.MenuPosition;
                ent.Entry(result).State = System.Data.EntityState.Modified;
            }
            ent.SaveChanges();
        }

        public string MenuLinkName(int ContentId)
        {

            var obj = ent.Contents.Where(x => x.ContentId == ContentId).FirstOrDefault();
            if (obj == null)
                return "Link Not Found";
            return ent.Contents.Where(x => x.ContentId == ContentId).FirstOrDefault().Title;


        }

        public MenuManagerModels GetDateForEdit(int MenuId)
        {

            var result = ent.Menus.Where(x => x.MenuId == MenuId).FirstOrDefault();
            MenuManagerModels model = new MenuManagerModels
            {
                MenuId = result.MenuId,
                ContentId = result.ContentId,
                MenuText = result.MenuText,
                ContentItemTitle = MenuLinkName(result.ContentId),
            };
            return model;
        }

        public void Update(MenuManagerModels model)
        {
            var objToEdit = ent.Menus.Where(x => x.MenuId == model.MenuId).FirstOrDefault();
            objToEdit.ContentId = model.ContentId;
            objToEdit.MenuText = model.MenuText;

            ent.Entry(objToEdit).State = System.Data.EntityState.Modified;
            ent.SaveChanges();
        }

        public void Delete(int MenuId)
        {
            var objToDelete = ent.Menus.Where(x => x.MenuId == MenuId).FirstOrDefault();

            ent.Menus.Remove(objToDelete);
            ent.SaveChanges();
        }
    }

}