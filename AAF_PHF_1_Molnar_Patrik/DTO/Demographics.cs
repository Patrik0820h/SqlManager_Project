using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAF_PHF_1_Molnar_Patrik.DTO
{
    internal class Demographics
    {
        public int Year {  get; set; }
        public string Country { get; set; }
        public int Population { get; set; }

        public Demographics(int year, string country, int population)
        {
            Year = year;
            Country = country;
            Population = population;
        }
    }
}
