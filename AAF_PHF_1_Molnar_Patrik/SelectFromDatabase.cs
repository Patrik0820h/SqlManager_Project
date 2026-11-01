using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAF_PHF_1_Molnar_Patrik
{
    internal class SelectFromDatabase
    {
        public static void SelectDatabase(string connString)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // 1. lekerdezeses feladat ( A )
                string selectInflation = "SELECT c.Country_Name, ROUND(SUM(e.Inflation), 2) AS Inflation FROM Economics e LEFT JOIN Country c ON e.CountryID = c.Id GROUP BY c.Country_Name";
                using (MySqlCommand cmd = new MySqlCommand(selectInflation, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Inflation:");
                        Console.WriteLine("--------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0] + ":" + reader[1]);
                        }
                        Console.WriteLine("--------------------------------------------\n");
                    }
                }

                // 2. lekerdezeses feladat ( B )
                string selectgdp = "SELECT c.Country_Name, ROUND(SUM(e.GDP_Growth), 2) AS GDP_Growth FROM Economics e LEFT JOIN Country c ON e.CountryID = c.Id GROUP BY c.Country_Name";
                using (MySqlCommand cmd = new MySqlCommand(selectgdp, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("GDP Growth:");
                        Console.WriteLine("--------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0] + ":" + reader[1]);
                        }
                        Console.WriteLine("--------------------------------------------\n");
                    }
                }
                
                // 3. lekerdeteses feladat ( C ) - A dupla select otlet az enyem de nem tudtam megcsinalni sehogy igy Ai-t kertem meg hogy egeszitse ki ha ez esetleg ez minusz pont.
                string selectPopulation = @"SELECT c.Country_Name, (SELECT Population FROM Demographics WHERE CountryID = c.Id ORDER BY Year ASC LIMIT 1), (SELECT Population FROM Demographics WHERE CountryID = c.Id ORDER BY Year DESC LIMIT 1) FROM Country c";
                using (MySqlCommand cmd = new MySqlCommand(selectPopulation, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Population changes:");
                        Console.WriteLine("--------------------------------------------");
                        while (reader.Read())
                        {
                            double first = Convert.ToDouble(reader[1]);
                            double last = Convert.ToDouble(reader[2]);
                            double change = (last - first) / first * 100;

                            if (change >= 1.5)
                            {
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (change > -1.5)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }

                            Console.WriteLine(reader[0] + ":" + Math.Round(change, 2) + "%");
                            Console.ResetColor();
                        }
                        Console.WriteLine("--------------------------------------------\n");
                    }
                }

                // 4. lekerdezeses feladat ( D )
                string selectEu = "SELECT Year, SUM(Population) FROM Demographics GROUP BY Year ORDER BY Year";
                using (MySqlCommand cmd = new MySqlCommand(selectEu, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Population in eu:");
                        Console.WriteLine("--------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0] + ":" + reader[1]);
                        }
                        Console.WriteLine("--------------------------------------------\n");
                    }
                }

                // 5. lekerdezeses feladat ( E )
                string selectCountry = "SELECT c.Country_Name, SUM(d.Population) FROM Demographics d LEFT JOIN Country c ON d.CountryID = c.Id GROUP BY c.Country_Name ORDER BY SUM(d.Population) DESC LIMIT 5";
                using (MySqlCommand cmd = new MySqlCommand(selectCountry, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Population in country: (TOP 5)");
                        Console.WriteLine("--------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(reader[0] + ":" + reader[1]);
                        }
                        Console.WriteLine("--------------------------------------------\n");
                    }
                }
            }
        } 
    }
}
