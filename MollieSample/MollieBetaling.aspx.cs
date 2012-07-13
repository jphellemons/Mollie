using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MollieSample
{
    public partial class MollieBetaling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                // check in database if paid. Mollie calls the molliereport to set orders to paid

                //Request.QueryString.Get("transaction_id")
            }
        }
    }
}