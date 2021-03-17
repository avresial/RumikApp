﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public class DatabaseConnectionService
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
                beverageTMP.Vanila = true;
            else
                beverageTMP.Vanila = false;

            if (reader.GetInt16(9) == 1)
                beverageTMP.Nuts = true;
            else
                beverageTMP.Nuts = false;

            if (reader.GetInt16(10) == 1)
                beverageTMP.Caramel = true;
            else
                beverageTMP.Caramel = false;

            if (reader.GetInt16(11) == 1)
                beverageTMP.Smoke = true;
            else
                beverageTMP.Smoke = false;

            if (reader.GetInt16(12) == 1)
                beverageTMP.Cinnamon = true;
            else
                beverageTMP.Cinnamon = false;

            if (reader.GetInt16(13) == 1)
                beverageTMP.Nutmeg = true;
            else
                beverageTMP.Nutmeg = false;

            if (reader.GetInt16(14) == 1)
                beverageTMP.Fruits = true;
            else
                beverageTMP.Fruits = false;

            if (reader.GetInt16(15) == 1)
                beverageTMP.Honey = true;
            else
                beverageTMP.Honey = false;
            return beverageTMP;
        }

        public string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
