using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MollieSample
{
    /// <summary>
    /// Summary description for MollieReport
    /// </summary>
    public class MollieReport : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString.Get("transaction_id");
            if(Mollie.VerifyPaid(id))
                context.Response.Write("Betaald en database order updaten");
            else
                context.Response.Write("Betaling niet gelukt");
            context.Response.ContentType = "text/plain";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}