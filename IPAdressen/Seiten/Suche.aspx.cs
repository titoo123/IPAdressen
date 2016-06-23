using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using IPAdressen.Extensions;

namespace IPAdressen
{
    public partial class Index : System.Web.UI.Page
    {

        DBaseDataContext d = new DBaseDataContext();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ////BereichPage.message = "";
            //if (SiteMaster.us.Rechte == "" || SiteMaster.us.Rechte == "Lesen")
            //{
            //    this.IPGridView.AutoGenerateSelectButton = false;
            //}
            //else
            //{
            //    this.IPGridView.AutoGenerateSelectButton = true;
            //}
            IPGridView.RowCreated += RowEvent.OnCustomRow; 

            if (!IsPostBack)
            {
                try
                {
                    int dis = Convert.ToInt32(Session["Distrikt"]);
                    var ber =
                         from b in d.Bereiche
                         where b.Id == dis
                         select b;

                    List<String> bList = new List<string>();
                    bList.Add(ber.First().Name);

                    BereichDropDownList.DataSource = bList;
                    BereichDropDownList.DataBind();


                }
                catch (Exception)
                {
                    //Auslesen und Bereitstellen der Bereiche
                    var ber =
                        from b in d.Bereiche
                        select new { b.Id, b.Name };


                    List<String> bList = new List<string>();
                    bList.Add("Alle Bereiche");

                    foreach (var bereich in ber)
                    {
                        bList.Add(bereich.Name);
                    }

                    BereichDropDownList.DataSource = bList;
                    BereichDropDownList.DataBind();
                }



                //Setzt zu übertragene Id zurück
                Session["id"] = null;
                Session["BEid"] = null;

                
                 //Daten für gespeicherte Suche bereitstellen
                 string stmp = Convert.ToString(Session["LetzteSuche"]);

                 if (stmp == "true")
                 {
                     FülleSuche();
                     sButton_Click(sender,e);
                     Session["LetzteSuche"] = "false";
                 }
            }
        }

        protected void IPGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["id"] = IPGridView.GetItemIDByGridView(0).X;

            if (Session["id"] != null)
            {
                Server.Transfer("GerätePage.aspx");
            }
            
        }
        protected void IPGridView_OnDataBound(object sender, EventArgs e)
        {
            
            
        }
        protected void IPGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IPGridView.PageIndex = e.NewPageIndex;
            sButton_Click(sender,e);
        }


        protected void sButton_Click(object sender, EventArgs e)
        {
            //OpenDetail.Visible= true;
            SpeicherSuche();

            //Wertet Dropdownliste aus
            string tmp = BereichDropDownList.SelectedValue;

            //Sucht Wert des Bereichs heraus
            var tmp_query = (from b in d.Bereiche
                             where b.Name == tmp
                             select b.Id).ToList();

            //Sucht Datensätze mit passenden Geräteinformationen
            //Fügt dabei mehrere andere Tabellen hinzu
            var query =
                    from Geräte in d.Geräte

                    where
                    (
                       (Geräte.Name       == null || Geräte.Name.Contains(sTextName.Text)         ) 
                    && (Geräte.Typ        == null || Geräte.Typ.Contains(sTextTyp.Text)           )    
                    && (Geräte.Art        == null || Geräte.Art.Contains(sTextArt.Text)           )
                    && (Geräte.Standort   == null || Geräte.Standort.Contains(sTextStandort.Text) )
                    && (Geräte.AlteIP     == null || Geräte.AlteIP.Contains(sTextAlteIP.Text)     )
                
                    )

                    join Adressen               in d.Adressen       on Geräte.Id equals Adressen.Id_Geräte
                    into AdressenGroups         from Adressen               in AdressenGroups.DefaultIfEmpty()

                    join BenutzerGeräte         in d.BenutzerGeräte on Geräte.Id equals BenutzerGeräte.Id_Gerät
                    into BenutzerGeräteGroups   from BenutzerGeräte         in BenutzerGeräteGroups.DefaultIfEmpty()
                    
                    //join Benutzer               in d.Benutzer       on BenutzerGeräte.Id_Benutzer equals Benutzer.Id
                    //into BenutzerGroups   from Benutzer in BenutzerGroups.DefaultIfEmpty()

                    orderby Adressen.Adb1, Adressen.Adb2,Adressen.Adb3, Adressen.Adb4 // descending 


                    select new { Geräte.Id, Geräte.Name ,Geräte.Art, Geräte.Typ, Geräte.Standort, 
                        Geräte.Id_Bereich, Geräte.AlteIP,
                        Adressen.Adb1, Adressen.Adb2, Adressen.Adb3, Adressen.Adb4, Adressen.Adb5, Adressen.MAC, 

                        BenutzerGeräte.Id_Benutzer, 
                        };

            //Sortiert Bereich heraus
            if (tmp == "Alle Bereiche")
            {
                query = from q in query
                        select q;
            }
            else
            {
                query = from q in query
                        where q.Id_Bereich == Convert.ToInt32(tmp_query.First())
                        select q;
            }



            // Sortiert Datensätze ohne passende Ip/MAC aus
            query =
                 from a in query
                 where (

                  (a.Adb1 == null || a.Adb1.Value.ToString().Contains(Helper.CleanNumber(sTextBox_ad1.Text)))
               && (a.Adb2 == null || a.Adb2.Value.ToString().Contains(Helper.CleanNumber(sTextBox_ad2.Text)))
               && (a.Adb3 == null || a.Adb3.Value.ToString().Contains(Helper.CleanNumber(sTextBox_ad3.Text)))
               && (a.Adb4 == null || a.Adb4.Value.ToString().Contains(Helper.CleanNumber(sTextBox_ad4.Text)))
               && (a.MAC == null || a.MAC.Contains(sTextMAC.Text))

                 )

                 select a;

            //Ist für zusätzliche Nutzdaten zuständig
            var query2 = from q in query
                    join b in d.Benutzer on q.Id_Benutzer equals b.Id into t
                    from rt in t.DefaultIfEmpty()
                    select new
                    {
                        q.Id,
                        q.Name,
                        q.Art,
                        q.Typ,
                        q.Standort,
                        q.Id_Bereich,
                        q.Adb1,
                        q.Adb2,
                        q.Adb3,
                        q.Adb4,
                        q.Adb5,
                        q.AlteIP,
                        q.MAC,
                        Vorname = rt.Vorname,
                        Nachname = rt.Name,
                    };


           
           // Sortiert Personen ohne passenden Namen aus

                query2 =
                from p in query2
                where (
                   (p.Vorname == null || p.Vorname.Contains(sTextPVorname.Text) )
                && (p.Nachname == null || p.Nachname.Contains(sTextNachname.Text))

                 && (p.Name.Contains(sTextBoxAllgemein.Text)
                        || p.Typ.Contains(sTextBoxAllgemein.Text)
                        || p.Art.Contains(sTextBoxAllgemein.Text)
                        || p.Standort.Contains(sTextBoxAllgemein.Text)
                        || p.Vorname.Contains(sTextBoxAllgemein.Text)
                        || p.Nachname.Contains(sTextBoxAllgemein.Text)
                        || p.AlteIP.Contains(sTextBoxAllgemein.Text)
                        )

                )
                select p ;

            //Joint AS400 Tabellen
            var query3 = from m in query2
                         join a in d.AS400Adressen on m.Id equals a.Id_Adressen into s
                         from df in s.DefaultIfEmpty()
                         join b in d.AS400Geräte on df.Id_AS400Geräte equals b.Id into l
                         from tz in l.DefaultIfEmpty()
                         //join c in d.AS400 on tz.Id equals c.Id_Geräte into f
                         //from jk in f.DefaultIfEmpty()
                         //join d in d.AS400Benutzer on jk.Id_Benutzer equals d.Id into z
                         //from uh in z.DefaultIfEmpty()  
                         select new { m.Id, m.Name, m.Art, m.Typ,m.Standort, m.Adb1, m.Adb2, m.Adb3, m.Adb4, m.Adb5, m.MAC, m.Vorname, m.Nachname , tz.Kennung };

            //Filtert DEVR heraus
            if (!(TextBox_DEVRDEPP.Text == String.Empty))
            {
                query3 = from y in query3
                         where  y.Kennung.Contains(TextBox_DEVRDEPP.Text)
                         select y;
            }
            ////Filtert DEU's heraus
            //if (!(TextBox_AS400Profil.Text == String.Empty))
            //{
            //    query3 = from y in query3
            //             where  y.Profil.Contains(TextBox_AS400Profil.Text)
            //             select y;
            //}


            var query4 = from x in query3
                         select new { x.Id, x.Name, x.Art, x.Typ, x.Standort, x.Adb1, x.Adb2, x.Adb3, x.Adb4, x.Adb5, x.MAC, x.Vorname, x.Nachname,x.Kennung };
            //var query4 = from x in query2
            //          select new { x.Id, x.Name, x.Art, x.Typ, x.Standort, x.Adb1, x.Adb2, x.Adb3, x.Adb4, x.MAC, x.Vorname, x.Nachname };

            if (query4.Count() > 0)
            {
                IPGridView.DataSource = query4;
                IPGridView.DataBind();
            }
            else
            {
                IPGridView.DataSource = null;
                IPGridView.DataBind();
            }


            //invisableColumns(IPGridView);
        }

        private void SpeicherSuche()
        {
            Session["sName"] = sTextName.Text;
            Session["sTyp"] = sTextTyp.Text;
            Session["sArt"] = sTextArt.Text;
            Session["sStandtort"] = sTextStandort.Text;
            Session["sAlteIP"] = sTextAlteIP.Text;
            
            Session["sAd1"] = sTextBox_ad1.Text;
            Session["sAd2"] = sTextBox_ad2.Text;
            Session["sAd3"] = sTextBox_ad3.Text;
            Session["sAd4"] = sTextBox_ad4.Text;
            Session["sMAC"] = sTextMAC.Text;

            Session["sVorname"] = sTextPVorname.Text;
            Session["sNachname"] = sTextNachname.Text;

            Session["sAllgemein"] = sTextBoxAllgemein.Text;

            Session["sBereich"] = BereichDropDownList.SelectedValue;
        }

        private void FülleSuche() {

            sTextName.Text = Convert.ToString( Session["sName"]);
            sTextTyp.Text = Convert.ToString(Session["sTyp"]);
            sTextArt.Text = Convert.ToString(Session["sArt"]);
            sTextStandort.Text = Convert.ToString(Session["sStandtort"]);
            sTextAlteIP.Text = Convert.ToString(Session["sAlteIP"]);

            sTextBox_ad1.Text = Convert.ToString(Session["sAd1"]);
            sTextBox_ad2.Text = Convert.ToString(Session["sAd2"]);
            sTextBox_ad3.Text = Convert.ToString(Session["sAd3"]);
            sTextBox_ad4.Text = Convert.ToString(Session["sAd4"]);
            sTextMAC.Text = Convert.ToString(Session["sMAC"]);

            sTextPVorname.Text = Convert.ToString(Session["sVorname"]);
            sTextNachname.Text = Convert.ToString(Session["sNachname"]);
            sTextBoxAllgemein.Text = Convert.ToString(Session["sAllgemein"]);
            BereichDropDownList.Text = Convert.ToString(Session["sBereich"]);


        }

        private void invisableColumns(ExtendedControls.ExtendedGridView g) {

            //for (int i = 0; i < g.Columns.Count; i++)
            //{
            //    if (g.Columns[i].HeaderText == "Id_Bereich" || g.Columns[i].HeaderText == "AlteIP")
            //    {
            //        g.Columns[i].Visible = false;
            //    }
            //}
            if (g.Rows.Count > 0)
            {
                g.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                //g.SetColumnsVisabiltiyFalse("Id");
                g.SetColumnsVisabiltiyFalse(0);

               // g.SetColumnsVisabiltiyFalse(5);
               //// g.SetColumnsVisabiltiyFalse(10);
                g.SetByteNumberFormat(6);
                g.SetByteNumberFormat(7);
                g.SetByteNumberFormat(8);
                g.SetByteNumberFormat(5);
               // g.SetStringMACFormat(11);
            }

        }
    }
}