using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class SportsProduct
    {
        [Key] [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int ProductID { get; set; }
        [Display(Name ="Назва")]
        [Required(ErrorMessage ="Будь ласка, введіть назву")]
        public string ProductName { get; set; }
        [Display(Name = "Країна-виробник")]
        public string ManufCountry { get; set; }
        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Будь ласка, введіть опис")]
        public string Description { get; set; }
        [Display(Name = "Ціна (грн)")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Будь ласка, введіть число більше 0")]
        public int Price { get; set; }
        [Display(Name = "Частина тіла")]
        [Required(ErrorMessage = "Будь ласка, введіть частину тіла")]
        public string PartOfBody { get; set; }
        [Display(Name = "Виробник")]
        [Required(ErrorMessage = "Будь ласка, введіть виробника")]
        public string Manufacturer { get; set; }
        [Display(Name = "Персональний опис (довгий)")]
        public string LongDesc { get; set; }
        [Display(Name = "Фаворит")]
        public bool? IsFavourite { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public byte[] ImageData_2 { get; set; }
        public string ImageMimeType_2 { get; set; }
        public byte[] ImageData_3 { get; set; }
        public string ImageMimeType_3 { get; set; }
        public byte[] ImageData_4 { get; set; }
        public string ImageMimeType_4 { get; set; }
       // public IEnumerable<string> GetCategories() => list.GetCategories();
    }
}
