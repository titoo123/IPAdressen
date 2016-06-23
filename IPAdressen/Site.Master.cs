using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.Globalization;
using System.Security.Permissions;

namespace IPAdressen
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        DBaseDataContext db = new DBaseDataContext();
        public static User us;

        protected void Page_Init(object sender, EventArgs e)
        {
            var log = from u in db.User
                      where u.Name == WindowsIdentity.GetCurrent().Name
                      select u;

    
            if (log.Count() != 0)
            {
                us = log.First();
                if (us.Distrikt != null)
                {
                    Session["Distrikt"] = us.Distrikt;
                }

            }

            if (log.Count() == 0)
            {
                User usr = new User() { Name = WindowsIdentity.GetCurrent().Name };
                db.User.InsertOnSubmit(usr);
                db.SubmitChanges();


            }
            else if (log.First().Rechte == "Admin")
            {
                NavigationMenu.Items.Add(new MenuItem { Text = "Suche", NavigateUrl = "~/Seiten/Suche.aspx" });
                NavigationMenu.Items.Add(new MenuItem { Text = "Bereiche", NavigateUrl = "~/Seiten/BereichPage.aspx" });
                NavigationMenu.Items.Add(new MenuItem { Text = "Benutzer", NavigateUrl = "~/Seiten/BenutzerPage.aspx" });
                NavigationMenu.Items.Add(new MenuItem { Text = "Anwendungen", NavigateUrl = "~/Seiten/AnwendungenPage.aspx" });
                NavigationMenu.Items.Add(new MenuItem { Text = "Info", NavigateUrl = "~/About.aspx" });

                NavigationMenu.Items.Add(new MenuItem { Text = "User", NavigateUrl = "~/Account/User.aspx" });
                Session["Distrikt"] = null;
            }
            else if (log.First().Rechte == "Schreiben")
            {
                NavigationMenu.Items.Add(new MenuItem { Text = "Suche", NavigateUrl = "~/Seiten/Suche.aspx" });
                NavigationMenu.Items.Add(new MenuItem { Text = "Bereiche", NavigateUrl = "~/Seiten/BereichPage.aspx" });
                NavigationMenu.Items.Add(new MenuItem { Text = "Benutzer", NavigateUrl = "~/Seiten/BenutzerPage.aspx" });
                NavigationMenu.Items.Add(new MenuItem { Text = "Anwendungen", NavigateUrl = "~/Seiten/AnwendungenPage.aspx" });
                NavigationMenu.Items.Add(new MenuItem { Text = "Info", NavigateUrl = "~/About.aspx" });
            }
            else if (log.First().Rechte == "Lesen")
            {
                NavigationMenu.Items.Add(new MenuItem { Text = "Suche", NavigateUrl = "~/Seiten/Suche.aspx" });
                NavigationMenu.Items.Add(new MenuItem { Text = "Info", NavigateUrl = "~/About.aspx" });
            }
            //else
            //{
            //    string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //    if (!url.Contains("Neu.aspx"))
            //    {
            //        Server.Transfer("Neu.aspx");
            //    }
            //    ////Trägt Bereich für mögliche Begrenzungen ein
            //    //Session["Distrikt"] = log.First().Distrikt.Value;
            //}

            try
            {
                db.SubmitChanges();

            }
            catch (Exception)
            {

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //if (!IsPostBack)
            //{
            //    if (us.Rechte == null)
            //    {
            //        string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //        if (!url.Contains("Neu.aspx") && a == false)
            //        {
            //            Server.Transfer("Neu.aspx");
            //            a = true;
            //        }
            //    }
            //}


          
            
        }




        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
           
        }
        protected void HeadLoginView_ViewChanged(object sender, EventArgs e)
        {

        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Response.Buffer = true;
            Response.StatusCode = 401;
            Response.StatusDescription = "Unauthorized";

            Response.AddHeader("WWW-Authenticate", "NTLM");

            Response.End();
        }


    }
}
