using System.ComponentModel.DataAnnotations;

namespace webQLCF.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập user name")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mời nhập password")]
        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}