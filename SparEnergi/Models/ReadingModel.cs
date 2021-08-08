using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SparEnergi.Models
{
    public class ReadingModel
    {
        public DateTime Date { get; set; }

        public int UserId { get; set; }

        [Required]
        [DisplayName("Energy used in kWh")]
        public int EnergyUsed { get; set; }

        public string EnergyUnit { get; set; }

        [Required]
        [DisplayName("Water used in m3")]
        public int WaterUsed { get; set; }

        public string WaterUnit { get; set; }
    }
}
