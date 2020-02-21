using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public long UserId { get; set; }
        public long ChatId { get; set; }
        [NotMapped]
        public string Key { get => CypherClass.md5(UserName + "tramb"); }
        [NotMapped]
        public string GetPass
        {
            get => CypherClass.XORCipher(Password, "tramb");
        }
    }
}
