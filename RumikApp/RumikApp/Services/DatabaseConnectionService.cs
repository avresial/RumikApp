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
                oString = "SELECT * FROM RumsBaseTEST ";
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

            beverageTMP.Vanila.IsSet = IntToBool(reader.GetInt16(8));
            beverageTMP.Nuts.IsSet = IntToBool(reader.GetInt16(9));
            beverageTMP.Carmel.IsSet = IntToBool(reader.GetInt16(10));
            beverageTMP.Smoke.IsSet = IntToBool(reader.GetInt16(11));
            beverageTMP.Cinnamon.IsSet = IntToBool(reader.GetInt16(12));
            beverageTMP.Spirit.IsSet = IntToBool(reader.GetInt16(13));
            beverageTMP.Fruits.IsSet = IntToBool(reader.GetInt16(14));
            beverageTMP.Honey.IsSet = IntToBool(reader.GetInt16(15));

            byte[] buffer = new byte[250000];
            reader.GetBytes(16, 0, buffer, 0, 250000);
            beverageTMP.TestIcon = ImageProcessingService.ConvertToBitMapImage(buffer);

            return beverageTMP;
        }

        public string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// Test method
        /// </summary>
        /// <param name="img"></param>
        public void SaveImageToDatabase(byte[] img)
        {
            using (MySqlConnection con = new MySqlConnection(CnnVal("sosek")))
            {
                //entity framework in future or dapper

                string query = @"INSERT INTO TestImgTable (Name, Image) VALUES (@name, @img)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = "XD";
                cmd.Parameters.Add("@img", MySqlDbType.Binary).Value = img;
                con.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {

                    int id = (int)cmd.LastInsertedId;

                }
                else
                {
                    ;
                    // failure msg ?
                }
                con.Close();
            }
        }

        public string SaveBevreageToDatabase(Beverage beverage, byte[] img)
        {
            //RumsBaseTEST

            string result = null;

            using (MySqlConnection con = new MySqlConnection(CnnVal("sosek")))
            {
                string query = @"INSERT INTO RumsBaseTEST 
                (Name, Capacity, AlcoholPercentage, Price, Grade, GradeWithCoke, Color, Vanilly, Nuts, Carmel, Smoky, Cinnamon, Spirit, Fruits, Honey, Image) 
                VALUES (@Name, @Capacity, @AlcoholPercentage, @Price, @Grade, @GradeWithCoke, @Color, @Vanilly, @Nuts, @Carmel, @Smoky, @Cinnamon, @Spirit, @Fruits, @Honey, @Image)";

                MySqlCommand cmd = new MySqlCommand(query, con);

                cmd.Parameters.Add("@Name", MySqlDbType.VarChar).Value = beverage.Name;
                cmd.Parameters.Add("@Capacity", MySqlDbType.Int64).Value = beverage.Capacity;

                cmd.Parameters.Add("@AlcoholPercentage", MySqlDbType.Float).Value = beverage.AlcoholPercentage;
                cmd.Parameters.Add("@Price", MySqlDbType.Float).Value = beverage.Price;
                cmd.Parameters.Add("@Grade", MySqlDbType.Int32).Value = beverage.Grade;
                cmd.Parameters.Add("@GradeWithCoke", MySqlDbType.Int32).Value = beverage.GradeWithCoke;
                cmd.Parameters.Add("@Color", MySqlDbType.VarChar).Value = beverage.Color;

                cmd.Parameters.Add("@Vanilly", MySqlDbType.Int16).Value = boolToInt16(beverage.Vanila.IsSet);
                cmd.Parameters.Add("@Nuts", MySqlDbType.Int16).Value = boolToInt16(beverage.Nuts.IsSet);
                cmd.Parameters.Add("@Carmel", MySqlDbType.Int16).Value = boolToInt16(beverage.Carmel.IsSet);
                cmd.Parameters.Add("@Smoky", MySqlDbType.Int16).Value = boolToInt16(beverage.Smoke.IsSet);
                cmd.Parameters.Add("@Cinnamon", MySqlDbType.Int16).Value = boolToInt16(beverage.Cinnamon.IsSet);
                cmd.Parameters.Add("@Spirit", MySqlDbType.Int16).Value = boolToInt16(beverage.Spirit.IsSet);
                cmd.Parameters.Add("@Fruits", MySqlDbType.Int16).Value = boolToInt16(beverage.Fruits.IsSet);
                cmd.Parameters.Add("@Honey", MySqlDbType.Int16).Value = boolToInt16(beverage.Honey.IsSet);

                cmd.Parameters.Add("@Image", MySqlDbType.Binary).Value = img;
                con.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    int id = (int)cmd.LastInsertedId;
                    result = "Dodano nowy rekord o Id - " + id + ".";
                }
                else
                {
                    result = "Coś poszło nie tak jak powinno \ndodawanie rekordu nie powiodło się;";
                }
                con.Close();
            }
            result += "\ninformacja z - " + DateTime.Now.ToString();

            return result;
        }
        Int16 boolToInt16(bool boolVariable)
        {
            if (boolVariable)
                return 1;
            else
                return 0;
        }
        bool IntToBool(Int16 Int16Variable)
        {
            if (Int16Variable == 1)
                return true;
            else
                return false;
        }
    }
}
