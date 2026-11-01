using AAF_PHF_1_Molnar_Patrik.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AAF_PHF_1_Molnar_Patrik
{
    internal static class CsvReader
    {
        public static List<Demographics> DemographicsList = new List<Demographics>();
        public static List<Economics> EconomicsList = new List<Economics>();

        public static void Demo_Reader(string path) 
        {
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();

                while (!sr.EndOfStream) 
                { 
                    var line = sr.ReadLine(); 
                    string[] splitted_line = line.Split(',');


                    Demographics Demo = new Demographics(int.Parse(splitted_line[0]), splitted_line[1], int.Parse(splitted_line[2]));
                    DemographicsList.Add(Demo);

                }
            }
        }

        public static void Eco_Reader(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var splitted_line = line.Split(',');

                    Economics Eco = new Economics(int.Parse(splitted_line[0]), splitted_line[1], float.Parse(splitted_line[2], CultureInfo.InvariantCulture), float.Parse(splitted_line[3], CultureInfo.InvariantCulture));
                    EconomicsList.Add(Eco);

                }
            }
        }
    }
}
