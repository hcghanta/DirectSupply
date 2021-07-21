using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace XML2CSVConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // read/load xml file
                XDocument xDocument = XDocument.Load("orders.xml");

                // read the content
                string data = Convert(xDocument);
                Console.WriteLine(data);

                // write the content into csv
                string outputPath = Directory.GetCurrentDirectory() + @"\output.csv";
                File.WriteAllText(outputPath, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private static string Convert(XDocument xDocument)
        {
            // get the ,separated into a string
            StringBuilder result = new StringBuilder();

            //OrderId,ItemId,Item
            //.. ,.. ,..

            result.AppendLine("OrderId,ItemId,Item");
            foreach (var order in xDocument.Descendants("order"))
            {
                var orderId = order.Attribute("id").Value;
                foreach (var item in order.Descendants("items").Descendants("item"))
                {
                    //handle null exceptions
                    string itemId = item.Attribute("id").Value;
                    string itemName = item.Value;

                    // append as a line item
                    result.AppendLine($"{orderId},{itemId},{itemName}");
                }
            }

            return result.ToString();
        }
    }
}
