using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace IPAdressen.Extensions
{
    public class MessageBox
    {

        public static void Show(Page p, String m) {
            p.ClientScript.RegisterStartupScript(p.GetType(), "alertScript", "alert('" + m + "');", true);
        }
    }
}