using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MollieSample
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                repBanks.DataSource = Mollie.GetBanks();
                repBanks.DataBind();
            }
        }

        protected void btnGo_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(Mollie.RequestPaymentStart("dit is maar een test",
                Request.Url.GetLeftPart(UriPartial.Authority) +"MollieReport.ashx",
                Request.Url.GetLeftPart(UriPartial.Authority) + "MollieBetaling.aspx",
                Convert.ToInt32(hf.Value), new decimal(10.85)));
        }
    }
}