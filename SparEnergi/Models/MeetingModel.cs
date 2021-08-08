using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SparEnergi.Models
{
    public class MeetingModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 3)]
        [DisplayName("Your Name")]
        public string MeetingRequester { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 8)]
        [RegularExpression(@"^[0-9]{8,8}$", ErrorMessage = "Must be a phonenumber with 8 digits")]
        [DisplayName("Your Phonenumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Request description")]
        public string RequestContent { get; set; }
    }
}
