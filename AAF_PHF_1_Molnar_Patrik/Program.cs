using AAF_PHF_1_Molnar_Patrik.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAF_PHF_1_Molnar_Patrik
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string connString = "Server=localhost;Database=Project_DB;Uid=root;port=3306;";

            CsvReader.Demo_Reader("C:\\Users\\Patrik\\Desktop\\Programozás\\Iskola\\AAF_PHF_1_Molnar_Patrik\\AAF_PHF_1_Molnar_Patrik\\eu_demographics_data.csv");
            CsvReader.Eco_Reader("C:\\Users\\Patrik\\Desktop\\Programozás\\Iskola\\AAF_PHF_1_Molnar_Patrik\\AAF_PHF_1_Molnar_Patrik\\eu_economics_data.csv");
            DatabaseService.Database(connString);
            SelectFromDatabase.SelectDatabase(connString);
        }
    }
}
