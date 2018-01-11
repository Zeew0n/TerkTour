using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrekTour.Areas.Admin.Models
{
    public class ContentItemModel
    {
        public string ContentItem { get; set; }

        public string Title { get; set; }
        public int ContentId { get; set; }
        

        public List<ContentItemModel> ContetnItemList { get; set; }

        [Required]
        [DisplayName("Content Type")]
        public string ContentType { get; set; }

        
    }
}