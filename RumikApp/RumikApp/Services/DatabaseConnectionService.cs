using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public class DatabaseConnectionService : IDatabaseConnectionService
    {
        public ObservableCollection<Beverage> GetData(string Query)
        {            
            if (Query == null || Query == "")
                return null;

            ObservableCollection<Beverage> allBeverages = new ObservableCollection<Beverage>();

            using (MySqlConnection con = new MySqlConnection(CnnVal("sosek")))
            {

                MySqlCommand cmd0 = new MySqlCommand(Query, con);

                con.Open();

                using (MySqlDataReader reader = cmd0.ExecuteReader())
                    while (reader.Read())
                        allBeverages.Add(saveReaderToBevrage(reader));

                con.Close();
            }
            return allBeverages;

        }

        public ObservableCollection<Beverage> GetAllData()
        {
            ObservableCollection<Beverage> allBeverages = new ObservableCollection<Beverage>();

            using (MySqlConnection con = new MySqlConnection(CnnVal("sosek")))
            {

                //string oString = "SELECT * FROM (SELECT * FROM RumsBase ORDER BY id DESC LIMIT 4) sub ORDER BY id ASC";
                string oString = "SELECT * FROM RumsBase ";

                MySqlCommand cmd0 = new MySqlCommand(oString, con);

                con.Open();

                using (MySqlDataReader reader = cmd0.ExecuteReader())
                    while (reader.Read())
                        allBeverages.Add(saveReaderToBevrage(reader));

                con.Close();
            }
            return allBeverages;

        }

        Beverage saveReaderToBevrage(MySqlDataReader reader)
        {
            Beverage beverageTMP = new Beverage();
            beverageTMP.ID = reader.GetInt32(0);
            beverageTMP.Name = reader.GetString(1);
            beverageTMP.Capacity = reader.GetInt32(2);
            beverageTMP.AlcoholPercentage = reader.GetFloat(3);
            beverageTMP.Price = reader.GetFloat(4);
            beverageTMP.Grade = reader.GetInt32(5);
            beverageTMP.GradeWithCoke = reader.GetInt32(6);
            beverageTMP.Color = reader.GetString(7);

            if (reader.GetInt16(8) == 1)
                beverageTMP.Vanila.IsSet = true;
            else
                beverageTMP.Vanila.IsSet = false;

            if (reader.GetInt16(9) == 1)
                beverageTMP.Nuts.IsSet = true;
            else
                beverageTMP.Nuts.IsSet = false;

            if (reader.GetInt16(10) == 1)
                beverageTMP.Carmel.IsSet = true;
            else
                beverageTMP.Carmel.IsSet = false;

            if (reader.GetInt16(11) == 1)
                beverageTMP.Smoke.IsSet = true;
            else
                beverageTMP.Smoke.IsSet = false;

            if (reader.GetInt16(12) == 1)
                beverageTMP.Cinnamon.IsSet = true;
            else
                beverageTMP.Cinnamon.IsSet = false;

            if (reader.GetInt16(13) == 1)
                beverageTMP.Nutmeg.IsSet = true;
            else
                beverageTMP.Nutmeg.IsSet = false;

            if (reader.GetInt16(14) == 1)
                beverageTMP.Fruits.IsSet = true;
            else
                beverageTMP.Fruits.IsSet = false;

            if (reader.GetInt16(15) == 1)
                beverageTMP.Honey.IsSet = true;
            else
                beverageTMP.Honey.IsSet = false;
            return beverageTMP;
        }

        public string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        void SaveImageToDatabase() 
        {
            using (MySqlConnection con = new MySqlConnection(CnnVal("sosek")))
            {
                byte[] img = new byte[5];

                // Notice that you are missing the third field (the image one)
                // Please replace Image with the correct name of the image field in your table
                string query = @"INSERT INTO example (Name, Description, Image) 
                 VALUES (@name, @description, @img";
                MySqlCommand cmd = new MySqlCommand(query, con);
                //cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = u;
                //cmd.Parameters.Add("@description", MySqlDbType.VarChar).Value = v;
                cmd.Parameters.Add("@img", MySqlDbType.Binary).Value = img;
                con.Open();
                // Do not execute the query two times.
                // cmd.ExecuteNonQuery();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    
                    int id = (int)cmd.LastInsertedId;

                }
                else
                {
                    // failure msg ?
                }
                con.Close();
            }
        }
     
    }
}
