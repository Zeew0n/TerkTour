using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrekTour
{
    public static class Utility
    {

        public static List<Functions> GetFunctions()
        {
            using (TrekTourEntities ent=new TrekTourEntities())
            {
                return ent.Functions.ToList();
            };
        }

        public static bool isExitFunction(int FunctionId, int ContentId)
        {
            using (TrekTourEntities ent = new TrekTourEntities())
            {
                return ent.ContentFunctions.Where(x => x.FunctionId == FunctionId && x.ContentId == ContentId).Any();
            }
        }

        public static int GetTotalImageOnContent(int ContentId)
        {
            using (TrekTourEntities ent = new TrekTourEntities())
            {
                return ent.ContentImages.Where(x => x.ContentId == ContentId).Count();
            };
        }
    }
}