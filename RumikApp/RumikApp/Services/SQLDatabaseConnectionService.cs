using MySql.Data.MySqlClient;
using RumikApp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumikApp.Services
{
    public class SQLDatabaseConnectionService : ISQLDatabaseConnectionService
    {

        private AvailableTables _MainDataTable = AvailableTables.RumsBaseTEST;
        public AvailableTables MainDataTable
        {
            get { return _MainDataTable; }
            set
            {
                if (_MainDataTable == value)
                    return;
                _MainDataTable = value;
            }
        }

        private AvailableTables _NotYetApprovedTESTDataTable = AvailableTables.NotYetApprovedTEST;
        public AvailableTables NotYetApprovedTESTDataTable
        {
            get { return _NotYetApprovedTESTDataTable; }
            set
            {
                if (_NotYetApprovedTESTDataTable == value)
                    return;
                _NotYetApprovedTESTDataTable = value;
            }
        }

        public bool TestConnectionToDatabase()
        {
            bool doesConnectionWork = true;

            try
            {
                MySqlConnection con = new MySqlConnection(cnnVal("sosek"));

                con.Open();

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            catch (ArgumentException a_ex)
            {
                doesConnectionWork = false;
                // System.Windows.Forms.MessageBox.Show(a_ex.Message.ToString());
            }
            catch (MySqlException ex)
            {
                doesConnectionWork = false;

                //System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }

            return doesConnectionWork;
        }

        public bool TestConnectionToTable(AvailableTables availableTables)
        {

            string oString = "SELECT * FROM " + availableTables.ToString() + " LIMIT 1";

            try
            {
                using (MySqlConnection con = new MySqlConnection(cnnVal("sosek")))
                {
                    MySqlCommand cmd0 = new MySqlCommand(oString, con);

                    con.Open();

                    using (MySqlDataReader reader = cmd0.ExecuteReader())
                        while (reader.Read())
                            return true;

                    con.Close();
                }

            }
            catch (ArgumentException a_ex)
            {
                System.Windows.Forms.MessageBox.Show(a_ex.Message.ToString());
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }

            return false;

        }

        public ObservableCollection<Beverage> GetAllData()
        {
            return getData("SELECT * FROM " + MainDataTable.ToString() + " ORDER BY Name");
        }

        public ObservableCollection<Beverage> GetAllPiratesBeverages()
        {
            return getData("SELECT* FROM " + MainDataTable.ToString() + " WHERE BeAPirate = 1");
        }

        public ObservableCollection<Beverage> GetDataFromDatabaseWithConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints)
        {

            List<string> conditionStrings = getListOfConditions(pollPurpose, pollPurposeWeight, pollMixes, Flavours, pollPricePoints);


            return getDataFromDatabaseWithConditions(conditionStrings);
        }

        public Beverage GetRandomRow(Random random = null)
        {
            if (!TestConnectionToDatabase())
                return null;

            string oString = "SELECT * FROM " + MainDataTable.ToString() + " ORDER BY RAND() LIMIT 1";

            ObservableCollection<Beverage> possibleOneBeverages = getData(oString);

            if (possibleOneBeverages.Count > 0)
                return possibleOneBeverages[0];
            else
                return null;
        }

        public string SaveBevreageToDatabase(Beverage beverage, byte[] img)
        {
            //RumsBaseTEST

            if (!TestConnectionToDatabase())
                return null;

            string result = null;

            using (MySqlConnection con = new MySqlConnection(cnnVal("sosek")))
            {
                string query = $"INSERT INTO {NotYetApprovedTESTDataTable.ToString()} " +
                    $"(Name, Capacity, AlcoholPercentage, Price, Grade, GradeWithCoke, Color, Vanilly, Nuts, Caramel, Smoky, Cinnamon, Spirit, Fruits, Honey, BeAPirate, Image) " +
                    $"VALUES(@Name, @Capacity, @AlcoholPercentage, @Price, @Grade, @GradeWithCoke, @Color, @Vanilly, @Nuts, @Caramel, @Smoky, @Cinnamon, @Spirit, @Fruits, @Honey, @BeAPirate, @Image)";

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
                cmd.Parameters.Add("@Caramel", MySqlDbType.Int16).Value = boolToInt16(beverage.Caramel.IsSet);
                cmd.Parameters.Add("@Smoky", MySqlDbType.Int16).Value = boolToInt16(beverage.Smoke.IsSet);
                cmd.Parameters.Add("@Cinnamon", MySqlDbType.Int16).Value = boolToInt16(beverage.Cinnamon.IsSet);
                cmd.Parameters.Add("@Spirit", MySqlDbType.Int16).Value = boolToInt16(beverage.Spirit.IsSet);
                cmd.Parameters.Add("@Fruits", MySqlDbType.Int16).Value = boolToInt16(beverage.Fruits.IsSet);
                cmd.Parameters.Add("@Honey", MySqlDbType.Int16).Value = boolToInt16(beverage.Honey.IsSet);
                cmd.Parameters.Add("@BeAPirate", MySqlDbType.Int16).Value = boolToInt16(beverage.BeAPirate.IsSet);

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

        private Beverage saveReaderToBevrage(MySqlDataReader reader)
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

            beverageTMP.Vanila.IsSet = intToBool(reader.GetInt16(8));
            beverageTMP.Nuts.IsSet = intToBool(reader.GetInt16(9));
            beverageTMP.Caramel.IsSet = intToBool(reader.GetInt16(10));
            beverageTMP.Smoke.IsSet = intToBool(reader.GetInt16(11));
            beverageTMP.Cinnamon.IsSet = intToBool(reader.GetInt16(12));
            beverageTMP.Spirit.IsSet = intToBool(reader.GetInt16(13));
            beverageTMP.Fruits.IsSet = intToBool(reader.GetInt16(14));
            beverageTMP.Honey.IsSet = intToBool(reader.GetInt16(15));

            byte[] buffer = new byte[250000];
            reader.GetBytes(17, 0, buffer, 0, 250000);
            beverageTMP.TestIcon = ImageProcessingService.ConvertToBitMapImage(buffer);

            return beverageTMP;
        }

        private Int16 boolToInt16(bool boolVariable)
        {
            if (boolVariable)
                return 1;
            else
                return 0;
        }

        private bool intToBool(Int16 Int16Variable)
        {
            if (Int16Variable == 1)
                return true;
            else
                return false;
        }

        private ObservableCollection<Beverage> getData(string Query)
        {
            if (!TestConnectionToDatabase())
                return null;

            if (Query == null || Query == "")
                return null;

            ObservableCollection<Beverage> allBeverages = new ObservableCollection<Beverage>();

            using (MySqlConnection con = new MySqlConnection(cnnVal("sosek")))
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

        private ObservableCollection<Beverage> getDataFromDatabaseWithConditions(List<string> conditions)
        {
            string oString;

            if (conditions.Count > 0)
            {
                oString = $"SELECT * FROM " + MainDataTable.ToString() + " WHERE ";

                for (int i = 0; i < conditions.Count; i++)
                {
                    if (i != 0)
                        oString += " and ";

                    oString += conditions[i];
                }

                if (conditions.Contains("grade > 5"))
                    oString += " ORDER BY Grade DESC";
                else if (conditions.Contains("GradeWithCoke > 5"))
                    oString += " ORDER BY GradeWithCoke DESC";
                //else if (conditions.Contains(" AlcoholPercentage / (100 * (Price/ Capacity))  > 4.0"))
                //    oString += " ORDER BY AlcoholPercentage / (100 * (Price/ Capacity)) DESC";
                //else if (conditions.Contains(" ((Grade+GradeWithCoke)/2)/((100 * (Price/ Capacity))) > 0.8 and ((Grade+GradeWithCoke)/2) > 5"))
                //    oString += " ORDER BY ((Grade+GradeWithCoke)/2)/((100 * (Price/ Capacity))) ASC";
                //else if (Exclusive)
                //    oString += " ORDER BY AlcoholPercentage / (100 * (Price / Capacity)) ASC";
            }
            else
            {
                oString = $"SELECT * FROM " + MainDataTable.ToString();
            }

            return getData(oString);
        }

        private List<string> getListOfConditions(PollPurpose pollPurpose, int pollPurposeWeight, PollMixes pollMixes, List<Flavour> Flavours, PollPricePoints pollPricePoints)
        {
            List<string> conditionsPartQuery = new List<string>();

            if (pollPurpose != PollPurpose.None)
                conditionsPartQuery.Add(getPollPurpose(pollPurpose, pollPurposeWeight));

            if (pollMixes != PollMixes.None)
                conditionsPartQuery.Add(getPollMixes(pollMixes));

            if (Flavours.Count > 0)
                conditionsPartQuery.Add(getFlavours(Flavours));

            if (pollPricePoints != PollPricePoints.None)
                conditionsPartQuery.Add(getStringPrice(pollPricePoints));

            return conditionsPartQuery;
        }

        private string getPollPurpose(PollPurpose pollPurpose, int pollPurposeWeight)
        {
            if (pollPurpose == PollPurpose.ForPartyBool)
                return " AlcoholPercentage / (100 * (Price/ Capacity))  > " + pollPurposeWeight;

            if (pollPurpose == PollPurpose.GoodButCheap)
                return " ((Grade+GradeWithCoke)/2)/((100 * (Price/ Capacity))) > 0.8 and ((Grade+GradeWithCoke)/2) > 5";

            if (pollPurpose == PollPurpose.Exclusive)
                return " Grade > 6 and AlcoholPercentage / (100 * (Price / Capacity)) < " + pollPurposeWeight;

            if (pollPurpose == PollPurpose.ForPiratesFromCarabien)
                return " BeAPirate = 1";

            return null;
        }

        private string getPollMixes(PollMixes pollMixes, int minimalAllowedGrade = 5)
        {
            if (pollMixes == PollMixes.Solo)
                return $"Grade > {minimalAllowedGrade}";

            if (pollMixes == PollMixes.WithCoke)
                return $"GradeWithCoke > {minimalAllowedGrade}";

            return null;
        }

        private string getFlavours(List<Flavour> Flavours)
        {

            List<string> flavoursList = new List<string>();
            string flavoursString = "";

            foreach (Flavour flavour in Flavours)
            {
                if (flavour.Name == nameof(Beverage.Vanila) && flavour.IsSet)
                    flavoursList.Add("Vanilly = 1");

                if (flavour.Name == nameof(Beverage.Nuts) && flavour.IsSet)
                    flavoursList.Add("Nuts = 1");

                if (flavour.Name == nameof(Beverage.Caramel) && flavour.IsSet)
                    flavoursList.Add("Caramel = 1");

                if (flavour.Name == nameof(Beverage.Smoke) && flavour.IsSet)
                    flavoursList.Add("Smoky = 1");

                if (flavour.Name == nameof(Beverage.Cinnamon) && flavour.IsSet)
                    flavoursList.Add("Cinnamon = 1");

                if (flavour.Name == nameof(Beverage.Spirit) && flavour.IsSet)
                    flavoursList.Add("Spirit = 1");

                if (flavour.Name == nameof(Beverage.Fruits) && flavour.IsSet)
                    flavoursList.Add("Fruits = 1");

                if (flavour.Name == nameof(Beverage.Honey) && flavour.IsSet)
                    flavoursList.Add("Honey = 1");
            }

            if (flavoursList.Count > 0)
            {
                for (int i = 0; i < flavoursList.Count; i++)
                {
                    if (i != 0)
                        flavoursString += " and ";

                    flavoursString += flavoursList[i];
                }
                return flavoursString;
            }

            return null;
        }

        private string getStringPrice(PollPricePoints pollPricePoints)
        {

            if (pollPricePoints == PollPricePoints.PricePoint1)
                return " Price < 50";

            if (pollPricePoints == PollPricePoints.PricePoint2)
                return " Price >= 50 and Price < 70";

            if (pollPricePoints == PollPricePoints.PricePoint3)
                return " Price >= 70 and Price < 90";

            if (pollPricePoints == PollPricePoints.PricePoint4)
                return " Price >= 90";

            return null;
        }

        private string cnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
