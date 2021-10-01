using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieReservationsConsole.Models
{
    public class Root
    {
        public Venue Venue { get; set; }
        public Dictionary<string, VenueSeat> Seats { get; set; }
    }
}
