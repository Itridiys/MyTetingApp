using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MyTetingApp.DbContext;
using MyTetingApp.Models;

namespace MyTetingApp
{
    class Program
    {
        private const string connectionString = "Data Source=(localdb)\\mssqllocaldb; Database=Money; Persist Security Info=false; MultipleActiveResultSets=True; Trusted_Connection=False;";

        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string data = string.Empty;
            DateTime today = DateTime.Now;
            data = today.Date.ToShortDateString();

            string url = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=";

            url += data;

            var table = GetMonyTable(url);

            using (SqlBulkCopy bulkInsert = new SqlBulkCopy(connectionString))
            {
                bulkInsert.DestinationTableName = "[MoneyTable]";
                bulkInsert.BulkCopyTimeout = 300;
                bulkInsert.WriteToServer(table);
            }

            Console.WriteLine("Data is loaded");

            Console.WriteLine("Input date in type Day.Month.Year");
            var date = DateTime.TryParse(Console.ReadLine(), out DateTime d) ? d : DateTime.MinValue;

            Console.WriteLine("Input currency in international format (Example: BYN)");
            var charCode = Console.ReadLine();

            using (var db = new AppDbContext())
            {
                var cash = db.MoneyTable.Where(x => x.DateOfUpload == date && x.CharCode == charCode).OrderBy(x => x.Id);
                foreach (var item in cash)
                {
                    Console.WriteLine($"Currency {item.Name} equal to {item.Value}");
                }
            }
        }

        
        

        public static DataTable GetMonyTable(string url)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(url);
            DataTable result = ds.Tables["Valute"];
            
            result.Columns.Add("Id", typeof(int)).SetOrdinal(0);
            result.Columns.Add("DateOfUpload", typeof(DateTime));

            for (int i = 0; i < result.Rows.Count; i++)
            {
                result.Rows[i][result.Columns.Count - 1] = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return result;
        }

        public static String GetUSDRate(string url)
        {
            //XmlDocument xml_doc = new XmlDocument();
            //xml_doc.Load(url);
            

            DataSet ds = new DataSet();
            ds.ReadXml(url);
            DataTable currency = ds.Tables["Valute"];
            foreach (DataRow row in currency.Rows)
            {
                if (row["CharCode"].ToString() == "USD") //Ищу нужный код валюты
                {
                    return row["Value"].ToString(); //Возвращаю значение курсы валюты
                }
            }
            return "";
        }
    }
}
