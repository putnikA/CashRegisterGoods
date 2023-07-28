using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashRegisterGoods.Models
{
    public class Users
    {
        private int _id;
        private string _name;
        private string _password;
        private string _email;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Required(ErrorMessage ="Name is required")]
        public string UserName
        {
            get { return _name; }
            set { _name = value; }
        }

        [Required(ErrorMessage ="Password is required")]
        public string UserPassword
        {
            get { return _password; }
            set { _password = value; }
        }

       // [Required(ErrorMessage ="Email is required")]
        public string UserEmail
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}
