using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SparEnergi.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength =8)]
        [DisplayName("User's Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 8)]
        [DisplayName("User's Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("User's Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("User's First Name")]
        public string FirstName { get; set; }

        [DisplayName("User's Last Name")]
        public string LastName { get; set; }

        [DisplayName("User's Street")]
        public string StreetName { get; set; }

        [DisplayName("User's Postal Code")]
        public int PostCode { get; set; }



    }
}
