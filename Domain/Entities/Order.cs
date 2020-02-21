using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        [Display(Name = "Місто")]
        public string City { get; set; }
        [Display(Name = "Адреса")]
        public string Address { get; set; }
        [Display(Name = "Країна")]
        public string Country { get; set; }
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        public int ProductID { get; set; }
        [Display(Name = "Кількість")]
        public int Quantity { get; set; }
        [Display(Name = "Час")]
        public DateTime OrdTime { get; set; }
        [Display(Name = "Назва товару")]
        public string ProdName { get; set; }
        [Display(Name = "Ціна")]
        public int TotalPrice { get; set; }
    }
}
