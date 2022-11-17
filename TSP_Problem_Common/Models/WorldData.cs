using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Problem_Common.Models
{
    public class WorldData
    {
        public int TotalCities { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
    }
}
