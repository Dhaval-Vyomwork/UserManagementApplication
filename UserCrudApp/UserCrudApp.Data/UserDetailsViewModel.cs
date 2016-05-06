using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCrudApp.Data
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddressAttribute]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int MobileNo { get; set; }
    }
}
