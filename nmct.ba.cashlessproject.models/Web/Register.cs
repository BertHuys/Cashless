using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.models.Web
{
    public class Register
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter Register name.")]
        [Display(Name = "Register Name")]
        [DataType(DataType.Text)]
        public string RegisterName { get; set; }
        [Required(ErrorMessage = "please enter Device type.")]
        [Display(Name = "Device")]
        [DataType(DataType.Text)]
        public string Device { get; set; }
        [Required(ErrorMessage = "please enter Purchase date .")]
        [Display(Name = "PurchaseDate")]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = "please enter Expiration date.")]
        [Display(Name = "ExpireDate")]
        [DataType(DataType.Date)]
        public DateTime ExpiresDate { get; set; }
    }
}
