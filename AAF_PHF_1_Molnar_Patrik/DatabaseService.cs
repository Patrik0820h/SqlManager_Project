using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAF_PHF_1_Molnar_Patrik
{
    internal class DatabaseService
    {
        public static void Database(string connString) 
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                // DROP TABLE
                string dropQuery = "DROP TABLE IF EXISTS Demographics; DROP TABLE IF EXISTS Economics; DROP TABLE IF EXISTS Country;";
                using (MySqlCommand cmd = new MySqlCommand(dropQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // CREATE SCRIPTS
                string createCountry = "CREATE TABLE IF NOT EXISTS Country (Id INT AUTO_INCREMENT PRIMARY KEY, Country_Name VARCHAR(255) UNIQUE)";
                using (MySqlCommand cmd = new MySqlCommand(createCountry, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string createDemo = "CREATE TABLE IF NOT EXISTS Demographics (Id INT AUTO_INCREMENT PRIMARY KEY, CountryID INT, Year INT, Population FLOAT, FOREIGN KEY (CountryID) REFERENCES Country(Id))";
                using (MySqlCommand cmd = new MySqlCommand(createDemo, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string createEco = "CREATE TABLE IF NOT EXISTS Economics (Id INT AUTO_INCREMENT PRIMARY KEY, CountryID INT, Year INT, GDP_Growth FLOAT, Inflation FLOAT, FOREIGN KEY (CountryID) REFERENCES Country(Id))";
                using (MySqlCommand cmd = new MySqlCommand(createEco, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                // INSERT SCRIPTS
                string insertCountry = "INSERT IGNORE INTO Country (Country_Name) VALUES (@Country_Name)";
                using (MySqlCommand cmd = new MySqlCommand(insertCountry, conn))
                {
                    foreach (var item in CsvReader.EconomicsList)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Country_Name", item.Country);
                        cmd.ExecuteNonQuery();
                    }

                    foreach (var item in CsvReader.DemographicsList)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Country_Name", item.Country);
                        cmd.ExecuteNonQuery();
                    }
              
                }

                string selectCountryId = "SELECT Id FROM Country WHERE Country_Name = @Country_Name";
                string insertDemo = "INSERT INTO Demographics (CountryID, Year, Population) VALUES (@CountryID, @Year, @Population)";
                string insertEco = "INSERT INTO Economics (CountryID, Year, GDP_Growth, Inflation) VALUES (@CountryID, @Year, @GDP_Growth, @Inflation)";
                using (MySqlCommand cmd = new MySqlCommand(selectCountryId, conn))
                {
                    using (MySqlCommand cmd2 = new MySqlCommand(insertDemo, conn))
                    {
                        foreach (var item in CsvReader.DemographicsList)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Country_Name", item.Country);
                            int countryid = Convert.ToInt32(cmd.ExecuteScalar());

                            cmd2.Parameters.Clear();
                            cmd2.Parameters.AddWithValue("@CountryID", countryid);
                            cmd2.Parameters.AddWithValue("@Year", item.Year);
                            cmd2.Parameters.AddWithValue("@Population", item.Population);
                            cmd2.ExecuteNonQuery();

                        }
                    }

                    using (MySqlCommand cmd2 = new MySqlCommand(insertEco, conn))
                    {
                        foreach (var item in CsvReader.EconomicsList)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Country_Name", item.Country);
                            int countryid = Convert.ToInt32(cmd.ExecuteScalar());

                            cmd2.Parameters.Clear();
                            cmd2.Parameters.AddWithValue("@CountryID", countryid);
                            cmd2.Parameters.AddWithValue("@Year", item.Year);
                            cmd2.Parameters.AddWithValue("@GDP_Growth", item.GDP_Growth);
                            cmd2.Parameters.AddWithValue("@Inflation", item.Inflation);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
