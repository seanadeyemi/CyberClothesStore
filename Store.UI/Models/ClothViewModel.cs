using Store.UI.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.UI.Models
{
    public class ClothViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        //[FileSize(102400)]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase ImageFile { get; set; }


        public bool IsFavorite { get; set; }

        [Display(Name = "Stock")]
        public int InStock { get; set; }


        [Display(Name = "Category")]
        public string CategoryName { get; set; }
       
        
    }
}