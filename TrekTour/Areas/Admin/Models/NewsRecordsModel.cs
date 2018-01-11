using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrekTour.Areas.Admin.Models
{
    public class NewsRecordsModel
    {
        [Key]
        public int NewsRecordId { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [DisplayName("Descriptions")]
        [Required(ErrorMessage = "*")]
        public string Descriptions { get; set; }

        [Required(ErrorMessage = "*")]
        public bool isActive { get; set; }

        public List<NewsRecordsModel> NewsList { get; set; }
        
    }
}