using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrekTour.Areas.Admin.Models
{
    public class ContentImagesModels
    {
        public int ContentImageId { get; set; }

        public int ContentId { get; set; }

        public string ImageName { get; set; }

        public string ImageFolderName { get; set; }

        public List<ContentImagesModels> ContentImagesList { get; set; }

        [Required(ErrorMessage = "File Required")]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}