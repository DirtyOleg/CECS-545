using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_03
{
    public class CityInfo
    {
        public int ID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public bool IsVisited { get; set; } //Used to determine if this city is already added to the partial tour
    }
}
