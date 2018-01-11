using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrekTour.Areas.Admin.Models
{
    public class MenuManagerModels
    {
        public int MenuId { get; set; }

        [Required]
        [DisplayName("Title")]
        public string MenuText { get; set; }


        public string MenuPosition { get; set; }

        public string MenuLinkName { get; set; }

        [Required]
        [DisplayName("Link Id")]
        public int ContentId { get; set; }

        public bool HasChild { get; set; }

        public string ContentItemTitle { get; set; }
        public List<MenuManagerModels> MenuItemEntries { get; set; }
    }
}