using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace MollieSample
{
    /// <summary>
    /// reference https://www.mollie.nl/support/documentatie/betaaldiensten/ideal
    /// </summary>
    public class Mollie
    {
        private static bool IsTest()
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings.Get("MollieDebug"));
        }

        public static List<Bank> GetBanks()
        {
            string retrieveBanksUrl = "https://secure.mollie.nl/xml/ideal?a=banklist";
            if (IsTest())
                retrieveBanksUrl += "&testmode=true";
            XDocument doc = XDocument.Load(retrieveBanksUrl);
            if (IsTest())
                doc.Save(HttpContext.Current.Server.MapPath("~/BankList.xml"));

            var banks = doc.Root.Elements("bank").Select(x => new Bank(Convert.ToInt32(x.Element("bank_id").Value), x.Element("bank_name").Value)).ToList();
            return banks;
        }

        internal static string RequestPaymentStart(string description, string reportUrl, string returnUrl, int bankId, decimal amount)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://secure.mollie.nl/xml/ideal?a=fetch&partnerid=");
            sb.Append(ConfigurationManager.AppSettings.Get("MolliePartnerId"));
            sb.Append("&description=");
            sb.Append(HttpUtility.UrlEncode(description));
            sb.Append("&reporturl=");
            sb.Append(HttpUtility.UrlEncode(reportUrl));
            sb.Append("&returnurl=");
            sb.Append(HttpUtility.UrlEncode(returnUrl));
            sb.Append("&bank_id=");
            sb.Append(bankId);
            sb.Append("&amount=");
            sb.Append(amount * 100);
            if (IsTest())
                sb.Append("&testmode=true");

            XDocument doc = XDocument.Load(sb.ToString());
            if (IsTest())
                doc.Save(HttpContext.Current.Server.MapPath("~/Fetch.xml"));

            var order =
                doc.Root.Elements("order").Select(
                    x => new Order(x.Element("transaction_id").Value, x.Element("URL").Value)).First();
            
            return order.RedirectUrl;
        }

        public static bool VerifyPaid(string id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://secure.mollie.nl/xml/ideal?a=check&partnerid=");
            sb.Append(ConfigurationManager.AppSettings.Get("MolliePartnerId"));
            sb.Append("&transaction_id=");
            sb.Append(id);
            if (IsTest())
                sb.Append("&testmode=true");
            XDocument doc = XDocument.Load(sb.ToString());
            if (IsTest())
                doc.Save(HttpContext.Current.Server.MapPath("~/Check.xml"));

            return Convert.ToBoolean(doc.Root.Element("payed").Value);
        }
    }

    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Bank(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Order
    {
        public string TransactionId { get; set; }
        public string RedirectUrl { get; set; }

        public Order(string id, string url)
        {
            TransactionId = id;
            RedirectUrl = url;
        }
    }
}