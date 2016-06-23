using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IPAdressen
{
    partial class User : System.Web.UI.Page
    {
        public static DBaseDataContext db = new DBaseDataContext();

        bool IsAdmin()
        {

            if (this.Rechte == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }
}