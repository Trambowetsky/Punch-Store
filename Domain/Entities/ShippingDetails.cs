using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Введіть Ваше ім'я")]
        [Display(Name = "ПІБ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажіть номер відділення Нової Пошти")]
        [Display( Name = "Номер відділення Нової Пошти:")]
        public string Line1 { get; set; }
        [Required(ErrorMessage = "Введіть номер мобільного")]
        [Display(Name = "Мобільний номер:")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Введіть назву міста")]
        [Display(Name = "Місто:")]
        public string City { get; set; }
        [Required(ErrorMessage = "Країна доставки")]
        [Display(Name = "Країна:")]
        public string Country { get; set; }
    }
}
