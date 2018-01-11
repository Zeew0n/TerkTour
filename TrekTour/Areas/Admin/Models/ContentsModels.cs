using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrekTour.Areas.Admin.Models
{
    public class ContentsModels
    {
        public int ContentId { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        public string SubTitle { get; set; }

        [DisplayName("Descriptions")]
        [Required(ErrorMessage = "*")]
        public string Descriptions { get; set; }

        //public string ImageFolderName { get; set; }

        [Required(ErrorMessage = "*")]
        public bool isFeatured { get; set; }

        [Required(ErrorMessage = "*")]
        public bool isActive { get; set; }

        public string TagValues { get; set; }

        public List<ContentsModels> ContentsList { get; set; }

       
        public IEnumerable<SelectListItem> FunctionList()
        {
            return new SelectList(Utility.GetFunctions(), "FunctionId", "FunctionName");
        }

        public int GetTotalImageOnContent(int ContentId)
        {
            return Utility.GetTotalImageOnContent(ContentId);
        }

        public List<int> SelectedFunctionIDs { get; set; }
    }

}