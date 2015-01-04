using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models.Web
{
    public class Organisation{
        public int ID { get; set; }
        [Required(ErrorMessage="please enter Login.")]
        [Display(Name = "Login")]
        [DataType(DataType.Text)]
        public string Login { get; set; }
        [Required(ErrorMessage = "please enter password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "please enter database name.")]
        [Display(Name = "DbName")]
        public string DbName { get; set; }
        [Required(ErrorMessage = "please enter database username.")]
        [DataType(DataType.Text)]
        [Display(Name = "DbLogin")]
        public string DbLogin { get; set; }
        [Required(ErrorMessage = "please enter database password.")]
        [DataType(DataType.Password)]
        [Display(Name = "DbPassword")]
        public string DbPassword { get; set; }
        [Required(ErrorMessage = "please enter organisation name.")]
        [DataType(DataType.Text)]
        [Display(Name = "OrganisationName")]
        public string OrganisationName { get; set; }
        [Required(ErrorMessage = "please enter address.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "please enter email.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "please enter phonenumber.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public override string ToString()
        {
            return OrganisationName;
        }


    }
}
