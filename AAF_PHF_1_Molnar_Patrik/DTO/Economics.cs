using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAF_PHF_1_Molnar_Patrik.DTO
{
    internal class Economics
    {
        public int Year {  get; set; }
        public string Country { get; set; }
        public float GDP_Growth { get; set; }
        public float Inflation { get; set; }

        public Economics(int year, string country, float gdp_growth, float inflation) 
        {
            Year = year;
            Country = country;
            GDP_Growth = gdp_growth;
            Inflation = inflation;
        }
    }
}
