using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrekTour.Areas.Admin.Models
{
    public class TestimonialsRecordsModel
    {

        [Key]
        public int TestimonialsRecordId { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }

        [DisplayName("Descriptions")]
        [Required(ErrorMessage = "*")]
        public string Descriptions { get; set; }

        public string ImageName { get; set; }

        [Required(ErrorMessage = "*")]
        public bool isActive { get; set; }

        public List<TestimonialsRecordsModel> TestimonialsRecordsList { get; set; }

        //public int CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int? UpdatedBy { get; set; }
        //public DateTime? UpdatedDate { get; set; }
    }
}